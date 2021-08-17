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
    public partial class TableChangeWindow : Window
    {

        List<TableV> list = new List<TableV>();
        public TableChangeWindow()
        {
            InitializeComponent();
            MessageBox.Show(MenuFood.IdCheck.ToString());
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
                var check = db.Checks.Where(i => i.Id == MenuFood.IdCheck).Select(i => i).OrderBy(i => i).LastOrDefault();
                if (check != null)
                {
                    int tableid = 0;
                    foreach (var item in list)
                    {
                        if (item == TableId.SelectedItem)
                        {
                            tableid = item.TableId;
                        }
                    }
                    check.TableId = tableid;
                    db.SaveChanges();
                }
            }
            ///c.lf 
            if (clsRW != null)
                clsRW();
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
            this.Close();
        }
    }
}
