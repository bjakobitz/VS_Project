using KaVE.Commons.Model.Events.CompletionEvents;
using KaVE.Commons.Utils.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kave_project
{
    class Completion : Event
    {
        private int _count;
        public CompletionEvent cEvent;

        public int count { get { return _count; } private set {} }
        
        public Completion(String name)
        {
            _name = name;
            _count = 1;
        }

        public void incrementCount()
        {
            _count++;
        }

        public int getSelectionCount()
        {
            return cEvent.Selections.Count;
        }

        public TimeSpan? getCompletionTime()
        {
            return cEvent.Duration;
        }

        public TerminationState getState()
        {
            return cEvent.TerminatedState;
        }

        public TimeSpan? getMedianSelectionTime()
        {
            IOrderedEnumerable<IProposalSelection> selections = cEvent.Selections.OrderBy(s=> s.SelectedAfter);

            return selections.ElementAt(selections.Count() / 2).SelectedAfter;
        }
    }
}
