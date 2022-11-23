using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M.EventArgs
{
    public class StudentAddArgs : System.EventArgs
    {
        public Student Student { get; set; }

        public StudentAddArgs(Student student)
        {
            Student = student;
        }
    }
}
