using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Printing;
using System.Linq;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using KafeProject.Date;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace KafeProject.All_Windows
{
    public partial class CheckWindow : Window
    {
        List<Params> paramses = new List<Params>();
        List<Params> paramses1 = new List<Params>();
        public string firstPrinter;
        public string secondPrinter;
        int checkid = 0;
        public CheckWindow(int id, int d)
        {
            InitializeComponent();
            MainWindow.timer.Stop();
            checkid = d;
            CheckOpenAsync(id);
        }

        public async void CheckOpenAsync(int id) 
        {
            Thread.Sleep(50);
            await Task.Run(()=> 
            {
                GetCafeName();
                GetItogSumAsync(id);
                GetPrinterName();
                GetFoodForCheckAsync(id);
                GetFoodForSecCheckAsync(id);
            });
        }

        public async void GetItogSumAsync(int id) 
        {
            //Thread.Sleep(10);
            await Task.Run(()=>
            {
                this.Dispatcher.Invoke(()=> GetItogSum(id));
            });
        }  
        public async void GetFoodForCheckAsync(int id)
        {
            //Thread.Sleep(10);
            await Task.Run(() => GetFoodForCheck(id));
        }
        public async void GetFoodForSecCheckAsync(int id)
        {
           // Thread.Sleep(10);
            await Task.Run(() => GetFoodForSecCheck(id));
        }

        async void GetPrinterName()
        {
            //Thread.Sleep(10);
            await Task.Run(() =>
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    firstPrinter = db.Options.Where(i => i.Key == "Printer-1").Select(i => i.Value).OrderBy(i => i).LastOrDefault();
                    secondPrinter = db.Options.Where(i => i.Key == "Printer-2").Select(i => i.Value).OrderBy(i => i).LastOrDefault();
                }
            });
        }

        void GetFoodForCheck(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                paramses = new List<Params>();
                // paramses.Add(new Params { Name = "Наименование", Count = "Кол-во", Price = "Цена", Itog = "Итог" });
                paramses.AddRange(
                    db.Orders.Where(i => i.CheckId == id && i.isCancel == 0).Join(db.Foods,
                    i => i.FoodId,
                    p => p.Id,
                    (i, p) => new Params
                    {
                        Gramm = i.isGramm.ToString(),
                        Name = i.isGramm > 0 ? p.Name + $"({i.isGramm})гр" : p.Name,
                        Count = (i.CountFood).ToString(),
                        Price = i.isGramm > 0 ? (p.Price * i.isGramm / 100) : p.Price,
                        Itog = (i.CountFood * (i.isGramm > 0 ? (p.Price * i.isGramm / 100) : p.Price))
                    })
                    );

            }
            this.Dispatcher.Invoke(() =>
            {
                list.ItemsSource = paramses;
            });
        }
        public void GetFoodForSecCheck(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                paramses1 = new List<Params>();
                paramses1.Add(new Params { Name = "Наименование", Count = "Кол-во" });
                paramses1.AddRange(
                    db.Orders.Where(i => i.CheckId == id && i.isCancel == 0).Join(db.Foods.Where(i => i.isCook == 1),
                    i => i.FoodId,
                    p => p.Id,
                    (i, p) => new Params
                    {
                        Name = i.isGramm > 0 ? p.Name + $"({i.isGramm})гр" : p.Name,
                        Count = (i.CountFood).ToString(),
                    })
                    );
            }
            this.Dispatcher.Invoke(() =>
            {
                list1.ItemsSource = paramses1;
            });
        }
        public void GetItogSum(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var check = db.Checks.Where(i => i.Id == id).Select(i => i).OrderBy(i => i).LastOrDefault();
                CheckDate.Text = check.DateTimeCheck.ToString();
                CheckName.Text = check.CheckCount.ToString();
                TableId.Text = db.Tables.Where(o => o.Id == check.TableId)?.Select(l => l.Name)?.OrderBy(p => p)?.LastOrDefault() ?? "";
                WaiterName.Text = db.Waiters.Where(p => p.Id == check.WaiterId)?.Select(p => p.Name)?.OrderBy(p => p)?.LastOrDefault() ?? "";

                CheckName1.Text = check.CheckCount.ToString();
                TableId1.Text = db.Tables.Where(o => o.Id == check.TableId)?.Select(l => l.Name)?.OrderBy(p => p)?.LastOrDefault() ?? "";
                WaiterName1.Text = db.Waiters.Where(p => p.Id == check.WaiterId)?.Select(p => p.Name)?.OrderBy(p => p)?.LastOrDefault() ?? "";

                double x = 0;
                foreach (var item in paramses)
                {
                    x += item.Itog;
                }
                // MessageBox.Show(paramses.Select(r => r.Itog == "Итог" ? 0 : Math.Round(Convert.ToDouble(r.Itog), 1)).OrderBy(i => i).Sum().ToString());
                //double x = paramses.Select(r => r.Itog == "Итог" ? 0 : Math.Round(Convert.ToDouble(r.Itog), 1)).OrderBy(i => i).Sum();

                Itog.Text = "Цена еды " + (x).ToString();
                if (db.Checks.Where(i => i.Id == id).Select(i => i.TableId).OrderBy(i => i).LastOrDefault() != 0)
                {
                    var re = db.Waiters.Where(i => i.Id == check.WaiterId).Select(i => i).OrderBy(i => i).LastOrDefault();
                    if (re.SalaryType == "Percent")
                        Obsluz.Text = "Обслуживание " + Convert.ToInt32((Convert.ToDouble(Itog.Text.Split()[2]) / 100 * re.Salary)).ToString();
                    else if (re.SalaryType == "Service")
                        Obsluz.Text = "Обслуживание " + (Convert.ToInt32(check.GuestsCount) * re.Salary).ToString();
                    else
                    {
                        Obsluz.Text = "Обслуживание 0";
                    }
                }
                else
                {
                    Obsluz.Text = "Обслуживание 0";
                }

                ObshItog.Text = "Итого " + Math.Round((Convert.ToDouble(Itog.Text.Split()[2]) + (Convert.ToDouble(Obsluz.Text.Split()[1]))), 1).ToString();
            }
        }
        public async void GetCafeName()
        {
            await Task.Run(() =>
            {
                using ApplicationContext db = new ApplicationContext();
                var cafename = db.CafeName.Select(u => u).OrderBy(u => u).LastOrDefault();
                if (cafename != null)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        CName.Text = cafename.Name.ToString();
                        CAdress.Text = cafename.Adress.ToString();
                    });
                }
            });
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkid == 1)
                {
                    PrintDialog printDialog = new PrintDialog();

                    myPrinters.SetDefaultPrinter(firstPrinter);

                    grd.Measure(new System.Windows.Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight));

                    grd.Arrange(new Rect(grd.DesiredSize));

                    grd.UpdateLayout();

                    printDialog.PrintTicket.PageMediaSize = new PageMediaSize(printDialog.PrintableAreaWidth, grd.ActualHeight);

                    printDialog.PrintQueue = LocalPrintServer.GetDefaultPrintQueue();

                    printDialog.PrintVisual(grd, "Test");
                }
                else
                {
                    if (paramses1.Count > 1)
                    {
                        PrintDialog printDialog = new PrintDialog();

                        myPrinters.SetDefaultPrinter(secondPrinter);

                        grd1.Measure(new System.Windows.Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight));

                        grd1.Arrange(new Rect(grd1.DesiredSize));

                        grd1.UpdateLayout();

                        printDialog.PrintTicket.PageMediaSize = new PageMediaSize(printDialog.PrintableAreaWidth, grd1.ActualHeight);

                        printDialog.PrintQueue = LocalPrintServer.GetDefaultPrintQueue();

                        printDialog.PrintVisual(grd1, "Test");
                    }
                }
            }
            catch
            {

            }
        }
        public static class myPrinters
        {
            [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern bool SetDefaultPrinter(string Name);

        }

        public class Params
        {
            public string Name { get; set; }
            public string Count { get; set; }
            public double Price { get; set; }
            public double Itog { get; set; }
            public string Gramm { get; set; }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow.t = 0;
            MainWindow.timer.Start();
            this.Close();
        }
    }
}
