using KaVE.Commons.Model.Events;
using KaVE.Commons.Model.Events.CompletionEvents;
using KaVE.Commons.Model.Events.VisualStudio;
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
            Dictionary<String,Developer> developers = new Dictionary<String,Developer>();
            String developerId;
            Event vsEvent;
            Developer developer;

            btn_run.Enabled = false;
            String eventsDir = tb_eventDir.Text;
            Console.Write("looking (recursively) for events in folder {0}\n", Path.GetFullPath(eventsDir));

            pb_file_progress.Value = 0;
            pb_quick.Value = 0;


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
                        if (pb_quick.Value == 100) 
                            pb_quick.Value = 0;
                        developerId = process(e,out vsEvent);

                        developers.TryGetValue(developerId, out developer);

                        if (developer == null)
                        {
                            developer = new Developer(developerId);
                            developer.addEvent(vsEvent);
                            developers.Add(developerId, developer);
                        }
                        else
                        {
                            developer.addEvent(vsEvent);
                        }

                        pb_quick.Value += 1;
                        lbl_time.Text = watch.Elapsed.ToString(@"hh\:mm\:ss");
                            
                        Application.DoEvents();
                    }
                }
                    
                pb_file_progress.Value += progressPortion;
            }

            using(StreamWriter writer = new StreamWriter(tb_output + "\\summaries.csv")){
                foreach (KeyValuePair<string, Developer> entry in developers)
                {
                    developer = entry.Value;
                    developer.writeEvents(tb_output.Text);

                    writer.WriteLine("Developer: " + developer.session_id);
                    foreach(String summary in developer.summaries)
                    {
                        writer.WriteLine("\t" + summary);
                    }
                }
            }

            watch.Stop();
            pb_file_progress.Value = 100;
            lbl_time.Text = watch.Elapsed.ToString(@"hh\:mm\:ss");

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
        private String process(IDEEvent e,out Event vsEvent)
        {
            CommandEvent ce = e as CommandEvent;
            CompletionEvent compE = e as CompletionEvent;
            DocumentEvent docE = e as DocumentEvent;
            WindowEvent winE = e as WindowEvent;
            NavigationEvent navE = e as NavigationEvent;

            String developerId = "";

            if (ce != null) developerId = process(ce, out vsEvent);
            else if (docE != null) developerId = process(docE, out vsEvent);
            else if (winE != null) developerId = process(winE, out vsEvent);
            else if (navE != null) developerId = process(navE, out vsEvent);
            else developerId = processBasic(e, out vsEvent);
            return developerId;
        }

        private String process(CommandEvent ce, out Event cmd)
        {
            //writer.WriteLine("found a CommandEvent (id: " + ce.CommandId + ")");
            cmd = new Command(ce.CommandId);
            cmd.triggeredAt = ce.TriggeredAt;
            
            return ce.IDESessionUUID;
        }

        private String process(DocumentEvent de, out Event cmd)
        {
            //writer.WriteLine("found a CommandEvent (id: " + ce.CommandId + ")");
            cmd = new DocuemntCmd(de.Action.ToString());
            if(de.Document != null)
                ((DocuemntCmd)cmd).docName = de.Document.FileName;
            if(de.ActiveDocument != null)
                ((DocuemntCmd)cmd).docName = de.ActiveDocument.FileName;
            cmd.triggeredAt = de.TriggeredAt;

            return de.IDESessionUUID;
        }
        private String process(WindowEvent we, out Event cmd)
        {
            //writer.WriteLine("found a CommandEvent (id: " + ce.CommandId + ")");
            cmd = new WindowCmd(we.Action.ToString());
            if(we.Window != null)
                ((WindowCmd)cmd).windowName = we.Window.Caption;
            cmd.triggeredAt = we.TriggeredAt;

            return we.IDESessionUUID;
        }

        private String process(NavigationEvent ne, out Event cmd)
        {
            //writer.WriteLine("found a CommandEvent (id: " + ce.CommandId + ")");
            cmd = new NavCmd(ne.TypeOfNavigation.ToString());
            ((NavCmd)cmd).target = ne.Target.Identifier;
            cmd.triggeredAt = ne.TriggeredAt;

            return ne.IDESessionUUID;
        }
        //private String process(StreamWriter writer, CompletionEvent e,out Event vsEvent)
        //{
        //    var snapshotOfEnclosingType = e.Context2.SST;
        //    var enclosingTypeName = snapshotOfEnclosingType.EnclosingType.FullName;
        //    vsEvent = new Event(enclosingTypeName);

        //    //writer.WriteLine("found a CompletionEvent (was triggered in: "+enclosingTypeName+")");

        //    return e.IDESessionUUID;
        //}

        private String processBasic(IDEEvent e, out Event vsEvent)
        {
            var eventType = e.GetType().Name;
            var triggerTime = e.TriggeredAt ?? DateTime.MinValue;
            vsEvent = new Event(eventType);

            //writer.WriteLine("found an " + eventType + " that has been triggered at: " + triggerTime + ")");

            return e.IDESessionUUID;
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
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            result = dialog.ShowDialog(this);

            if (result == System.Windows.Forms.DialogResult.OK)
                tb_output.Text = dialog.SelectedPath;
        }
    }
}
