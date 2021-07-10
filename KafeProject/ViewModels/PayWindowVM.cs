using KafeProject.Infrastructure.Commands;
using KafeProject.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KafeProject.ViewModels
{
    public class PayWindowVM:ClassForChane
    {
        
        
        private bool CanCloseApplicationExecat(object p) => true;
        MainWindow mainWindow;
        internal PayWindowVM()
        {
            ButtonForAutorization = new LambdaCommand(ClickButton, CanCloseApplicationExecat);
            ButtonForClear = new LambdaCommand(ClickClear, CanCloseApplicationExecat);
            LoadBase();
        }

        private async void LoadBase()
        {
            await Task.Run(() => DateL());
        }

        private void DateL()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
            }
        }
        private void ClickButton(object p)
        {
            int Id = 0;
            if (p.ToString() == "Удалить")
                Password = "";

            if (p.ToString() != "Удалить")
                Password += p.ToString();

            if (Password.Length == 4 || Password.Length >4)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                     Id = db.Waiters.Where(t => t.Pass == Password).Select(t => t.Id).FirstOrDefault();
                    if (Id>0)
                    {
                        Password = "";
                        
                        mainWindow = new MainWindow(Id);
                        mainWindow.Show();
                        CloseAction();
                    }
                    Password = "";

                }

            }
            
        }
        private void ClickClear(object p)
        {
            if(Password.Length>0)
                Password = Password.Substring(0, Password.Length - 1);
        }

    }
}
