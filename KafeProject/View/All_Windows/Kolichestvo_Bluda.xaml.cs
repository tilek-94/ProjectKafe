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
        int tableIdForDynamicCheck = 0;
        public delegate void Message2(int tableIdParametr,int guestCountParametr);
        public static event Message2 menuStolForDynamicCheck_;
        public Kolichestvo_Bluda(int tableId=0)
        {
            InitializeComponent();
            tableIdForDynamicCheck = tableId;
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
            menuStolForDynamicCheck_(tableIdForDynamicCheck, Convert.ToInt32(Text_Kolichestvo.Text.ToString()));
            this.Close();
        }
    }
}
