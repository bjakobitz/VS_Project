using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kave_project
{
    class Command
    {
        private String _name;
        private int _count;

        public String name { get { return _name; } private set {} }
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
    }
}
