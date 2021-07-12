using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Data;
using Kafe.User_Menu;
using Kafe.All_Windows;
using KafeProject.Date;
using KafeProject.Models;
using KafeProject.View.All_Windows;
using KafeProject.View.User_Menu;
using System.Linq;

namespace KafeProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int Id { get; set; }
        public MainWindow(int id=0)
        {
            InitializeComponent();
            //MenuFood menuFood = new MenuFood();
            MenuStol menuFood = new MenuStol(id);
            GlawMenu.Children.Add(menuFood);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MenuStol.menuStol_ += checkIdForMainWindow =>
            {
                MessageBox.Show(checkIdForMainWindow+"");
                GlawMenu.Children.Clear();
                MenuFood m = new MenuFood();
                GlawMenu.Children.Add(m);
            };
            Kolichestvo_Bluda.menuStolForDynamicCheck_ += (tableId,guestCount) =>
            {
                MessageBox.Show(tableId + " "+ guestCount);
                GlawMenu.Children.Clear();
                MenuFood m = new MenuFood();
                GlawMenu.Children.Add(m);
            };

        }
        void windowGlawMenu(object sender=null, RoutedEventArgs e=null) 
        {
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            this.Close();
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Parol_Window parol_Window = new Parol_Window();
            parol_Window.Show();
            this.Close();
        }
    }
}
