using M.DataAccessLayer;
using M.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace M
{
    public class StudentLogic : IModel
    {
        //public IList<Student> Students = new List<Student>();
        public event EventHandler<StudentAddArgs> EventStudentAddModel = delegate { };
        public event EventHandler<StudentDeleteArgs> EventStudentDeleteModel = delegate { };

        private IRepository<Student> repository { set; get; }
        public StudentLogic(IRepository<Student> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Добавить нового студента.
        /// </summary>
        public void AddStudent(Student student)
        {
            repository.Create(student);
            repository.Save();
            EventStudentAddModel(this, new StudentAddArgs(student));
        }

        /// <summary>
        /// Удалить студента по идентификатору.
        /// </summary>
        public void DeleteStudent(int id)
        {
            int index = 0;
            List<Student> students = repository.GetAll().ToList();
            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].Id == id)
                    index = i;
            }
            repository.Delete(id);
            repository.Save();
            EventStudentDeleteModel(this, new StudentDeleteArgs(index));
        }

        public void DeleteStudent()
        {
            repository.DeleteAll();//смотри описание метода
            repository.Save();
        }

        /// <summary>
        /// Вывести весь список студентов.
        /// </summary>
        public List<Student> GetAll()
        {
            return repository.GetAll().ToList();
        }

        /// <summary>
        /// Библиотека специальностей и количества студентов
        /// </summary>
        public Dictionary<string, int> DistributionOfSpecialties()
        {
            Dictionary<string, int> specialtiesDistribution = new Dictionary<string, int>();

            foreach (Student student in (List<Student>)repository.GetAll())
            {
                if (specialtiesDistribution.ContainsKey(student.Speciality))
                    specialtiesDistribution[student.Speciality] += 1;

                else
                    specialtiesDistribution[student.Speciality] = 1;
            }
            return specialtiesDistribution;
        }

        /// <summary>
        /// Можно ли добавить этого студента?
        /// </summary>
        public bool IsCanAddStudent(Student student)
        {
            if (!string.IsNullOrEmpty(student.Name) && !string.IsNullOrEmpty(student.Speciality) && !string.IsNullOrEmpty(student.Group)) //можно добавить студента, если заполнены все поля
            {
                //foreach (var item in repository.GetAll())
                //{
                //    if (item.Name == student.Name && item.Speciality == student.Speciality && item.Group == student.Group)
                //        return false; //запрещает добавление тёсок
                //}
                return true;
            }
            return false;
        }
    }
}
