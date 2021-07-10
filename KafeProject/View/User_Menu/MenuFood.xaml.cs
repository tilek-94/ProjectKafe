using KafeProject.User_Menu;
using KafeProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KafeProject.View.User_Menu
{
    /// <summary>
    /// Логика взаимодействия для MenuFood.xaml
    /// </summary>
    public partial class MenuFood : UserControl
    {
        public MenuFood()
        {
            InitializeComponent();
        }

        private void Stol_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Check_Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Ofissiant_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Menu_Button_Click(object sender, RoutedEventArgs e)
        {
            Popup_Menu.PlacementTarget = Menu_Button;
            Popup_Menu.Placement = PlacementMode.Bottom;
            if (Popup_Menu.IsOpen == false)
            {
                Popup_Menu.IsOpen = true;
            }
            else
            {
                Popup_Menu.IsOpen = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //string s = "SELECT marka,kurs,data FROM cars";
            //connection.Open();
            //MySqlCommand cmd = connection.CreateCommand();
            //cmd.CommandType = CommandType.Text;
            //cmd.CommandText = s;
            //cmd.ExecuteNonQuery();
            //DataTable dta1 = new DataTable();
            //MySqlDataAdapter dataadap = new MySqlDataAdapter(cmd);
            //dataadap.Fill(dta1);
            //dataGridView1.ItemsSource = dta1.DefaultView;
            //connection.Close();
        }

        private void Otpravit_Kuxne_Click(object sender, RoutedEventArgs e)
        {
            Popup_Kuxne.PlacementTarget = Prog_Border;
            Popup_Kuxne.Placement = PlacementMode.Center;
            if (Popup_Kuxne.IsOpen == false)
            {
                Popup_Kuxne.IsOpen = true;
            }
            else
            {
                Popup_Kuxne.IsOpen = false;
            }
        }

        private void Kuxne_Otmena_Click(object sender, RoutedEventArgs e)
        {
            Popup_Kuxne.IsOpen = false;
        }

        private void dataGridView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*try
            {
                DataGrid dt = sender as DataGrid;
                DataRowView selection = dt.SelectedItem as DataRowView;
                if (dataGridView1.SelectedItem != null)
                {
                    if (selection["kurs"].ToString() != null || selection["data"].ToString() != null)
                    {
                        Kolichestvo_Bluda kolichestvo_Bluda = new Kolichestvo_Bluda();
                        kolichestvo_Bluda.ShowDialog();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }*/
        }

        private void Vse_Tovar_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Dynamik_but(object sender, RoutedEventArgs e)
        {
            int c = int.Parse((sender as Button).Tag.ToString());
            //MessageBox.Show(KategoryMenu.Children.Count.ToString());
            for (; c + 1 < KategoryMenu.Children.Count;)
            {
                if (c + 1 == KategoryMenu.Children.Count)
                {
                    MessageBox.Show(KategoryMenu.Children.Count.ToString() + " " + c.ToString());
                    break;
                }
                KategoryMenu.Children.RemoveAt(KategoryMenu.Children.Count - 1);
            }
            //MessageBox.Show(KategoryMenu.Children.Count.ToString()+" "+ (sender as Button).Tag.ToString());
            //Pod_category(sender,e);
            D_B(int.Parse((sender as Button).Uid.ToString()));
        }
        private void D_B(int x)
        { }
        private void Pod_category(object sender, RoutedEventArgs e)
        {
        }
        public void Kategory_button_dynamic()
        {
            KategoryMenu.Children.Clear();
            //VerticalAlignment="Center" Height="42" HorizontalAlignment="Left" Width="130.5" Margin="25,0"   Style="{DynamicResource Button_Kategory}" Click="Vse_Tovar_Click"
            Button butt = new Button();
            butt.Style = (Style)this.TryFindResource("Button_Kategory");
            butt.VerticalAlignment = VerticalAlignment.Center;
            butt.Height = 42;
            butt.HorizontalAlignment = HorizontalAlignment.Left;
            butt.Width = 130.5;
            butt.Margin = new Thickness(25, 0, 0, 0);
            butt.Click += new RoutedEventHandler(Vse_Tovar_Click);
            KategoryMenu.Children.Add(butt);
        }
        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {/*
            Parol_Window parol = new Parol_Window();
            parol.Show();  */     
        }

        private void OpenOplatitWindow(object sender, RoutedEventArgs e)
        {
            Oplatit oplatitwindow = new Oplatit(ItogSumma.Text);
            oplatitwindow.Show();
        }
    }
}
