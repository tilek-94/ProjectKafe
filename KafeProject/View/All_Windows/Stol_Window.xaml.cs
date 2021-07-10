using KafeProject.View.All_Windows;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;


namespace Kafe.All_Windows
{
    /// <summary>
    /// Логика взаимодействия для Stol_Window.xaml
    /// </summary>
    public partial class Stol_Window : Window
    {
       
     
        public Stol_Window()
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
            this.Close();
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
