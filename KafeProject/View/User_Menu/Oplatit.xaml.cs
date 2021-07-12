using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KafeProject.View.User_Menu
{
    /// <summary>
    /// Логика взаимодействия для Oplatit.xaml
    /// </summary>
    public partial class Oplatit : Window
    {
        static TextBox text = new TextBox();

        int sdacha = 0;

        int itogsum = 0;

        public Oplatit(string s)
        {
            InitializeComponent();
            K_Oplate.Text = s;
            itogsum = int.Parse(s);
        }
        private void Zakryt_Oplaty_Click(object sender, RoutedEventArgs e)
        {
            if (Zakryt_Oplaty.IsChecked != true)
            {
                
            }
            else
            {

            }
        }

        private void Otmena_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Check() 
        {
            sdacha = itogsum - (int.Parse(CardText.Text) + int.Parse(NalichText.Text));
            if (sdacha < 0)
                Sdacha.Text = (int.Parse(CardText.Text) + int.Parse(NalichText.Text) - itogsum).ToString();
            else
                Sdacha.Text = "0";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (text != null)
            {
                if (text.Text == "0")
                {
                    text.Text = "";
                    text.Text += (sender as Button).Content;
                    Check();
                }
                else
                {
                    text.Text += (sender as Button).Content;
                    Check();

                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (text != null)
            {
                text.Text = "0";
                Check();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (text != null)
            {
                if (CardText.Text.Length != 1 || NalichText.Text.Length != 1)
                {
                    text.Text = text.Text.Substring(0, text.Text.Length - 1);
                    Check();
                }
                else
                {
                    text.Text = "0";
                }
            }
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            text = sender as TextBox;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
