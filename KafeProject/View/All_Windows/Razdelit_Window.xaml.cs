using KafeProject.ViewModels;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
namespace KafeProject.All_Windows
{
    /// <summary>
    /// Логика взаимодействия для Razdelit_Window.xaml
    /// </summary>
    public partial class Razdelit_Window : Window
    {
        public Razdelit_Window()
        {
            InitializeComponent();
            MainWindow.timer.Stop();
            Error.clsRW += cl;
        }
        void cl() 
        {
            Error.clsRW -= cl;
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.t = 0;
            MainWindow.timer.Start();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //string s = "SELECT fio FROM joktoo";
            //connection.Open();
            //MySqlCommand cmd = connection.CreateCommand();
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = s;
            //cmd.ExecuteNonQuery();
            //DataTable dta1 = new DataTable();
            //MySqlDataAdapter dataadap = new MySqlDataAdapter(cmd);
            //dataadap.Fill(dta1);
            //Datagrid_Spisok.ItemsSource = dta1.DefaultView;
            //connection.Close();
        }

        private void Datagrid_Spisok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*  (sender as ListBox).SelectedValue = null;*/
        }
    }
}
