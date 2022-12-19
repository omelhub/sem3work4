using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedEventArgs
{
    public class StudentAddArgs : System.EventArgs
    {
        public (string, string, string) StudentInfo { get; }

        public StudentAddArgs((string, string, string) studentInfo)
        {
            StudentInfo = studentInfo;
        }
    }
}
