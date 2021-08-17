using KafeProject.Date;
using KafeProject.View.User_Menu;
using KafeProject.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace KafeProject.All_Windows
{
    /// <summary>
    /// Логика взаимодействия для Kolichestvo_Bluda.xaml
    /// </summary>
    public partial class Kolichestvo_Bluda : Window
    {
        int tableIdForDynamicCheck = 0;
        int checkIdForGuestCount = 0;
        public delegate void Message2(int tableIdParametr,int guestCountParametr);
        public static event Message2 menuStolForDynamicCheck_;
        public delegate void PopupClose();
        public static event PopupClose pclose;
        bool flag = false;
        public Kolichestvo_Bluda(int tableId=0, int checkId=0)
        {
            InitializeComponent();
            MainWindow.timer.Stop();
            if (pclose!=null)
                pclose();
            if (tableId == 0)
            {
                
                flag = true;
                checkIdForGuestCount = MenuFood.IdCheck;
                using (ApplicationContext db = new ApplicationContext())
                {
                    Text_Kolichestvo.Text =db.Checks.Where(k => k.Id == checkIdForGuestCount).Select(o => o.GuestsCount.ToString()).OrderBy(p => p).LastOrDefault();
                }
            }               
            tableIdForDynamicCheck = tableId;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.t = 0;
            MainWindow.timer.Start();
            this.Close();
        }

        private void Plus_Button_Click_1(object sender, RoutedEventArgs e)
        {
            int count = Convert.ToInt32(Text_Kolichestvo.Text);
            if ((sender as Button).Name == "Plus_Button")
            {
                count++;
                Text_Kolichestvo.Text = count.ToString();
            }
            else if ((sender as Button).Name == "Minus_Button")
            {
                if (count > 0)
                {
                    count--;
                    Text_Kolichestvo.Text = count.ToString();
                }
            }
            else if ((sender as Button).Name == "Plus_Button_Copy1")
            {
                count += 10;
                Text_Kolichestvo.Text = count.ToString();
            }
            else if ((sender as Button).Name == "Minus_Button_Copy1")
            {
                if (count >= 10)
                {
                    count -= 10;
                    Text_Kolichestvo.Text = count.ToString();
                }
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(Text_Kolichestvo.Text) > 0)
            {
                string a = "";
                if (!flag)
                    menuStolForDynamicCheck_(tableIdForDynamicCheck, Convert.ToInt32(Text_Kolichestvo.Text.ToString()));
                else
                {

                    //checkIdForGuestCount
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        try
                        {
                            var t = db.Checks.Where(k => k.Id == checkIdForGuestCount).OrderBy(l => l.Id).LastOrDefault();
                            if (t != null)
                            {
                                t.GuestsCount = Convert.ToInt32(Text_Kolichestvo.Text);
                                db.SaveChanges();
                            }
                        }
                        catch (Exception ex)
                        {
                            a = ex.Message;
                        }
                    }
                }
                //MessageBox.Show(a+flag);
                MainWindow.t = 0;
                MainWindow.timer.Start();
                this.Close();
            }
        }

    }
}
