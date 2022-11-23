using M;
using M.EventArgs;
using Ninject;
using P;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleView
{
    public class Program : IView
    {
        static void Commands()
        {
            Console.WriteLine("Список команд:");
            Console.WriteLine(" 1  - добавить студента,");
            Console.WriteLine(" 2  - удалить студента по идентификатору,");
            Console.WriteLine(" 3  - вывести список студентов,");
            Console.WriteLine("Esc - выйти.");
        }
        public static event EventHandler<StudentAddArgs> EventStudentAddView = delegate { };
        public static event EventHandler<StudentDeleteArgs> EventStudentDeleteView = delegate { };
        private PresenterStudent presenter;
        public static IModel studentModel;

        static void Main(string[] args)
        {
            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());
            studentModel = ninjectKernel.Get<StudentLogic>();



            //тестовый список студентов
            studentModel.AddStudent(new Student { Name = "Петров", Speciality = "Информатика", Group = "КИ21-21Б" });

            bool x = true;
            while (x)
            {
                Commands();
                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.D1:
                        AddStudentCommand();
                        break;
                    case ConsoleKey.D2:
                        DeleteStudentCommand();
                        break;
                    case ConsoleKey.D3:
                        ShowStudentsCommand();
                        break;
                    case ConsoleKey.Escape:
                        x = false;
                        break;


                    case ConsoleKey.D5:
                        studentModel.DeleteStudent(); //удаление всех студентов (сброс счётчика работает ток в Dapper)
                        break;
                }
                Console.WriteLine();
            }
            void AddStudent(Student student)
            {
                if (studentModel.IsCanAddStudent(student))
                {
                    studentModel.AddStudent(student);
                    EventStudentAddView(this, new StudentAddArgs(student));
                    ShowStudentsCommand();
                }
            }

            void AddStudentCommand()
            {
                Console.WriteLine("\nВведите Имя Специальность Группу студента через пробел:");
                string[] NewStudent = Console.ReadLine().Split();
                Student student = new Student { Name = NewStudent[0].Trim(), Speciality = NewStudent[1].Trim(), Group = NewStudent[2].Trim() };
                AddStudent(student);
            }

            void DeleteStudentCommand()
            {
                Console.WriteLine("\nВведите идентификатор студента:");
                if ((Int32.TryParse(Console.ReadLine(), out int result)) && result > 0 && studentModel.GetAll().Exists(y => y.Id == result))//x => x.Id == result)
                    studentModel.DeleteStudent(result);
                else
                    Console.WriteLine("Идентификатор введен неправильно.");
            }

            void ShowStudentsCommand()
            {
                Console.WriteLine($"\n\n{"Имя",-30} {"| Специальность",-30} {"| Группа",-20} {"| Id",-20}");
                Console.WriteLine(new string('-', 100));
                var allStudents = studentModel.GetAll();
                foreach (var student in allStudents)
                {
                    Console.WriteLine($"{student.Name,-30} {student.Speciality,-30} {student.Group,-20} {student.Id,-20}");
                }
            }
        }
    }
}
