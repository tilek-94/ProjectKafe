
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
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
            try
            {
                DataGrid dt = sender as DataGrid;
                DataRowView selection = dt.SelectedItem as DataRowView;
                if (Datagrid_Spisok.SelectedItem != null)
                {
                    selection.Row.Delete();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int count = Convert.ToInt32(Count_Gost.Text);
            if (count>0)
            {
                count--;
                Count_Gost.Text = count.ToString();
                int count1 = Convert.ToInt32(Count_New_Stol.Text);
                count1++;
                Count_New_Stol.Text = count1.ToString();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int count = Convert.ToInt32(Count_New_Stol.Text);
            if (count > 0)
            {
                count--;
                Count_New_Stol.Text = count.ToString();
                int count1 = Convert.ToInt32(Count_Gost.Text);
                count1++;
                Count_Gost.Text = count1.ToString();
            }
        }
    }
}
