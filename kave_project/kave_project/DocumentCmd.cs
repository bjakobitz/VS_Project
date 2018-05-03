using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kave_project
{
    class DocuemntCmd : Event
    {
        private int _count;
        private String _docName;
        private String _activeDoc;

        public int count { get { return _count; } private set {} }
        public String docName { get { return _docName; } set { _docName = value; } }
        public String activeDoc { get { return _activeDoc; } set { _activeDoc = value; } }

        public DocuemntCmd(String name)
        {
            _name = name;
            _count = 1;
            _docName = "null";
            _activeDoc = "null";
        }

        public void incrementCount()
        {
            _count++;
        }

        public override String ToString()
        {
            return "executed\tdoc command\t" + name + "\tat\t" + triggeredAt +"\twith doc\t"+ _docName +"\tacitve doc\t"+ _activeDoc;
        }
    }
}
