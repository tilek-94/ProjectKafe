using System;
using System.Windows;
using System.Windows.Controls;

namespace KafeProject.All_Windows
{
    /// <summary>
    /// Логика взаимодействия для Klaviatura.xaml
    /// </summary>
    public partial class Klaviatura : Window
    {
        public Klaviatura()
        {
            InitializeComponent();
            MainWindow.timer.Stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.t = 0;
            MainWindow.timer.Start();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            textbox1.Text += button.Content.ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            textbox1.Text = String.Empty;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (textbox1.Text!=String.Empty)
            {
                textbox1.Text = textbox1.Text.Substring(0, textbox1.Text.Length - 1);
            }
        }
    }
}
