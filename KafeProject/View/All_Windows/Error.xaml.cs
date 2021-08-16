using KafeProject.Date;
using KafeProject.Models;
using KafeProject.View.User_Menu;
using KafeProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace KafeProject.All_Windows
{
    /// <summary>
    /// Логика взаимодействия для Error.xaml
    /// </summary>
    public partial class Error : Window
    {

        List<TableV> list = new List<TableV>();
        ObservableCollection<Order> checks = new ObservableCollection<Order>();

        ObservableCollection<Order> retchecks = new ObservableCollection<Order>();

        int a = 0;
        int b = 0;

        public Error(ObservableCollection<RCheck> returnedList, ObservableCollection<RCheck> updatedList, int retStol, int razStol)
        {
            InitializeComponent();

            foreach (var item in updatedList)
            {
                checks.Add(new Order { CheckId = MenuFood.IdCheck, CountFood = item.RCheckCount, FoodId = item.RFoodID });
            }

            foreach (var item in returnedList)
            {
                retchecks.Add(new Order { CheckId = MenuFood.IdCheck, CountFood = item.RCheckCount, FoodId = item.RFoodID });
            }

            a = retStol;

            b = razStol;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TablePlace_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TablePlace.SelectedItem != null)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var lister = db.Tables.Where(i => i.Location.Name == TablePlace.SelectedItem.ToString()).Select(i => i);
                    list = new List<TableV>();
                    foreach (var item in lister)
                    {
                        if (Result(item.Id))
                        {
                            list.Add(new TableV { TableId = item.Id, TableName = item.Name });
                        }
                    }

                    TableId.ItemsSource = list.ToArray();

                    #region 
                    /*                    var list = db.Checks.Join(db.Tables,
                        c=>c.TableId,
                        t=>t.Id,
                        (c, t) => new 
                        {
                        Name = t.Name
                        }
                        );*/
                    #endregion
                }
            }
        }

        bool Result(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var che = db.Checks.Where(c => c.DateTimeCheck > DateTime.Now.Date && c.Status == 5 && c.Id != id).Select(c => c.TableId);
                foreach (var item in che)
                {
                    if (item == id) return false;
                }
            }
            return true;
        }

        private void TableId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var ch = db.Checks.Where(i => i.Id == MenuFood.IdCheck).Select(i => i).OrderBy(i => i).LastOrDefault();
                if (ch != null)
                {
                    ch.GuestsCount = a;
                    db.SaveChanges();
                }

                var raz = db.Orders.Where(i => i.CheckId == MenuFood.IdCheck);
                db.RemoveRange(raz);
                db.SaveChanges();

                db.AddRange(checks);
                db.SaveChanges();

                int id = 0;
                var checknew = db.Checks.Where(i =>i.DateTimeCheck > DateTime.Now.Date).Select(i => i.CheckCount).OrderBy(i => i).LastOrDefault() + 1;
                foreach (var item in list)
                {
                    if (item == TableId.SelectedItem)
                    {
                        id = item.TableId;
                    }
                }

                var t = new Check { CheckCount = checknew, Status = 5, GuestsCount = b, TableId = id, DateTimeCheck = DateTime.Now, WaiterId = ch.WaiterId };
                db.Checks.Add(t);
                db.SaveChanges();

                ObservableCollection<Order> re = new ObservableCollection<Order>();
                var lastid = db.Checks.Select(i => i.Id).OrderBy(i => i).LastOrDefault();
                foreach (var item in retchecks)
                {
                    re.Add(new Order { FoodId = item.FoodId, CheckId = lastid, CountFood = item.CountFood });
                }
                db.AddRange(re);
                db.SaveChanges();
            }
            ///c.lf
            clsRW();
            MainWindow.t = 0;
            MainWindow.timer.Start();
            this.Close();
        }
        public delegate void CloseRW();
        public static event CloseRW clsRW;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var list = db.Locations.Select(i => i.Name);
                TablePlace.ItemsSource = list.ToArray();
            }
        }

        private void Otpravka_Kuxne_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.t = 0;
            MainWindow.timer.Start();
            this.Close();
        }
    }
    class TableV
    {
        public int TableId { get; set; }
        public string TableName { get; set; }
    }
}
