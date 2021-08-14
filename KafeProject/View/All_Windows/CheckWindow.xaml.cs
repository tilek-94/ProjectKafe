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

namespace KafeProject.All_Windows
{
    public partial class CheckWindow : Window
    {
        List<Params> paramses = new List<Params>();
        List<Params> paramses1 = new List<Params>();
        int checkid = 0;
        public CheckWindow(int id, int d)
        {
            InitializeComponent();
            paramses = new List<Params>();
            paramses1 = new List<Params>();
            MessageBox.Show(id.ToString());
            checkid = d;
            paramses.Add(new Params { Name = "Наименование", Count = "Кол-во", Price = "Цена", Itog = "Итог" });
            paramses1.Add(new Params { Name = "Наименование", Count = "Кол-во" });
            using (ApplicationContext db = new ApplicationContext())
            {
                paramses.AddRange(
                    db.Orders.Where(i => i.CheckId == id && i.isCancel == 0).Join(db.Foods,
                    i => i.FoodId,
                    p => p.Id,
                    (i, p) => new Params
                    {
                        Name = p.Name,
                        Count = (i.CountFood).ToString(),
                        Price = p.Price.ToString(),
                        Itog = (i.CountFood * p.Price).ToString()
                    })
                    );

                paramses1.AddRange(
                    db.Orders.Where(i => i.CheckId == id && i.isCancel == 0).Join(db.Foods.Where(i => i.isCook == 1),
                    i => i.FoodId,
                    p => p.Id,
                    (i, p) => new Params
                    {
                        Name = p.Name,
                        Count = (i.CountFood).ToString(),
                        Price = p.Price.ToString(),
                        Itog = (i.CountFood * p.Price).ToString()
                    })
                    );

                var check = db.Checks.Where(i => i.Id == id).Select(i => i).OrderBy(i => i).LastOrDefault();

                CheckDate.Text = check.DateTimeCheck.ToString();
                CheckName.Text = check.CheckCount.ToString();
                TableId.Text = db.Tables.Where(o => o.Id == check.TableId)?.Select(l => l.Name)?.OrderBy(p => p)?.LastOrDefault() ?? "";
                WaiterName.Text = db.Waiters.Where(p => p.Id == check.WaiterId)?.Select(p => p.Name)?.OrderBy(p => p)?.LastOrDefault() ?? "";


                CheckName1.Text = check.CheckCount.ToString();
                TableId1.Text = db.Tables.Where(o => o.Id == check.TableId)?.Select(l => l.Name)?.OrderBy(p => p)?.LastOrDefault() ?? "";
                WaiterName1.Text = db.Waiters.Where(p => p.Id == check.WaiterId)?.Select(p => p.Name)?.OrderBy(p => p)?.LastOrDefault() ?? "";

                #region MyRegion

                double x = paramses.Select(r => r.Itog == "Итог" ? 0 : Convert.ToDouble(r.Itog)).OrderBy(i => i).Sum();
                Itog.Text = "Цена еды " + (x).ToString();
                if (db.Checks.Where(i => i.Id == id).Select(i => i.TableId).OrderBy(i => i).LastOrDefault() != 0)
                {
                    var re = db.Waiters.Where(i => i.Id == check.WaiterId).Select(i => i).OrderBy(i => i).LastOrDefault();
                    if (re.SalaryType == "Percent")
                        Obsluz.Text = "Обслуживание " + (Convert.ToDouble(Itog.Text.Split()[2]) / 100 * re.Salary).ToString();
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

                ObshItog.Text = "Итого " + (Convert.ToDouble(Itog.Text.Split()[2]) + (Convert.ToDouble(Obsluz.Text.Split()[1]))).ToString();

                #endregion

            }
            list1.ItemsSource = paramses1;
            list.ItemsSource = paramses;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (checkid == 1)
                {
                    PrintDialog printDialog = new PrintDialog();

                    myPrinters.SetDefaultPrinter("XP-80C");

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

                        myPrinters.SetDefaultPrinter("XP-80C (copy 1)");

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
            public string Price { get; set; }
            public string Itog { get; set; }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
