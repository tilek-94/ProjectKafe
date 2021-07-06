using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kafe.User_Menu
{
    /// <summary>
    /// Логика взаимодействия для Oplatit.xaml
    /// </summary>
    public partial class Oplatit : Window
    {
        public Oplatit()
        {
            InitializeComponent();
        }
        TextBox textboxx = null;
        private void Zakryt_Oplaty_Click(object sender, RoutedEventArgs e)
        {
            if (Zakryt_Oplaty.IsChecked != true)
            {
                Bez_Oplatit.Visibility = Visibility.Collapsed;
                Oplatit1.Visibility = Visibility.Visible;
            }
            else
            {
                Bez_Oplatit.Visibility = Visibility.Visible;
                Oplatit1.Visibility = Visibility.Collapsed;
            }
        }

        private void Otmena_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (textboxx != null)
            {
                Button button = sender as Button;
                textboxx.Text += button.Content.ToString();
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (textboxx != null)
            {
                textboxx.Text = String.Empty;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (textboxx != null)
            {
                if (textboxx.Text != String.Empty)
                {
                    textboxx.Text = textboxx.Text.Substring(0, textboxx.Text.Length - 1);
                }
            }
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.textboxx = (TextBox)sender;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
    }
}
