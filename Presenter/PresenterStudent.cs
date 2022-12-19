using M;
using SharedEventArgs;
using System.Linq;
using WindowsFormsView;

namespace Presenter
{
    public class PresenterStudent
    {
        private IView view = null;

        private IModel model = null;

        public PresenterStudent(IView studentView, IModel studentModel)
        {
            view = studentView;
            view.EventStudentView += view_StudentAll;
            view.EventStudentAddView += view_StudentAdd;
            view.EventStudentDeleteView += view_StudentDelete;

            model = studentModel;
            model.EventStudentAddModel += model_StudentAdd;
            model.EventStudentDeleteModel += model_StudentDelete;
        }

        public void view_StudentAdd(object sender, StudentAddArgs e) => model.AddStudent(new Student() { Name = e.StudentInfo.Item1, Speciality = e.StudentInfo.Item2, Group = e.StudentInfo.Item3 });

        public void view_StudentDelete(object sender, StudentDeleteArgs e) => model.DeleteStudent(model.GetAll().ElementAt(e.Index).Id);

        public void view_StudentAll(object sender, StudentAllArgs e)
        {
            foreach (Student student in model.GetAll())
            {
                view.AddStudent( (student.Name, student.Speciality, student.Group) );
            }
        }

        public void model_StudentAdd(object sender, StudentAddArgs e) => view.AddStudent(e.StudentInfo);

        public void model_StudentDelete(object sender, StudentDeleteArgs e) => view.DeleteStudent(e.Index);
    }
}
