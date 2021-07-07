using Kafe.All_Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KafeProject.View.User_Menu
{
    /// <summary>
    /// Логика взаимодействия для MenuStol.xaml
    /// </summary>
    public partial class MenuStol : UserControl
    {
        public MenuStol()
        {
            InitializeComponent();
        }

        void timer_Tick(object sender, EventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Parol_Window parol_Window = new Parol_Window();
            parol_Window.Show();
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Knopki();
            Knopki_kategoryy();


        }
        void button_Click(object sender, RoutedEventArgs e)
        {

            //MessageBox.Show(string.Format("You clicked on the Content {0} TAG {1}. button.", (sender as Button).Content, (sender as Button).Tag));
        }
        void button_Category_Click(object sender, RoutedEventArgs e)
        {

        }
        public void Knopki()
        {

        }
        public void Knopki_kategoryy()
        {

        }
    }
}
