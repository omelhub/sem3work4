using SharedEventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M
{
    public interface IModel
    {
        event EventHandler<StudentAddArgs> EventStudentAddModel;
        event EventHandler<StudentDeleteArgs> EventStudentDeleteModel;

        void AddStudent(Student student);

        void DeleteStudent(int id);

        void DeleteStudent();

        List<Student> GetAll();
    }
}
