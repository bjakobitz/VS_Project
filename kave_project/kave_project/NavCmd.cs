using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kave_project
{
    class NavCmd : Event
    {
        private int _count;
        private String _target;

        public int count { get { return _count; } private set {} }
        public String target { get { return _target; } set { _target = value; } }
        
        public NavCmd(String name)
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
            return "executed\tcommand\t" + name + "\tat\t" + triggeredAt + "\ttarget\t" + target;
        }
    }
}
