using SharedEventArgs;
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
using System.Xml.Linq;
using M;
using System.Reflection;
using System.Globalization;

namespace WindowsFormsView
{
    public partial class Form1 : Form, IView
    {
        public Form1()
        {
            InitializeComponent();
        }

        public event EventHandler<StudentAllArgs> EventStudentView = delegate { };
        public event EventHandler<StudentAddArgs> EventStudentAddView = delegate { };
        public event EventHandler<StudentDeleteArgs> EventStudentDeleteView = delegate { };

        private void Form1_Load(object sender, EventArgs e)
        {
            RefreshListView();

            //тестовый список студентов
            EventStudentAddView.Invoke(this, new StudentAddArgs(("Иванов", "Английский", "УБ21-18Б")));
            EventStudentAddView.Invoke(this, new StudentAddArgs(("Машкова", "Математика", "МФ21-18Б")));
        }

        public void AddStudent((string, string, string) studentInfo)
        {
            ListViewItem newitem = new ListViewItem(studentInfo.Item1);
            newitem.SubItems.Add(studentInfo.Item2);
            newitem.SubItems.Add(studentInfo.Item3);
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

            EventStudentView.Invoke(this, new StudentAllArgs());
        }

        public void RefreshGraph()
        {
            if (Application.OpenForms.OfType<Form2>().Count() == 1)
            {
                Application.OpenForms.OfType<Form2>().First().Close();

                Form2 newForm2 = new Form2(listView1.Items);

                newForm2.Show();
            }
        }

        #region Обработчики событий

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var (name, group, speciality) = (NameBox.Text.Trim(), GroupBox.Text.Trim(), SpecialityBox.Text.Trim());
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(speciality) && !string.IsNullOrEmpty(group))
            {
                EventStudentAddView.Invoke(this, new StudentAddArgs((name, speciality, group)));
                RefreshGraph();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count != 0)
            {
                int[] indices = new int[listView1.SelectedIndices.Count];
                listView1.SelectedIndices.CopyTo(indices,0);

                for (int i = 0; i < indices.Length; i++)
                {
                    for (int j = i + 1; j < indices.Length; j++)
                    {
                        if (indices[j] > indices[i])
                        {
                            indices[j] -= 1;
                        }
                    }
                    EventStudentDeleteView.Invoke(this, new StudentDeleteArgs(indices[i]));
                }
                RefreshGraph();
            }
        }

        private void buttonViewGraph_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<Form2>().Count() == 1)
            {
                Application.OpenForms.OfType<Form2>().First().Close();
            }

            Form2 newForm2 = new Form2(listView1.Items);

            newForm2.Show();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e){}

        #endregion
    }
}
