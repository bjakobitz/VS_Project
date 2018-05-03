using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kave_project
{
    class Developer
    {
        private String _session_id;
        private List<List<Event>> _eventLists;
        private List<String> _summaries;

        public String session_id { get { return _session_id; } private set { } }
        public List<List<Event>> eventsLists { get { return _eventLists; } private set { } }
        public List<String> summaries { get { return _summaries; } private set { } }

        public Developer(String session_id)
        {
            _session_id = session_id;
            _eventLists = new List<List<Event>>();
        }

        public void addEvent(Event vsEvent)
        {
            List<Event> eventList;

            switch (vsEvent.name)
            {
                case "Collapse All":
                    goto case "Home";
                case "Show All Files":
                    goto case "Home";
                case "Sync with Active Document (Ctrl+[, S)":
                    goto case "Home";
                case "Home":
                    eventList = new List<Event>();
                    eventList.Add(vsEvent);
                    _eventLists.Add(eventList);
                    break;
                default:
                    if (_eventLists.Count > 0)
                    {
                        eventList = _eventLists[_eventLists.Count - 1];
                        eventList.Add(vsEvent);
                    }
                    break;
                    
            }
        }

        public void writeEvents(String dir)
        {
            Event ev;
            int cmds;
            int docs;
            int window;
            int nav;
            int other;
            String summary;
            _summaries = new List<string>();

            if (eventsLists.Count > 0)
            {
                using (StreamWriter writer = new StreamWriter(dir + "\\" + _session_id + ".csv", false))
                {
                    writer.WriteLine("Developer: " + _session_id);
                    foreach (List<Event> events in _eventLists)
                    {
                        cmds = 0;
                        docs = 0;
                        window = 0;
                        nav = 0;
                        other = 0;

                        writer.WriteLine("\t" + events[0].ToString());
                        for (int i = 1; i < events.Count; i++)
                        {
                            ev = events[i];
                            writer.WriteLine("\t\t" + ev.ToString());


                            if (ev is Command)
                                cmds++;
                            else if (ev is DocuemntCmd)
                                docs++;
                            else if (ev is WindowCmd)
                                window++;
                            else if (ev is NavCmd)
                                nav++;
                            else
                                other++;
                        }

                        summary = "CMD Summary\tsoulution commands: " + cmds.ToString() + "\tdoc commands: " + docs.ToString() + "\twindow commands: " + window.ToString() + "\tnavigation commands: " + nav.ToString() + "\tother commands: " + other.ToString();
                        _summaries.Add(summary);
                        writer.WriteLine(summary);
                        writer.Flush();
                    }
                }
            } 
        }
    }
}
