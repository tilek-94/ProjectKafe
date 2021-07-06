using System;
using System.Windows;
using System.Windows.Controls;

namespace Kafe.All_Windows
{
    /// <summary>
    /// Логика взаимодействия для Chasy.xaml
    /// </summary>
    public partial class Chasy : Window
    {
        public Chasy()
        {
            InitializeComponent();
        }
        private void Decrement_button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Name== "Button_Hourse_Text")
            {
                int Hor = Convert.ToInt32(Hourse_Text.Text);
                if (Hor == 00)
                {
                    Hourse_Text.Text = "23";
                }
                else
                {
                    Hor--;
                    if (Hor < 10)
                    {
                        Hourse_Text.Text = "0" + Hor.ToString();
                    }
                    else
                    {
                        Hourse_Text.Text = Hor.ToString();
                    }
                }
            }
            else if (button.Name == "Button_Minuta_Text")
            {
                int Hor = Convert.ToInt32(Minuta_Text.Text);
                int Kaldyk = Hor % 10;
                if (Kaldyk == 10)
                {
                    if (Hor == 00)
                    {
                        Minuta_Text.Text = "50";
                    }
                    else
                    {
                        Hor -= 10;
                        if (Hor < 10)
                        {
                            Minuta_Text.Text = "0" + Hor.ToString();
                        }
                        else
                        {
                            Minuta_Text.Text = Hor.ToString();
                        }
                    }
                }
                else
                {
                    Hor = Hor - Kaldyk;
                    if (Hor == 00)
                    {
                        Minuta_Text.Text = "50";
                    }
                    else
                    {
                        Hor -= 10;
                        if (Hor < 10)
                        {
                            Minuta_Text.Text = "0" + Hor.ToString();
                        }
                        else
                        {
                            Minuta_Text.Text = Hor.ToString();
                        }
                    }
                }
             
            }
        }

        private void Button_Hourse_Text_inc_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Name == "Button_Hourse_Text_inc")
            {
                int Hor = Convert.ToInt32(Hourse_Text.Text);
                if (Hor == 23)
                {
                    Hourse_Text.Text = "00";
                }
                else
                {
                    Hor++;
                    if (Hor < 10)
                    {
                        Hourse_Text.Text = "0" + Hor.ToString();
                    }
                    else
                    {
                        Hourse_Text.Text = Hor.ToString();
                    }
                }
            }
            else if (button.Name == "Button_Hourse_Text_inc1")
            {
                int Hor = Convert.ToInt32(Minuta_Text.Text);
                int Kaldyk = Hor % 10;
                if (Kaldyk == 10)
                {
                    if (Hor == 50)
                    {
                        Minuta_Text.Text = "00";
                    }
                    else
                    {
                        Hor += 10;
                        if (Hor < 10)
                        {
                            Minuta_Text.Text = "0" + Hor.ToString();
                        }
                        else
                        {
                            Minuta_Text.Text = Hor.ToString();
                        }
                    }
                }
                else
                {
                    Hor = Hor - Kaldyk;
                    if (Hor == 50)
                    {
                        Minuta_Text.Text = "00";
                    }
                    else
                    {
                        Hor += 10;
                        if (Hor < 10)
                        {
                            Minuta_Text.Text = "0" + Hor.ToString();
                        }
                        else
                        {
                            Minuta_Text.Text = Hor.ToString();
                        }
                    }
                }
            }
        }

        private void Button_Dec_Day_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Name == "Button_Dec_Day")
            {
                int Hor = Convert.ToInt32(Date_Day.Text);
                if (Hor == 01)
                {
                    Date_Day.Text = "31";
                }
                else
                {
                    Hor--;
                    if (Hor < 10)
                    {
                        Date_Day.Text = "0" + Hor.ToString();
                    }
                    else
                    {
                        Date_Day.Text = Hor.ToString();
                    }
                }
            }
            else if (button.Name == "Button_Dec_Month")
            {
                int Hor = Convert.ToInt32(Date_Month.Text);
                if (Hor == 01)
                {
                    Date_Month.Text = "12";
                }
                else
                {
                    Hor--;
                    if (Hor < 10)
                    {
                        Date_Month.Text = "0" + Hor.ToString();
                    }
                    else
                    {
                        Date_Month.Text = Hor.ToString();
                    }
                }
            }
            else if (button.Name == "Button_Dec_Year")
            {
                int Hor = Convert.ToInt32(Date_Year.Text);            
                    Hor--;
                    Date_Year.Text =Hor.ToString();
                
            }
        }

        private void Button_Inc_Day_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Name == "Button_Inc_Day")
            {
                int Hor = Convert.ToInt32(Date_Day.Text);
                if (Hor == 31)
                {
                    Date_Day.Text = "01";
                }
                else
                {
                    Hor++;
                    if (Hor < 10)
                    {
                        Date_Day.Text = "0" + Hor.ToString();
                    }
                    else
                    {
                        Date_Day.Text = Hor.ToString();
                    }
                }
            }
            else if (button.Name == "Button_Inc_Month")
            {
                int Hor = Convert.ToInt32(Date_Month.Text);
                if (Hor == 12)
                {
                    Date_Month.Text = "01";
                }
                else
                {
                    Hor++;
                    if (Hor < 10)
                    {
                        Date_Month.Text = "0" + Hor.ToString();
                    }
                    else
                    {
                        Date_Month.Text = Hor.ToString();
                    }
                }
            }
            else if (button.Name == "Button_Inc_Year")
            {
                int Hor = Convert.ToInt32(Date_Year.Text);
                Hor++;
                Date_Year.Text = Hor.ToString();

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            Date_Year.Text = dateTime.Year.ToString();
            Date_Month.Text = dateTime.Month.ToString();
            Date_Day.Text = dateTime.Day.ToString();
            Hourse_Text.Text = dateTime.Hour.ToString();
            Minuta_Text.Text = dateTime.Minute.ToString();
        }
    }
}
