using WindowsFormsView;
using System;
using System.Windows.Forms;
using M;
using Ninject;

namespace Presenter
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());

            IView view = InitWinFormsView();
            // IView view = InitXamarinForms();
            // IView view = InitAvaloniaUI();
            // IView view = InitMaui();
            // etc

            new PresenterStudent(view, ninjectKernel.Get<StudentLogic>());

            RunWinFormsView(view);
            // RunXamarinForms();
            // RunAvaloniaUI();
            // RunMaui();
            // etc
        }

        private static IView InitWinFormsView()
        {
            ThisConsole.Hide();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            return new Form1();
        }

        private static void RunWinFormsView(IView view)
        {
            Application.Run((Form)view);
        }
    }
}
