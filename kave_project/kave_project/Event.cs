using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kave_project
{
    class Event
    {
        protected String _name;
        protected DateTimeOffset? _triggeredAt;

        public String name { get { return _name; } private set { } }
        public DateTimeOffset? triggeredAt { get { return _triggeredAt; } set { _triggeredAt = value; } }

        protected Event() { }
        public Event(String name)
        {
            _name = name;
        }

        
        public override String ToString()
        {
            return "executed event: " + name + " at " + triggeredAt;
        }
    }
}
