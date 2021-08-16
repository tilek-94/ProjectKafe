using KafeProject.All_Windows;
using KafeProject.Date;
using KafeProject.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace KafeProject.ViewModels
{
    class ArchiveCheckViewModel : Base.ViewModel
    {
        private ObservableCollection<ACheck> _CheckList = new ObservableCollection<ACheck>();
        public ObservableCollection<ACheck> CheckList
        {
            get { return _CheckList; }
            set { Set(ref _CheckList, value); }
        }
        private ObservableCollection<FoodClass> _CheckItems = new ObservableCollection<FoodClass>();
        public ObservableCollection<FoodClass> CheckItems
        {
            get { return _CheckItems; }
            set { Set(ref _CheckItems, value); }
        }
        private double itog = 0;
        public double Itog
        {
            get
            {
                if ((itog + ob) % 1 > 0.4)
                    return Convert.ToInt32(itog + ob + 0.9);
                else return itog + ob;
            }
            set
            {
                Set(ref itog, value);
            }
        }
        private double ob = 0;
        public double Ob
        {
            get
            {
                if (ob == 0) return 0;
                double x = ob;
                using (ApplicationContext db = new ApplicationContext())
                {
                    if (SelectedCheck.CheckTable != "0")
                    {
                        //x = db.Orders.Where(i => i.CheckId == Convert.ToInt32(_SelectedCheck.IdCheck)).Select(h => db.Foods.Where(l => l.Id == h.FoodId).OrderBy(o => o.Id).Select(n => n.Price).LastOrDefault() * h.CountFood).OrderBy(p => p).Sum();
                        var st = db.Waiters.Where(i => i.Id == MainWindow.Id).OrderBy(l => l.Id).LastOrDefault();
                        if (st != null)
                        {
                            if (st.SalaryType == "Service")
                            {
                                x = x + Convert.ToInt32(_SelectedCheck.CheckStatus);
                            }
                            else if (st.SalaryType == "Percent")
                            {
                                x = x + (x / 100 * st.Salary);
                            }
                        }
                    }
                }
                ob = x - itog;
                double k = itog;
                Itog = 0;
                Itog = k;
                //MessageBox.Show(ob + ""+Itog);
                return Math.Round(ob, 2);
            }
            set { Set(ref ob, itog); }
        }
        private ACheck _SelectedCheck = new ACheck();
        public ACheck SelectedCheck
        {
            get { return _SelectedCheck; }
            set
            {
                if (value != null)
                {
                    Set(ref _SelectedCheck, value);
                    Itog = 0;
                    Ob = 0;
                    CreateCheck(Convert.ToInt32(value.IdCheck));
                    _SelectedCheck = value;
                }
                else
                {
                    _SelectedCheck = new ACheck();
                }
                //if (!Set(ref _SelectedCheck, value)) return;
                //_SelectedChecks.Source = value?.FoodInCheck;
                //OnPropertyChanged(nameof(SelectedChecks));
            }
        }
        void CreateCheck(int id)
        {
            Itog = 0;
            Ob = 0;
            CheckItems = new ObservableCollection<FoodClass>();
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var h in db.Orders.Where(g => g.CheckId == id).OrderBy(o => o.Id))
                {
                    double x = priceF(h.FoodId);
                    if (h.isCancel == 0)
                    {
                        CheckItems.Add(new FoodClass { Count = h.CountFood, Price = x, Itog = Convert.ToInt32(x * h.CountFood), Name = nameF(h.FoodId), Otmena = "" });
                    }
                    else 
                    {
                        CheckItems.Add(new FoodClass { Count = h.CountFood, Price = x, Itog = Convert.ToInt32(x * h.CountFood), Name = nameF(h.FoodId), Otmena = "Отменнено" });
                    }
                    Itog += x * h.CountFood;
                }
            }
            Ob = 0;
        }
        //private readonly CollectionViewSource _SelectedChecks = new CollectionViewSource();
        //public ICollectionView SelectedChecks => _SelectedChecks?.View;
        double priceF(int h)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Foods.Where(k => k.Id == h).OrderBy(p => p).LastOrDefault().Price;
            };
        }
        string nameF(int h)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Foods.Where(k => k.Id == h).OrderBy(p => p).LastOrDefault().Name;
            }
        }
        public ArchiveCheckViewModel()
        {

            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var t in db.Checks.Where(g => g.WaiterId == MainWindow.Id && g.DateTimeCheck > DateTime.Now.Date).OrderBy(i => i.Id))
                    CheckList.Add(new ACheck { IdCheck = t.Id.ToString(), CheckID = t.CheckCount, CheckTable = t.TableId.ToString(), CheckPrice = checksPrise(t.Id, t.GuestsCount), CheckStatus = t.GuestsCount.ToString() });
            }
            Chasy.getDate += changeDate;
            MainWindow.clsd += (t)=> Chasy.getDate -= changeDate; 
        }
        ~ArchiveCheckViewModel() => Chasy.getDate -= changeDate;
        string checksPrise(int id, int count)
        {
            double x = 0;
            using (ApplicationContext db = new ApplicationContext())
            {
                
                x = db.Orders.Where(i => i.CheckId == id && i.isCancel == 0).Select(h => db.Foods.Where(l => l.Id == h.FoodId).OrderBy(o => o.Id).Select(n => n.Price).LastOrDefault() * h.CountFood).OrderBy(p => p).Sum();
                if (db.Checks.Where(i => i.Id == id).Select(i => i.TableId).OrderBy(i => i).LastOrDefault() == 0)
                    return x.ToString();
                var st = db.Waiters.Where(i => i.Id == MainWindow.Id).OrderBy(l => l.Id).LastOrDefault();
                if (st != null)
                {
                    if (st.SalaryType == "Service")
                    {
                        x = x + count;
                    }
                    else if (st.SalaryType == "Percent")
                    {
                        x = Convert.ToInt32(x + (x / 100 * st.Salary));
                    }
                }
            }

            return x.ToString();
        }
        void changeDate(DateTime i)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                CheckList = new ObservableCollection<ACheck>();
                foreach (var t in db.Checks.Where(g => g.WaiterId == MainWindow.Id && g.DateTimeCheck.Date == i.Date).OrderBy(i => i.Id))
                    CheckList.Add(new ACheck { IdCheck = t.Id.ToString(), CheckID = t.CheckCount, CheckTable = t.TableId.ToString(), CheckPrice = checksPrise(t.Id, t.GuestsCount), CheckStatus = t.GuestsCount.ToString() });
                CheckList = new ObservableCollection<ACheck>(CheckList.OrderBy(i => i.IdCheck));
            }

        }
    }
}
