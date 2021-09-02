using KafeProject.Date;
using KafeProject.Models;
using KafeProject.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace KafeProject.ViewModels
{
    class ChecksLogic : ViewModel
    {
        private List<StandartAbstract> _checks = new List<StandartAbstract>();
        public List<StandartAbstract> checks
        {
            get => _checks;
            set => Set(ref _checks, value);
        }
        public StandartAbstract getCheck
        {
            set
            {
                int o = 0;
                using (ApplicationContext db = new ApplicationContext())
                {
                    o = db.Checks.Where(b => b.CheckCount == value.Id && b.WaiterId == MainWindow.Id && b.DateTimeCheck > DateTime.Now.Date).Select(p => p.Id).OrderBy(h => h).LastOrDefault();
                }
                //MessageBox.Show(value.Name + " " + value.Id) ;
                menuStol_(o);
                MainWindow.t = 0;
                MainWindow.timer.Start();
                cls();
            }
        }
        public delegate void SetCheck(int id);
        public static event SetCheck menuStol_;
        public delegate void closeCheck(int o = 0);
        public static event closeCheck cls;
        public ChecksLogic()
        {
            using ApplicationContext db = new ApplicationContext();
            var c = db.Checks.
                Where(b => b.WaiterId == MainWindow.Id && b.DateTimeCheck > DateTime.Now.Date && b.Status == 5).
                Select(p => p).
                OrderBy(a => a);
            if (c != null)
                foreach (Check t in c)
                {
                    if (t.TableId != 0)
                        checks.Add(new StandartAbstract { Name = $"номер чека '{t.CheckCount}' номер стола '{getTable(t)}'", Id = t.CheckCount });
                    else
                        checks.Add(new StandartAbstract { Name = $"номер чека '{t.CheckCount}' номер стола 'c собой'", Id = t.CheckCount });
                }
        }
        string getTable(Check t)
        {
            using ApplicationContext db = new ApplicationContext();
            return db.Tables.Where(i => i.Id == t.TableId).Select(i => i.Name).OrderBy(i => i).LastOrDefault();
        }
    }
}
