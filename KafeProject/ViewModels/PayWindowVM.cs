using KafeProject.Date;
using KafeProject.Infrastructure.Commands;
using KafeProject.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KafeProject.ViewModels
{
    public class PayWindowVM : ClassForChane
    {
        private bool CanCloseApplicationExecat(object p) => true;
        MainWindow mainWindow;
        internal PayWindowVM()
        {
            ButtonForAutorization = new LambdaCommand(ClickButton, CanCloseApplicationExecat);
            ButtonForClear = new LambdaCommand(ClickClear, CanCloseApplicationExecat);
            DateL();

        }


        private async void DateL()
        {
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                }
            });

        }
        private string _Password = "";
        public string Password
        {
            get => _Password;
            set
            {
                Password2 = value;
                if (_Password.Length < 5)
                {
                    Set(ref _Password, value);

                    LoadDate();
                }

            }
        }

        private int _M = 0;
        public int M
        {
            get => _M;
            set => Set(ref _M, value);

        }

        private string _Password2 = "";
        public string Password2
        {
            get
            {
                return new string('*', _Password2.Length);
            }
            set
            {
                Set(ref _Password2, value);

            }
        }

        int Id = 0;
        private void ClickButton(object p)
        {

            if (p.ToString() == "Удалить")
                Password = "";

            if (p.ToString() != "Удалить")
                Password += p.ToString();

        }
        private void LoadDate()
        {
            if (Password.Length == 4 || Password.Length > 4)
            {
                //Thread.Sleep(1000);
                using (ApplicationContext db = new ApplicationContext())
                {

                    Id = db.Waiters.Where(t => t.Pass == Password).Select(t => t.Id).OrderBy(i=>i).FirstOrDefault();
                    if (Id > 0)
                    {
                        //Password = "";
                        //id = Id;
                        if (db.Regime.Where(i => i.WaiterId == Id && i.StartTime.Date >= DateTime.Now.Date).Count() == 0)
                        {
                            Regimes regime = new Regimes { StartTime = DateTime.Now, EndTime = DateTime.Now, WaiterId = Id };
                            db.Regime.Add(regime);
                            db.SaveChanges();
                        }
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
            if (Password.Length > 0)
                Password = Password[0..^1];
        }

    }
}