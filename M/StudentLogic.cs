using M.DataAccessLayer;
using SharedEventArgs;
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
            EventStudentAddModel(this, new StudentAddArgs((student.Name, student.Speciality, student.Group)));
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
    }
}
