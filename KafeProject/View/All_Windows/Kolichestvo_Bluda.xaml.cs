using System;
using System.Windows;
using System.Windows.Controls;

namespace Kafe.All_Windows
{
    /// <summary>
    /// Логика взаимодействия для Kolichestvo_Bluda.xaml
    /// </summary>
    public partial class Kolichestvo_Bluda : Window
    {
        public Kolichestvo_Bluda(int userId=0, int tableId=0)
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
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
                if (count > 10)
                {
                    count -= 10;
                    Text_Kolichestvo.Text = count.ToString();
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
