using M;
using M.EventArgs;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P
{
    public class PresenterStudent
    {
        private IView view = null;

        private IModel model = null;

        public PresenterStudent(IView studentView, IModel studentModel)
        {
            view = studentView;
            view.EventStudentAddView += view_StudentAdd;
            view.EventStudentDeleteView += view_StudentDelete;

            model = studentModel;
            model.EventStudentAddModel += model_StudentAdd;
            model.EventStudentDeleteModel += model_StudentDelete;
        }

        public void view_StudentAdd(object sender, StudentAddArgs e)
        {
            model.AddStudent(e.Student);
        }

        public void view_StudentDelete(object sender, StudentDeleteArgs e)
        {
            //model.DeleteStudent(e.Id);
            model.DeleteStudent(model.GetAll().ElementAt(e.Index).Id);
        }

        public void model_StudentAdd(object sender, StudentAddArgs e)
        {
            view.AddStudent(e.Student);
        }

        public void model_StudentDelete(object sender, StudentDeleteArgs e)
        {
            view.DeleteStudent(e.Index);
        }
    }
}
