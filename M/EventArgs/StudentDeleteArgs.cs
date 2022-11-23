using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M.EventArgs
{
    public class StudentDeleteArgs : System.EventArgs
    {
        public int Index { get; set; }

        public StudentDeleteArgs(int index)
        {
            Index = index;
        }
    }
}
