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

namespace KafeProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using(ApplicationContext db=new ApplicationContext())
            {
                Options op = new Options()
                {
                    Key="Test1",
                    Value="test2"
                };
                db.Options.Add(op);
                db.SaveChanges();
            }
        }
    }
}
