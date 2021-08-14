using KafeProject.All_Windows;
using KafeProject.Date;
using KafeProject.Models;
using System;
using System.Linq;
using System.Windows.Controls;

namespace KafeProject.View.User_Menu
{
    /// <summary>
    /// Логика взаимодействия для User_Ofissiant.xaml
    /// </summary>
    public partial class User_Ofissiant : UserControl
    {
        public delegate void CloseAl(int x=0);
        public static event CloseAl closeAll_;
        public User_Ofissiant()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            closeAll_(0);
            Arhiv_Check m = new Arhiv_Check();
            m.ShowDialog();
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var regime = db.Regime.Where(i => i.WaiterId == MainWindow.Id && i.StartTime > DateTime.Now.Date).Select(i=>i).OrderBy(i=>i).LastOrDefault();

                if (regime != null)
                {
                    regime.EndTime = DateTime.Now;
                    db.SaveChanges();
                    closeAll_(1);
                }
            }
        }
    }
}
