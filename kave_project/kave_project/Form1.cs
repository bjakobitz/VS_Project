﻿using KaVE.Commons.Model.Events;
using KaVE.Commons.Model.Events.CompletionEvents;
using KaVE.Commons.Utils.Collections;
using KaVE.Commons.Utils.IO.Archives;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kave_project
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        public void Run()
        {
            int progressPortion;
            Stopwatch watch = new Stopwatch();
            String cmd;
            Dictionary<String,Command> commands = new Dictionary<String,Command>();
            Command command;
            Object[] row;

            btn_run.Enabled = false;
            String eventsDir = tb_eventDir.Text;
            Console.Write("looking (recursively) for events in folder {0}\n", Path.GetFullPath(eventsDir));

            pb_file_progress.Value = 0;

            using (StreamWriter writer = new StreamWriter(tb_output.Text, false))
            {
                /*
                 * Each .zip that is contained in the eventsDir represents all events
                 * that we have collected for a specific user, the folder represents the
                 * first day when the user uploaded data.
                 */
                var userZips = FindUserZips(eventsDir);

                progressPortion = 100 / userZips.Count;

                watch.Start();
                foreach (var userZip in userZips)
                {
                    Console.Write("\n#### processing user zip: {0} #####\n", userZip);

                    // open the .zip file ...
                    using (IReadingArchive ra = new ReadingArchive(Path.Combine(eventsDir, userZip)))
                    {
                        // ... and iterate over content.
                        while (ra.HasNext())
                        {
                            /*
                             * within the userZip, each stored event is contained as a
                             * single file that contains the Json representation of a
                             * subclass of IDEEvent.
                             */
                            var e = ra.GetNext<IDEEvent>();

                            // the events can then be processed individually
                            cmd = process(writer,e);
                            if (cmd.Length > 0)
                            {
                                commands.TryGetValue(cmd, out command);
                                if (command == null)
                                {
                                    command = new Command(cmd);
                                    commands.Add(cmd, command);
                                }
                                else
                                {
                                    command.incrementCount();
                                }
                            }
                            
                            writer.Flush();
                            Application.DoEvents();
                        }
                    }
                    
                    pb_file_progress.Value += progressPortion;
                }
            }
            watch.Stop();
            pb_file_progress.Value = 100;
            lbl_time.Text = watch.Elapsed.ToString(@"hh\:mm\:ss");

            foreach (KeyValuePair<string, Command> entry in commands)
            {
                command = entry.Value;
                row = new object[2];
                row[0] = command.name;
                row[1] = command.count;
                dgv_counts.Rows.Add(row);
            }
            btn_run.Enabled = true;
        }

        /*
             * will recursively search for all .zip files in the eventsDir. The paths
             * that are returned are relative to the eventsDir.
             */
        public ISet<string> FindUserZips(String eventsDir)
        {
            var prefix = eventsDir.EndsWith("\\") ? eventsDir : eventsDir + "\\";
            var zips = Directory.EnumerateFiles(eventsDir, "*.zip", SearchOption.AllDirectories)
                .Select(f => f.Replace(prefix, ""));
            return Sets.NewHashSetFrom(zips);
        }

        /*
             * if you review the type hierarchy of IDEEvent, you will realize that
             * several subclasses exist that provide access to context information that
             * is specific to the event type.
             * 
             * To access the context, you should check for the runtime type of the event
             * and cast it accordingly.
             * 
             * As soon as I have some more time, I will implement the visitor pattern to
             * get rid of the casting. For now, this is recommended way to access the
             * contents.
             */
        private String process(StreamWriter writer, IDEEvent e)
        {
            CommandEvent ce = e as CommandEvent;
            CompletionEvent compE = e as CompletionEvent;
            String cmd = "";

            if (ce != null) cmd = process(writer, ce);
            //else if (compE != null) process(writer, compE);
            //else processBasic(writer, e);
            return cmd;
        }

        private String process(StreamWriter writer, CommandEvent ce)
        {
            writer.WriteLine("found a CommandEvent (id: " + ce.CommandId + ")");
            return ce.CommandId;
        }

        private void process(StreamWriter writer, CompletionEvent e)
        {
            var snapshotOfEnclosingType = e.Context2.SST;
            var enclosingTypeName = snapshotOfEnclosingType.EnclosingType.FullName;

            writer.WriteLine("found a CompletionEvent (was triggered in: "+enclosingTypeName+")");
        }

        private void processBasic(StreamWriter writer, IDEEvent e)
        {
            var eventType = e.GetType().Name;
            var triggerTime = e.TriggeredAt ?? DateTime.MinValue;

            writer.WriteLine("found an " + eventType + " that has been triggered at: " + triggerTime + ")");
        }

        private void btn_run_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void btn_event_browse_Click(object sender, EventArgs e)
        {
            DialogResult result;
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            result = dialog.ShowDialog(this);

            if (result == System.Windows.Forms.DialogResult.OK)
                tb_eventDir.Text = dialog.SelectedPath;

        }

        private void btn_output_browse_Click(object sender, EventArgs e)
        {
            DialogResult result;
            SaveFileDialog dialog = new SaveFileDialog();

            result = dialog.ShowDialog(this);

            if (result == System.Windows.Forms.DialogResult.OK)
                tb_output.Text = dialog.FileName;
        }
    }
}