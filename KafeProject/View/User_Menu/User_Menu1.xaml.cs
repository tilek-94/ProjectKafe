using KafeProject.All_Windows;
using KafeProject.Date;
using System.Linq;
using System.Windows.Controls;
namespace KafeProject.View.User_Menu
{
    public partial class User_Menu1 : UserControl
    {
        public delegate void closePop();
        public static event closePop cls;
        public User_Menu1()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (cls != null)
                cls();
            if (MenuFood.IdCheck > 0) 
            {
                Kolichestvo_Bluda n = new Kolichestvo_Bluda();
                n.ShowDialog();
            }
        }
        private void Button_Click_2(object sender, System.Windows.RoutedEventArgs e)
        {
            if (cls != null)
                cls();
            if (MenuFood.IdCheck != 0)
                using (ApplicationContext db = new ApplicationContext())
                { 
                    int? t = db.Checks.Where(i => i.Id == MenuFood.IdCheck && i.Status == 5)?.Select(i=>i.GuestsCount)?.OrderBy(i=>i)?.LastOrDefault()??0;
                    if (t>1) 
                    {
                        Razdelit_Window m = new Razdelit_Window();
                        m.Show();
                    }
                }
        }
    }
}
