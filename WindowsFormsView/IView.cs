using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedEventArgs;

namespace WindowsFormsView
{
    public interface IView
    {
        event EventHandler<StudentAllArgs> EventStudentView;
        event EventHandler<StudentAddArgs> EventStudentAddView;
        event EventHandler<StudentDeleteArgs> EventStudentDeleteView;

        void AddStudent((string, string, string) studentInfo);
        void DeleteStudent(int index);
    }
}
