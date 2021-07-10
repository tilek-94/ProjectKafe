
using KafeProject.Date;
using KafeProject.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace KafeProject.View.All_Windows
{
    /// <summary>
    /// Логика взаимодействия для Parol_Window.xaml
    /// </summary>
    public partial class Parol_Window : Window
    {
        
        public Parol_Window()
        {
            InitializeComponent();
            PayWindowVM vm = new PayWindowVM();
            this.DataContext = vm;
            if (vm.CloseAction == null)
                vm.CloseAction = new Action(this.Close);

            
        }

        
        //vv
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           /* Button button = sender as Button;
            if (Password_text.Password.Length < 4)
            {
                Password_text.Password += button.Content.ToString();
            }*/
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            /*if (Password_text.Password != String.Empty)
            {
                Password_text.Password = Password_text.Password.Substring(0, Password_text.Password.Length - 1);
            }*/
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           /* Password_text.Password = String.Empty;*/
        }

        private void Password_text_PasswordChanged(object sender, RoutedEventArgs e)
        {
            /*if (Password_text.Password.Length > 3)
            {
                bd = new DataBase();
                bd.del += db =>
                 {
                     if (db.Rows.Count > 0)
                     {
                         viewModels.OffID = db.Rows[0][0].ToString();
                         viewModels.OffName = db.Rows[0][5].ToString();
                         viewModels.OffPass = db.Rows[0][6].ToString();
                     }
                 };
                bd.SoursData("Select * from add_ofissiant where ofissiant_password = '" + Password_text.Password + "'");
                if (viewModels.OffPass != null && Password_text.Password == viewModels.OffPass)
                {
                    Stol_Window s = new Stol_Window();
                    s.Show();
                    this.Close();

                }
                else
                {
                    Error error = new Error();
                    error.ShowDialog();
                    // MessageBox.Show("Неправильый пароль");
                    Password_text.Password = "";
                }
            }*/
        }
    }
}
