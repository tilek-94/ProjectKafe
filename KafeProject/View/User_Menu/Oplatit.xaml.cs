using KafeProject.Date;
using KafeProject.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KafeProject.View.User_Menu
{
    /// <summary>
    /// Логика взаимодействия для Oplatit.xaml
    /// </summary>
    public partial class Oplatit : Window
    {
        static TextBox text = new TextBox();
        
        double sdacha = 0;

        int itogsum = 0;

        public delegate void OplatitDel();
        public static event OplatitDel menuStolOpen;

        public Oplatit(string s)
        {
            InitializeComponent();
            K_Oplate.Text = s;
            if (s != null && s != "")
                itogsum = int.Parse(s);
            text = NalichText as TextBox;
            //using (ApplicationContext db = new ApplicationContext())
            //{

            //    var f = db.Waiters.Where(t => t.Id == MainWindow.Id).OrderBy(t => t.Id).FirstOrDefault();

            //    if (f != null)
            //    {
            //        if (f.SalaryType != null && f.SalaryType == "Percent")
            //        {
            //            itogsum = itogsum + Convert.ToInt32(itogsum / 100 * f.Salary);
            //        }
            //        else if (f.SalaryType != null && f.SalaryType == "Service")
            //        {
            //            if (MenuFood.IdCheck != 0)
            //            {
            //                var h = db.Checks.Where(g => g.Id == MenuFood.IdCheck).LastOrDefault();
            //                itogsum = itogsum + Convert.ToInt32(h.GuestsCount * f.Salary);
            //            }
            //        }
            //    }
            //}
        }
        private void Zakryt_Oplaty_Click(object sender, RoutedEventArgs e)
        {
            if (Zakryt_Oplaty.IsChecked == true)
            {

                using (ApplicationContext db = new ApplicationContext())
                {
                    //MenuFood.IdCheck
                    var result = db.Checks.SingleOrDefault(b => b.Id == MenuFood.IdCheck);
                    if (result != null)
                    {
                        if (status1.IsChecked == true)
                            result.Status = 4;
                        else if (status2.IsChecked == true)
                            result.Status = 3;
                        else
                            result.Status = 2;
                        db.SaveChanges();
                    }
                    menuStolOpen();
                    this.Close();
                }
            }
            else
            {
                if (double.Parse(K_Oplate.Text) <= double.Parse(CardText.Text) + double.Parse(NalichText.Text))
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        var result = db.Checks.SingleOrDefault(b => b.Id == MenuFood.IdCheck);
                        if (result != null)
                        {
                            result.Status = 1;
                            db.SaveChanges();
                        }
                    }
                    menuStolOpen();
                    this.Close();
                }
            }


        }

        private void Otmena_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Check()
        {
            if (NalichText.Text.Length > 0 && CardText.Text.Length > 0)
            {
                sdacha = itogsum - (double.Parse(CardText.Text) + double.Parse(NalichText.Text));
                if (sdacha < 0)
                    Sdacha.Text = (double.Parse(CardText.Text) + double.Parse(NalichText.Text) - itogsum).ToString();
                else
                    Sdacha.Text = "0";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (text != null)
            {
                if (text.Text.Length < 10)
                {
                    if (text.Text == "0")
                    {
                        text.Text = "";
                        text.Text += (sender as Button).Content;
                        Check();
                    }
                    else
                    {
                        text.Text += (sender as Button).Content;
                        Check();

                    }
                }                
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (text != null)
            {
                text.Text = "0";
                Check();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (text != null)
            {
                if (text.Text.Length > 0)
                {
                    if (CardText.Text.Length > 0 || NalichText.Text.Length > 0)
                    {
                        text.Text = text.Text.Substring(0, text.Text.Length - 1);
                        if (text.Text.Length == 0) text.Text = "0";
                        Check();
                    }
                    else
                    {
                        text.Text = "0";
                        Check();
                    }
                }
                else
                {
                    text.Text = "0";
                    Check();
                }

            }
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            text = sender as TextBox;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Zakryt_Oplaty_Click_1(object sender, RoutedEventArgs e)
        {
            if (Zakryt_Oplaty.IsChecked == true)
            {
                Oplatit1.Visibility = Visibility.Collapsed;
                Bez_Oplatit.Visibility = Visibility.Visible;
            }
            else
            {
                Oplatit1.Visibility = Visibility.Visible;
                Bez_Oplatit.Visibility = Visibility.Collapsed;
            }
        }
    }
}
