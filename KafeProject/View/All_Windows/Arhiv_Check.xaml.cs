using KafeProject.Date;
using KafeProject.Infrastructure;
using KafeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KafeProject.All_Windows
{
    /// <summary>
    /// Логика взаимодействия для Arhiv_Check.xaml
    /// </summary>
    public partial class Arhiv_Check : Window
    {
        List<ArchiveFood> lister;
        public Arhiv_Check()
        {
            InitializeComponent();
            lister = new List<ArchiveFood>();
            GetFoodsAsync();
            FirstDate.Text = DateTime.Now.Date.ToString();
            SecondDate.Text = DateTime.Now.Date.ToString();
            Chasy.getDate2 += Returne;
        }

        public async void GetFoodsAsync()
        {
            await Task.Run(() => { GetFoods(); });
        }

        public void GetFoods()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var checks = db.Checks.Where(i => i.WaiterId == MainWindow.Id && (i.DateTimeCheck <= DateTime.Now && i.DateTimeCheck >= DateTime.Now.AddDays(-1))).Select(i => i);

                foreach (var item in checks)
                {
                    GetOrder(item);
                }
                var result = from s in lister
                             group s by new { s.Name, s.Price } into g
                             select new
                             {
                                 Name = g.Key.Name,
                                 Price = g.Key.Price,
                                 Count = g.Sum(i => i.Count),
                                 Itog = g.Sum(i => i.Count) * g.Key.Price
                             };
                this.Dispatcher.Invoke(() =>
                {
                    var salary = ReturnSalary();
                    if (salary.Item1 == "Percent")
                    {
                        Obsluz.Text = "Обслуживание - " + (result.Sum(i => i.Itog) * salary.Item2 / 100);
                        SumItog.Text = "Общее - " + (result.Sum(i => i.Itog) + (result.Sum(i => i.Itog) * salary.Item2 / 100));
                    }
                    else if (salary.Item1 == "Service")
                    {
                        Obsluz.Text = "Обслуживание - " + ((result.Sum(i => i.Itog) + (salary.Item2 * checks.Sum(i => i.GuestsCount))));
                        SumItog.Text = "Общее - " + (result.Sum(i => i.Itog) + (result.Sum(i => i.Itog) + (salary.Item2 * checks.Sum(i => i.GuestsCount))));
                    }
                    else
                    {
                        Obsluz.Text = "Обслуживание - 0";
                        SumItog.Text = "Общее - " + result.Sum(i => i.Itog);
                    }

                    ObshItog.Text = "Итого - " + result.Sum(i => i.Itog);
                    list.ItemsSource = result;
                });
            }
        }
        public void GetOrder(Check check)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var order = db.Orders.Where(i => i.CheckId == check.Id).Select(i => i);
                foreach (var item in order)
                {
                    if (item.isCancel == 0)
                    {
                        if (item.isGramm > 0)
                        {
                            lister.Add(new ArchiveFood
                            {
                                Name = GetFoodName(item.FoodId) + $"({item.isGramm})гр",
                                Count = item.CountFood,
                                Price = (GetFoodPrice(item.FoodId) * item.isGramm / 100),
                                Itog = item.CountFood * (GetFoodPrice(item.FoodId) * item.isGramm / 100),
                            });
                        }
                        else
                        {
                            lister.Add(new ArchiveFood
                            {
                                Name = GetFoodName(item.FoodId),
                                Count = item.CountFood,
                                Price = GetFoodPrice(item.FoodId),
                                Itog = item.CountFood * GetFoodPrice(item.FoodId)
                            });
                        }
                    }
                }
            }
        }
        public string GetFoodName(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Foods.Where(i => i.Id == id).Select(o => o.Name).OrderBy(i => i).LastOrDefault();
            }

        }
        public double GetFoodPrice(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                return db.Foods.Where(i => i.Id == id).Select(o => o.Price).OrderBy(i => i).LastOrDefault();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Otpravka_Kuxne_Copy_Click(object sender, RoutedEventArgs e)
        {
            Chasy chasy = new Chasy();
            chasy.ShowDialog();
        }

        private void GetDa(object sender, RoutedEventArgs e)
        {
            string tag = (sender as Button).Tag.ToString();
            if (tag == "1")
            {
                Chasy chasy = new Chasy(1);
                chasy.ShowDialog();
            }
            else
            {
                Chasy chasy = new Chasy(2);
                chasy.ShowDialog();
            }
        }
        void Returne(DateTime i, int co)
        {
            if (co == 1)
            {

                FirstDate.Text = i.ToString();
            }
            else
            {

                SecondDate.Text = i.ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (FirstDate.Text != "" && SecondDate.Text != "")
            {
                GetFoodsSecAsync(DateTime.Parse(FirstDate.Text), DateTime.Parse(SecondDate.Text));
            }
            else
            {
                MessageWindow message = new MessageWindow("Выберите даты!");
                message.ShowDialog();
            }
        }

        async void GetFoodsSecAsync(DateTime first, DateTime second)
        {
            await Task.Run(() => { GetFoods(first, second); });
        }
        public void GetFoods(DateTime first, DateTime second)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var checks = db.Checks.Where(i => i.WaiterId == MainWindow.Id && (i.DateTimeCheck >= first && i.DateTimeCheck <= second)).Select(i => i);

                foreach (var item in checks)
                {
                    GetOrder(item);
                }
                var result = from s in lister
                             group s by new { s.Name, s.Price } into g
                             select new
                             {
                                 Name = g.Key.Name,
                                 Price = g.Key.Price,
                                 Count = g.Sum(i => i.Count),
                                 Itog = g.Sum(i => i.Count) * g.Key.Price
                             };

                this.Dispatcher.Invoke(() =>
                {
                    var salary = ReturnSalary();
                    if (salary.Item1 == "Percent")
                    {
                        Obsluz.Text = "Обслуживание - " + (result.Sum(i => i.Itog) * salary.Item2 / 100);
                        SumItog.Text = "Общее - " + (result.Sum(i => i.Itog) + (result.Sum(i => i.Itog) * salary.Item2 / 100));
                    }
                    else if (salary.Item1 == "Service")
                    {
                        Obsluz.Text = "Обслуживание - " + ((result.Sum(i => i.Itog) + (salary.Item2 * checks.Sum(i => i.GuestsCount))));
                        SumItog.Text = "Общее - " + (result.Sum(i => i.Itog) + (result.Sum(i => i.Itog) + (salary.Item2 * checks.Sum(i => i.GuestsCount))));
                    }
                    else
                    {
                        Obsluz.Text = "Обслуживание - 0";
                        SumItog.Text = "Общее - " + result.Sum(i => i.Itog);
                    }

                    ObshItog.Text = "Итого - " + result.Sum(i => i.Itog);

                });
            }
        }

        public (string, double) ReturnSalary()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var salary = db.Waiters.Where(i => i.Id == MainWindow.Id).OrderBy(i => i).LastOrDefault();
                if (salary.SalaryType == "Percent")
                {
                    return ("Percent", salary.Salary);

                }
                else if (salary.SalaryType == "Service")
                {
                    return ("Service", salary.Salary);
                }
                return ("0", 0);
            }
        }
    }
    public class ArchiveFood
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public double Itog { get; set; }
    }
}
