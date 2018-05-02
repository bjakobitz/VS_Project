using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kave_project
{
    class Command : Event
    {
        private int _count;

        public int count { get { return _count; } private set {} }
        
        public Command(String name)
        {
            _name = name;
            _count = 1;
        }

        public void incrementCount()
        {
            _count++;
        }

        public override String ToString()
        {
            return "executed command: " + name + " at " + triggeredAt;
        }
    }
}
