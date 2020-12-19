using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MF
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MyApplicationContext(() => new Auth()));
        }
            catch (Exception)
            {
                MessageBox.Show(text: "Произошла ошибка.", caption: "Ошибка", buttons: MessageBoxButtons.OK, icon: MessageBoxIcon.Error);
            }
}
        public class MyApplicationContext : ApplicationContext
        {
            public MyApplicationContext(Func<Form> formFactory)
            {
                Form startupForm = formFactory();
                startupForm.FormClosed += OnFormClosed;
                startupForm.Show();
            }

            private void OnFormClosed(object sender, FormClosedEventArgs e)
            {
                if (Application.OpenForms.Count > 0)
                {
                    foreach (Form form in Application.OpenForms)
                    {
                        form.FormClosed -= OnFormClosed;
                        form.FormClosed += OnFormClosed;
                    }
                }
                else ExitThread();
            }
        }
    }
}
