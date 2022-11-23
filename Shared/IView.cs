using M;
using M.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public interface IView
    {
        void AddStudent(Student student);
        void DeleteStudent(int index);
        event EventHandler<StudentAddArgs> EventStudentAddView;
        event EventHandler<StudentDeleteArgs> EventStudentDeleteView;
    }
}
