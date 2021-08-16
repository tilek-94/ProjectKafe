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
                    o=db.Checks.Where(b => b.CheckCount == value.Id && b.WaiterId == MainWindow.Id && b.DateTimeCheck > DateTime.Now.Date).Select(p => p.Id).OrderBy(h => h).LastOrDefault();
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
        public delegate void closeCheck(int o=0);
        public static event closeCheck cls;
        public ChecksLogic()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var c = db.Checks.
                    Where(b => b.WaiterId == MainWindow.Id && b.DateTimeCheck > DateTime.Now.Date && b.Status == 5).
                    Select(p =>  "номер чека № " + p.CheckCount + " \tномер стола № " + db.Tables.
                         Where(t => t.Id == p.TableId).
                         Select(g => g.Name).
                         OrderBy(a => a).
                     LastOrDefault()).
                    OrderBy(a => a);
                if(c!=null)
                    foreach (var t in c)
                        checks.Add(new StandartAbstract {Name=t.ToString(),Id=Convert.ToInt32(t.Split()[3])});
            }
        }
    }
}
