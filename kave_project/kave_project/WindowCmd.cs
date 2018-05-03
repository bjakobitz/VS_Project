using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kave_project
{
    class WindowCmd : Event
    {
        private int _count;
        private String _windowName;

        public int count { get { return _count; } private set {} }
        public String windowName { get { return _windowName; } set { _windowName = value; } }
        
        public WindowCmd(String name)
        {
            _name = name;
            _count = 1;
            _windowName = "null";
        }

        public void incrementCount()
        {
            _count++;
        }

        public override String ToString()
        {
            return "executed\twindow command\t" + name + "\tat\t" + triggeredAt + "\twindow\t"+_windowName;
        }
    }
}
