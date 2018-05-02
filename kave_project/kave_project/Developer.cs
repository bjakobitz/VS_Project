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

        public String session_id { get { return _session_id; } private set { } }
        public List<List<Event>> eventsLists { get { return _eventLists; } private set { } }

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

        public void writeEvents(StreamWriter writer)
        {
            Event ev;

            writer.WriteLine("Developer: " + _session_id);
            foreach (List<Event> events in _eventLists)
            {
                writer.WriteLine("\t"+events[0].ToString());
                for (int i = 1; i < events.Count; i++)
                {
                    ev = events[i];
                    writer.WriteLine("\t\t"+ev.ToString());
                }
            }
        }
    }
}
