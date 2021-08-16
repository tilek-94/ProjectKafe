using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using KafeProject.All_Windows;
using KafeProject.Date;
using KafeProject.Infrastructure.Commands;
using KafeProject.Models;
using KafeProject.View.User_Menu;
using KafeProject.ViewModels.Base;

namespace KafeProject.ViewModels
{
    class MenuFoodViewModel : ViewModel
    {
        #region Prop
        private ObservableCollection<MenuFoodParams> _FoodList = new ObservableCollection<MenuFoodParams>();
        public ObservableCollection<MenuFoodParams> FoodList
        {
            get { return _FoodList; }
            set { Set(ref _FoodList, value); }
        }
        private ObservableCollection<MenuFoodParams> _isCancel = new ObservableCollection<MenuFoodParams>();
        public ObservableCollection<MenuFoodParams> isCancel
        {
            get { return _isCancel; }
            set { Set(ref _isCancel, value); }
        }
        private double _ItogSum = 0.00;
        public double ItogSum
        {
            get
            {
                if (idTable != 0)
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {

                        var f = db.Waiters.Where(t => t.Id == MainWindow.Id).OrderBy(t => t.Id).FirstOrDefault();

                        if (f != null)
                        {
                            if (f.SalaryType != null && f.SalaryType == "Percent")
                            {
                                return _ItogSum + Convert.ToInt32(_ItogSum / 100 * f.Salary);
                            }
                            else if (f.SalaryType != null && f.SalaryType == "Service")
                            {
                                if (MenuFood.IdCheck != 0)
                                {
                                    var h = db.Checks.Where(g => g.Id == MenuFood.IdCheck).OrderBy(t=>t).LastOrDefault();
                                    return _ItogSum + Convert.ToInt32(h.GuestsCount * f.Salary);
                                }
                                else
                                {
                                    return _ItogSum + Convert.ToInt32(MenuFood.GuestCount * f.Salary);//MenuFood.GuestCount
                                }
                            }
                        }
                    }
                    return _ItogSum;
                }
                else 
                {
                    //MessageBox.Show("idtable "+idTable + " idCheck "+idCheck);
                    return _ItogSum;
                }
            }
            set { Set(ref _ItogSum, value); }
        }
        private double _ObsSum = 0.00;
        public double ObsSum
        {
            get { return _ObsSum; }
            set { Set(ref _ObsSum, _ItogSum); }
        }
        private double _PercentSum = 0.00;
        public double PercentSum
        {
            get { return _PercentSum; }
            set
            {
                if (idTable != 0)
                {
                    Set(ref _PercentSum, (ItogSum - _ItogSum));
                }
                else 
                    Set(ref _PercentSum, 0);
            }
        }
        #endregion
        private int idCheck = 0;
        private int idTable = 0;
        private int guestCount = 0;
        private int idWaiter = 0;
        public static string comm = "";
        public delegate void AddNewCheck();
        public static event AddNewCheck openStolWindow;
        public delegate void ifCountProductNull();
        public static event ifCountProductNull showMessage;

        private MenuFoodParams _SelectedFood = new MenuFoodParams();
        public MenuFoodParams SelectedFood
        {
            get
            {
                return _SelectedFood;
            }
            set
            {
                if (value != null)
                {
                    isCancel.Add(value);
                    if (value.Count > 1)
                    {
                        ItogSum = _ItogSum - value.Price;
                        PercentSum = 0;
                        ObsSum = 0;
                        int food = FoodList.IndexOf(value);
                        FoodList.RemoveAt(food);
                        var fod = value;                   
                        fod.Count -= 1;
                        FoodList.Insert(food, fod);
                    }
                    else
                    {
                        ItogSum = _ItogSum - value.Price;
                        PercentSum = 0;
                        ObsSum = 0;
                        int food = FoodList.IndexOf(value);
                        FoodList.RemoveAt(food);
                    }
                }
            }
        }

        public MenuFoodViewModel()
        {
            // string checkStatus;
            PercentSum = 0.00;
            ObsSum = 0.00;
            ItogSum = 0.00;
            MainWindow.menuCheck_ += MethodForDelegate;
            MenuFood.foodEvent += MethodForAddFood;
            MainWindow.fuckAll_ += thisClose;
            MainWindow.clsd+= thisClose;
        }
        ~MenuFoodViewModel() => thisClose();
        void thisClose(int i = 0)
        {
            if (i == 1)
            {
                MainWindow.menuCheck_ -= MethodForDelegate;
                MenuFood.foodEvent -= MethodForAddFood;
                return;
            }
            MainWindow.menuCheck_ -= MethodForDelegate;
            MenuFood.foodEvent -= MethodForAddFood;
            MainWindow.fuckAll_ -= thisClose;
            MainWindow.clsd -= thisClose;
        }
        void MethodForAddFood(MenuFoodParams x, int id)
        {
            for (int y = 0; y < FoodList.Count; y++)
                if (FoodList[y].Id == x.Id)
                {
                    var dynamic = FoodList[y];
                    FoodList.RemoveAt(y);
                    dynamic.Count += 1;
                    ItogSum = _ItogSum + x.Price;
                    PercentSum = 0;
                    ObsSum = 0;
                    //ItogSum += x.Price;
                    //ItogSum = _ItogSum;
                    FoodList.Add(dynamic);
                    return;
                }

            ItogSum = _ItogSum + x.Price;
            PercentSum = 0;
            ObsSum = 0;
            //ItogSum += x.Price;
            //ItogSum = _ItogSum;
            FoodList.Add(x);
        }
        void Fa(int idCheck, int idTable, int guestCount, int idWaiter)
        {
            Thread.Sleep(10);
            Application.Current.Dispatcher.Invoke(() =>
            {
                this.idWaiter = idWaiter;
            this.idCheck = idCheck;
            this.idTable = idTable;
            this.guestCount = guestCount;
            if (this.idCheck == 0)
                return;
            //MessageBox.Show(idCheck+"");
            using (ApplicationContext db = new ApplicationContext())
            {
                if (idCheck != 0)
                {
                    this.idTable = db.Checks.Where(i => i.Id == idCheck).Select(i => i.TableId)?.OrderBy(i => i)?.LastOrDefault() ?? 0;
                }
                FoodList = new ObservableCollection<MenuFoodParams>();
                // checkStatus = db.Checks.Where(b => b.TableId == 1).OrderBy(t => t.Id).LastOrDefault().Status.ToString() ?? "";

                var ord = db.Orders.Where(d => d.CheckId == idCheck && d.isCancel == 0);

                var result = ord.Join(db.Foods,
                    p => p.FoodId,
                    t => t.Id,
                    (p, t) => new { id = t.Id, t.Name, Count = p.CountFood, p.CheckId, t.Price });

                foreach (var s in result)
                {
                    ItogSum = _ItogSum + s.Price * s.Count;
                    PercentSum = 0;
                    ObsSum = 0;
                    FoodList.Add(new MenuFoodParams { Id = s.id, Name = s.Name, Count = s.Count, Price = s.Price });
                    //NaKuxneViewModel._FoodList.Add(new Food { Name = s.Name, Count = s.Count, Price = s.Count * s.Price }); ;
                }
                ItogSum = _ItogSum;
                //string checkStatus = db.Checks.Where(b => b.TableId == t.Id).OrderBy(t => t.Id).LastOrDefault().Status ?? "";
            }
            MainWindow.menuCheck_ -= MethodForDelegate;
            });
        }
        // определение асинхронного метода
        async void Fac(int idCheck, int idTable, int guestCount, int idWaiter)
        {
            await Task.Run(() => Fa(idCheck, idTable, guestCount, idWaiter));
        }
        private void MethodForDelegate(int idCheck, int idTable, int guestCount, int idWaiter)
        {
            Fac(idCheck, idTable, guestCount, idWaiter);
        }
        #region Commands

        #region Check
        public delegate void MessageForKitchen(ObservableCollection<MenuFoodParams> x, int idForCheck = 0);
        public static event MessageForKitchen paramsForKitchen;

        private LambdaCommand _SendCheckCommand;
        public LambdaCommand SendCheckCommand =>
            _SendCheckCommand ?? (_SendCheckCommand = new LambdaCommand(ExecuteSendCheckCommand, CanExecuteSendCheckCommand));

        void ExecuteSendCheckCommand(object p)
        {
            if (FoodList.Count > 0)
                paramsForKitchen(FoodList, 0);
            //Application.Current.Shutdown();
        }
        bool CanExecuteSendCheckCommand(object p)
        {
            return true;

        }




        private LambdaCommand _SendCheckKuhCommand;
        public LambdaCommand SendCheckKuhCommand =>
            _SendCheckKuhCommand ?? (_SendCheckKuhCommand = new LambdaCommand(ExecuteSendCheckKuhCommand, CanExecuteSendCheckKuhCommand));

        void ExecuteSendCheckKuhCommand(object p)
        {
            CheckWindow kuh = new CheckWindow(MenuFood.IdCheck,1);
            kuh.ShowDialog();
        }
        bool CanExecuteSendCheckKuhCommand(object p)
        {
            if (MenuFood.IdCheck != 0)
                return true;
            else
                return false;
        }



        #endregion

        #region Send

        private LambdaCommand _CheckPrintCommand;
        public LambdaCommand CheckPrintCommand =>
            _CheckPrintCommand ?? (_CheckPrintCommand = new LambdaCommand(ExecuteCheckPrintCommand, CanExecuteCheckPrintCommand));
        string p = "";
        private bool countProd(int idF, int countF)
        {

            using (ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    var result = db.ReceiptGoods.
                        Where(t => t.ProductId == db.Recipes.
                            Where(a => a.FoodId == idF).
                            Select(u => u.ProductId).OrderBy(o => o).LastOrDefault() && t.Count != 0).
                        OrderBy(g => g.Id).
                        FirstOrDefault();
                    if (result != null)
                    {
                        if (result.Count - countF < 1)
                        {
                            result.Count = 0;
                            db.SaveChanges();
                            return countProd(idF, countF - result.Count);
                        }
                        else
                        {
                            result.Count -= countF;
                            db.SaveChanges();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    p += ex.Message;
                    return false;
                }
            }

        }
        private async void countProdPlus(int idF, int countF)
        {
            await using (ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    var result = db.ReceiptGoods.
                        Where(t => t.ProductId == db.Recipes.
                            Where(a => a.FoodId == idF).
                            Select(u => u.ProductId).OrderBy(o => o).LastOrDefault() && t.Count != 0).
                        OrderBy(g => g.Id).
                        FirstOrDefault();
                    if (result != null)
                    {
                        result.Count += countF;
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    p += ex.Message;
                }
            }

        }
        void ExecuteCheckPrintCommand(object p)
        {
            if (idCheck == 0)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    try
                    {
                        bool productStatus = false;
                        string product = db.Options.Where(t => t.Key == "product").Select(g => g.Value).OrderBy(b => b).LastOrDefault() ?? "0";
                        if (product == "1")
                        {
                            foreach (var i in FoodList)
                                productStatus = countProd(i.Id, i.Count);
                            if (!productStatus)
                            {
                                foreach (var i in FoodList)
                                    countProdPlus(i.Id, i.Count);
                                //MessageBox.Show(this.p);
                                showMessage();
                                return;
                            }
                        }

                        int x = db.Checks?.Where(b => b.DateTimeCheck > DateTime.Now.Date)?.Select(p => p.CheckCount).OrderBy(a => a)?.LastOrDefault() ?? 0;
                        x += 1;
                        db.Checks.Add(new Check { comment = comm, Status = 5, DateTimeCheck = DateTime.Now, CheckCount = x, GuestsCount = guestCount, TableId = idTable, WaiterId = idWaiter });
                        db.SaveChanges();
                        MenuFood.IdCheck = db.Checks.Select(i => i.Id).OrderBy(i => i).LastOrDefault();
                        int idC = db.Checks.Select(l => l.Id).OrderBy(p => p).LastOrDefault();
                        ObservableCollection<Order> o = new ObservableCollection<Order>();
                        foreach (var i in FoodList)
                            o.Add(new Order { CountFood = i.Count, CheckId = idC, FoodId = i.Id, isCancel = 0 });
                        db.Orders.AddRange(o);

                        db.SaveChanges();
                        //h += o.Count()+"";
                        //db.Checks.Select(l=>l.Id).OrderBy(p=>p).LastOrDefault();
                        //idCheck = MenuFood.IdCheck;
                    }
                    catch (Exception)
                    {
                        //h += ex.Message;
                    }
                }
            }
            else
                using (ApplicationContext db = new ApplicationContext())
                {
                    bool productStatus = false;
                    string product = db.Options.Where(t => t.Key == "product").Select(g => g.Value).OrderBy(b => b).LastOrDefault() ?? "0";
                    if (product == "1")
                    {
                        foreach (var i in FoodList)
                            productStatus = countProd(i.Id, i.Count);
                        if (!productStatus)
                        {
                            foreach (var i in FoodList)
                                countProdPlus(i.Id, i.Count);
                            showMessage();
                            return;
                        }
                    }
                    ///////nenf
                    var t = db.Orders.Where(d => d.CheckId == idCheck && d.isCancel != 1);
                    var c = db.Checks.Where(d => d.Id == idCheck).OrderBy(y => y.Id).LastOrDefault();
                    if (c != null && comm != "")
                    {
                        c.comment = comm;
                        db.SaveChanges();
                    }
                    filtr();




                    foreach (var y in isCancel)
                        setOrderStatus(y.Id, y.Count);

                    db.Orders.RemoveRange(t);
                    db.SaveChanges();
                    ObservableCollection<Order> o = new ObservableCollection<Order>();
                    foreach (var i in FoodList)
                        o.Add(new Order { CountFood = i.Count, CheckId = idCheck, FoodId = i.Id, isCancel = 0 });
                    db.AddRange(o);
                    db.SaveChanges();
                }
            comm = "";
            CheckWindow ch = new CheckWindow(MenuFood.IdCheck,0);
            ch.ShowDialog();
            MenuFood.IdCheck = 0;
            thisClose();
            openStolWindow();
        }
        void filtr() 
        {
            isCancel.OrderBy(p=>p.Id);
            for (int qo = isCancel.Count() - 2; qo >= 0; qo--)
            {
                if (isCancel[qo] == isCancel[qo + 1])
                {
                    isCancel[qo].Count = isCancel[qo].Count + isCancel[qo + 1].Count;
                    isCancel.RemoveAt(qo + 1);
                }
            }
        }
        private void setOrderStatus(int idF, int countF)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    var result = db.Orders.Where(p => p.CheckId == idCheck && p.FoodId == idF && p.isCancel == 1).Select(p => p).OrderBy(p => p.Id).LastOrDefault();
                    if (result != null)
                    {
                        result.CountFood += countF;
                        db.SaveChanges();
                    }
                    else 
                    {
                        Order o = new Order{ CountFood = countF, CheckId = idCheck, FoodId = idF, isCancel = 1 };
                        db.Orders.Add(o);
                        db.SaveChanges();
                    }
                }
                catch { }
            }
        }
        private void setProd(int idF, int countF)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    var result = db.ReceiptGoods.
                            Where(t => t.ProductId == db.Recipes.
                                Where(a => a.FoodId == idF).
                                Select(u => u.ProductId).OrderBy(o => o).LastOrDefault() && t.Count == 0).
                            OrderBy(g => g.Id).
                            LastOrDefault();
                    if (result != null)
                    {
                        result.Count = countF;
                        db.SaveChanges();
                    }
                }
                catch { }
            }
        }
        bool CanExecuteCheckPrintCommand(object p)
        {
            return true;
        }

        #endregion

        #endregion
    }
}
