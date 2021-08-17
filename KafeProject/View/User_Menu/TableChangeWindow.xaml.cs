using KafeProject.All_Windows;
using KafeProject.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace KafeProject.View.User_Menu
{
    /// <summary>
    /// Логика взаимодействия для TableChangeWindow.xaml
    /// </summary>
    public partial class TableChangeWindow : Window
    {
        List<TableV> list = new List<TableV>();
        public TableChangeWindow()
        {
            InitializeComponent();
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
            if (closeChange != null)
                closeChange();
            this.Close();
        }
        public delegate void CloseRW();
        public static event CloseRW closeChange;
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
