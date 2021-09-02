using KafeProject.All_Windows;
using KafeProject.Date;
using KafeProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

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
                if ((itog + ob) % 1 > 0.1)
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
                    if (SelectedCheck.CheckTable != "0" || SelectedCheck.CheckTable != "Без услуги")
                    {
                        //x = db.Orders.Where(i => i.CheckId == Convert.ToInt32(_SelectedCheck.IdCheck)).Select(h => db.Foods.Where(l => l.Id == h.FoodId).OrderBy(o => o.Id).Select(n => n.Price).LastOrDefault() * h.CountFood).OrderBy(p => p).Sum();
                        var st = db.Waiters.Where(i => i.Id == MainWindow.Id).OrderBy(l => l.Id).LastOrDefault();
                        if (st != null)
                        {
                            if (st.SalaryType == "Service")
                            {
                                x += Convert.ToInt32(_SelectedCheck.CheckStatus);
                            }
                            else if (st.SalaryType == "Percent")
                            {
                                x += (x / 100 * st.Salary);
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
                    CreateCheckAsync(Convert.ToInt32(value.IdCheck));
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

        async void CreateCheckAsync(int id) => await Task.Run(() => { CreateCheck(id); });

        void CreateCheck(int id)
        {
            Itog = 0;
            Ob = 0;
            CheckItems = new ObservableCollection<FoodClass>();
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var h in db.Orders.Where(g => g.CheckId == id).OrderBy(o => o.Id))
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        double x = priceF(h.FoodId);
                        if (h.isCancel == 0)
                        {
                            if (h.isGramm > 0)
                            {
                                CheckItems.Add(new FoodClass { Count = h.CountFood, Price = (x * h.isGramm / 100), Itog = Convert.ToInt32((x * h.isGramm / 100) * h.CountFood), Name = nameF(h.FoodId) + $"({h.isGramm})грамм", Otmena = "" });
                                Itog += (x * h.isGramm / 100) * h.CountFood;
                            }
                            else
                            {
                                CheckItems.Add(new FoodClass { Count = h.CountFood, Price = x, Itog = Convert.ToInt32(x * h.CountFood), Name = nameF(h.FoodId), Otmena = "" });
                                Itog += x * h.CountFood;
                            }
                        }
                        else
                        {
                            if (h.isGramm > 0)
                            {
                                CheckItems.Add(new FoodClass { Count = h.CountFood, Price = (x * h.isGramm / 100), Itog = Convert.ToInt32((x * h.isGramm / 100) * h.CountFood), Name = nameF(h.FoodId) + $"({h.isGramm})грамм", Otmena = "Отменнено" });
                            }
                            else
                            {
                                CheckItems.Add(new FoodClass { Count = h.CountFood, Price = x, Itog = Convert.ToInt32(x * h.CountFood), Name = nameF(h.FoodId), Otmena = "Отменнено" });

                            }
                        }
                    });

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
            using ApplicationContext db = new ApplicationContext();
            return db.Foods.Where(k => k.Id == h).OrderBy(p => p).LastOrDefault().Name;
        }
        public ArchiveCheckViewModel()
        {
            GetFirstChecksAsync();
            Chasy.getDate += changeDateAsync;
            MainWindow.clsd += (t) => Chasy.getDate -= changeDate;
        }
        public async void GetFirstChecksAsync() => await Task.Run(() => { GetFirstChecks(); });
        public void GetFirstChecks()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var t in db.Checks.Where(g => g.WaiterId == MainWindow.Id && g.DateTimeCheck > DateTime.Now.Date).OrderBy(i => i.Id))
                    Application.Current.Dispatcher.Invoke(() => { CheckList.Add(new ACheck { IdCheck = t.Id.ToString(), CheckID = t.CheckCount, CheckTable = t.TableId.ToString() == "0" ? ("C собой") : t.TableId.ToString(), CheckDate = t.DateTimeCheck.ToString(), CheckPrice = checksPrise(t.Id, t.GuestsCount), CheckStatus = t.TableId.ToString() == "0" ? "Без услуги" : returnStatus(t.Status) }); });
            }
        }
        private string returnStatus(int status)
        {
            if (status == 1)
                return "Оплачено";
            else if (status == 2)
                return "Ошибка официанта";
            else if (status == 3)
                return "За счет заведения";
            else if (status == 4)
                return "Гость ушел";
            else
                return "В процессе";
        }
        ~ArchiveCheckViewModel() => Chasy.getDate -= changeDate;
        string checksPrise(int id, int count)
        {
            double x = 0;
            using (ApplicationContext db = new ApplicationContext())
            {
                // x = db.Orders.Where(i => i.CheckId == id && i.isCancel == 0).Select(h => db.Foods.Where(l => l.Id == h.FoodId).OrderBy(o => o.Id).Select(n => n.Price).LastOrDefault() * h.CountFood).OrderBy(p => p).Sum();
                foreach (var item in db.Orders.Where(i => i.CheckId == id && i.isCancel == 0).Select(i => i))
                {
                    double pl = priceF(item.FoodId);
                    if (item.isGramm > 0)
                        x += item.isGramm * pl / 100;
                    else
                        x += item.CountFood * pl;
                }
                if (db.Checks.Where(i => i.Id == id).Select(i => i.TableId).OrderBy(i => i).LastOrDefault() == 0)
                    return x.ToString();
                var st = db.Waiters.Where(i => i.Id == MainWindow.Id).OrderBy(l => l.Id).LastOrDefault();
                if (st != null)
                {
                    if (st.SalaryType == "Service")
                    {
                        x += count;
                    }
                    else if (st.SalaryType == "Percent")
                    {
                        x = Convert.ToInt32(x + (x / 100 * st.Salary));
                    }
                }
            }
             
            return x.ToString();
        }
        async void changeDateAsync(DateTime i) => await Task.Run(() => { changeDate(i); });
        void changeDate(DateTime i)
        {
            using ApplicationContext db = new ApplicationContext();
            Application.Current.Dispatcher.Invoke(() =>
            {
                CheckList = new ObservableCollection<ACheck>();
                foreach (var t in db.Checks.Where(g => g.WaiterId == MainWindow.Id && g.DateTimeCheck.Date == i.Date).OrderBy(i => i.Id))
                    CheckList.Add(new ACheck { IdCheck = t.Id.ToString(), CheckID = t.CheckCount, CheckDate = t.DateTimeCheck.ToString(), CheckTable = t.TableId.ToString(), CheckPrice = checksPrise(t.Id, t.GuestsCount), CheckStatus = t.GuestsCount.ToString() });
                CheckList = new ObservableCollection<ACheck>(CheckList.OrderBy(i => i.IdCheck));
            });
        }
    }
}
