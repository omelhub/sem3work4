using M;
using M.EventArgs;
using Ninject;
using P;
using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using View = System.Windows.Forms.View;

namespace WindowsFormsView
{
    public partial class Form1 : Form, IView
    {
        public Form1()
        {
            InitializeComponent();
            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());
            studentModel = ninjectKernel.Get<StudentLogic>();
            presenter = new PresenterStudent(this, studentModel);

            //тестовый список студентов
            studentModel.AddStudent(new Student { Name = "Иванов", Speciality = "Информатика", Group = "КИ21-18Б" });
            //studentModel.AddStudent("Петров", "Информатика", "КИ21-21Б");
            //studentModel.AddStudent("Сидоров", "Информатика", "КИ21-21Б");
            //studentModel.AddStudent("Лагойда", "Информатика", "КИ21-21Б");
            //studentModel.AddStudent("Машкова", "Биология", "КИ21-01А");
            //studentModel.AddStudent("Викторова", "Биология", "КИ21-02А");
        }

        public event EventHandler<StudentAddArgs> EventStudentAddView = delegate { };
        public event EventHandler<StudentDeleteArgs> EventStudentDeleteView = delegate { };
        private PresenterStudent presenter;
        public IModel studentModel;

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshListView();
        }

        public void AddStudent(Student student)
        {
            ListViewItem newitem = new ListViewItem(student.Name);
            newitem.SubItems.Add(student.Group);
            newitem.SubItems.Add(student.Speciality);
            listView1.Items.Add(newitem);
        }

        public void DeleteStudent(int index)
        {
            listView1.Items.RemoveAt(index);
        }

        public void RefreshListView()
        {
            listView1.Clear();

            listView1.View = View.Details;

            listView1.Columns.Add("ФИО", 210);
            listView1.Columns.Add("Cпециальность", 100);
            listView1.Columns.Add("Группа", 100);

            foreach (Student student in studentModel.GetAll())
            {
                ListViewItem newitem = new ListViewItem(student.Name);
                newitem.SubItems.Add(student.Speciality);
                newitem.SubItems.Add(student.Group);

                listView1.Items.Add(newitem);
            }
        }

        public void RefreshGraph()
        {
            if (Application.OpenForms.OfType<Form2>().Count() == 1)
            {
                Application.OpenForms.OfType<Form2>().First().Close();

                Form2 newForm2 = new Form2(studentModel);

                newForm2.Show();
            }
        }

        #region Обработчики событий

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Student student = new Student() { Name = NameBox.Text.Trim(), Group = GroupBox.Text.Trim(), Speciality = SpecialityBox.Text.Trim() };
            if (studentModel.IsCanAddStudent(student))
            {
                EventStudentAddView(this, new StudentAddArgs(student));
                RefreshGraph();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count != 0)
            {
                //foreach (int index in listView1.SelectedIndices)
                //{
                //    List<int> ids = new List<int>();
                //    ids.Add(studentModel.GetAll().ElementAt(index).Id);
                //    foreach (int id in ids)
                //        studentModel.DeleteStudent(id);
                //} //код, который можно было бы использовать, если бы был включен multiselection

                EventStudentDeleteView(this, new StudentDeleteArgs(listView1.SelectedIndices[0]));

                RefreshGraph();
            }
        }

        private void buttonViewGraph_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Form2>().Count() == 1)
            {
                Application.OpenForms.OfType<Form2>().First().Close();
            }

            Form2 newForm2 = new Form2(studentModel);

            newForm2.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
