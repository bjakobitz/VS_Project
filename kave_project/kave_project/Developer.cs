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
        // completion info
        public List<Completion> completions;
        public TimeSpan max_time;
        public TimeSpan min_time;
        public TimeSpan avg_time;
        public TimeSpan median_time;
        public int total_completions;
        public int applied_completions;
        public int canceled_completions;
        public int filtered_completions;
        public TimeSpan total_time;
        public TimeSpan total_span;

        public TimeSpan total_canceled;
        public TimeSpan total_approved;
        public TimeSpan total_filtered;

        public String session_id { get { return _session_id; } private set { } }
        public List<List<Event>> eventsLists { get { return _eventLists; } private set { } }
        public List<String> summaries { get { return _summaries; } private set { } }

        public Developer(String session_id)
        {
            _session_id = session_id;
            _eventLists = new List<List<Event>>();
            completions = new List<Completion>();
        }

        public void runStats()
        {
            findTotals();
            findTimeSpan();
            findMax();
            findMedian();
            findMin();
            findAvg();
        }

        private void findTotals()
        {
            total_completions = 0;
            applied_completions = 0;
            canceled_completions = 0;
            filtered_completions = 0;
            total_approved = new TimeSpan(0, 0, 0);
            total_canceled = new TimeSpan(0, 0, 0);
            total_filtered = new TimeSpan(0, 0, 0);
            total_time = new TimeSpan(0, 0, 0);


            foreach (Completion comp in completions)
            {
                total_completions++;
                total_time = total_time.Add(comp.cEvent.Duration.Value);
                if (comp.getState() == KaVE.Commons.Model.Events.CompletionEvents.TerminationState.Applied)
                {
                    applied_completions++;
                    total_approved = total_approved.Add(comp.cEvent.Duration.Value);
                }
                if (comp.getState() == KaVE.Commons.Model.Events.CompletionEvents.TerminationState.Cancelled)
                {
                    canceled_completions++;
                    total_canceled = total_canceled.Add(comp.cEvent.Duration.Value);
                }
                if (comp.getState() == KaVE.Commons.Model.Events.CompletionEvents.TerminationState.Filtered)
                {
                    filtered_completions++;
                    total_filtered = total_filtered.Add(comp.cEvent.Duration.Value);
                }
            }
        }
        private void findMax()
        {
            max_time = completions.Max(c => c.getCompletionTime().Value);
        }

        private void findMin()
        {
            min_time = completions.Min(c => c.getCompletionTime().Value);
        }

        private void findAvg()
        {
            avg_time = new TimeSpan(0,0,0,0,Convert.ToInt32(completions.Average(c => Convert.ToDecimal(c.getCompletionTime().Value.Milliseconds))));
        }

        private void findMedian()
        {
            IOrderedEnumerable<Completion> times = completions.OrderBy(c => c.getCompletionTime());

            median_time = times.ElementAt(times.Count() / 2).getCompletionTime().Value;
        }

        private void findTimeSpan()
        {
            total_span = completions[completions.Count-1].cEvent.TriggeredAt.Value.Subtract(completions[0].cEvent.TriggeredAt.Value);
        }

        public void addEvent(Completion vsEvent)
        {
            List<Event> eventList;

            completions.Add(vsEvent);
            
            

            //switch (vsEvent.name)
            //{
            //    case "Collapse All":
            //        goto case "Home";
            //    case "Show All Files":
            //        goto case "Home";
            //    case "Sync with Active Document (Ctrl+[, S)":
            //        goto case "Home";
            //    case "Home":
            //        eventList = new List<Event>();
            //        eventList.Add(vsEvent);
            //        _eventLists.Add(eventList);
            //        break;
            //    default:
            //        if (_eventLists.Count > 0)
            //        {
            //            eventList = _eventLists[_eventLists.Count - 1];
            //            eventList.Add(vsEvent);
            //        }
            //        break;
                    
            //}
        }

        public void writeEvents(String dir)
        {
            using (StreamWriter writer = new StreamWriter(dir + "\\" + _session_id + ".csv", false))
            {
                foreach (Completion ev in completions)
                {
                    writer.WriteLine(" Completion count " +((Completion)ev).getSelectionCount().ToString());
                }

                writer.WriteLine("Completions\t" + "Total\t" + total_completions.ToString() + "\tApplied\t" + applied_completions.ToString() + " "+ total_approved.ToString(@"hh\:mm\:ss\:fff") + "\tCanceled\t" + canceled_completions.ToString() + " "+total_canceled.ToString(@"hh\:mm\:ss\:fff") + "\tFiltered\t"+filtered_completions.ToString() + " " + total_filtered.ToString(@"hh\:mm\:ss\:fff")); 
                writer.WriteLine("Times:\t" + "Min\t" + min_time.ToString(@"hh\:mm\:ss\:fff") + "\tMedian\t" + median_time.ToString(@"hh\:mm\:ss\:fff") + "\tMax\t " + max_time.ToString(@"hh\:mm\:ss\:fff") + "\tAvg\t" + avg_time.ToString(@"hh\:mm\:ss\:fff"));
                writer.WriteLine("Dev Time\t" + total_span.ToString(@"hh\:mm\:ss\:fff"));
            }


            //Event ev;
            //int cmds;
            //int docs;
            //int window;
            //int nav;
            //int other;
            //String summary;
            //_summaries = new List<string>();

            //if (eventsLists.Count > 0)
            //{
            //    using (StreamWriter writer = new StreamWriter(dir + "\\" + _session_id + ".csv", false))
            //    {
            //        writer.WriteLine("Developer: " + _session_id);
            //        foreach (List<Event> events in _eventLists)
            //        {
            //            cmds = 0;
            //            docs = 0;
            //            window = 0;
            //            nav = 0;
            //            other = 0;

            //            writer.WriteLine("\t" + events[0].ToString());
            //            for (int i = 1; i < events.Count; i++) // the next 20 commands after a solution nav command
            //            {
            //                ev = events[i];
            //                writer.WriteLine("\t\t" + ev.ToString());


            //                if (ev is Command)
            //                    cmds++;
            //                else if (ev is DocuemntCmd)
            //                    docs++;
            //                else if (ev is WindowCmd)
            //                    window++;
            //                else if (ev is NavCmd)
            //                    nav++;
            //                else
            //                    other++;
            //            }

            //            summary = "CMD Summary\tsoulution commands: " + cmds.ToString() + "\tdoc commands: " + docs.ToString() + "\twindow commands: " + window.ToString() + "\tnavigation commands: " + nav.ToString() + "\tother commands: " + other.ToString();
            //            _summaries.Add(summary);
            //            writer.WriteLine(summary);
            //            writer.Flush();
            //        }
            //    }
            //} 
        }
    }
}
