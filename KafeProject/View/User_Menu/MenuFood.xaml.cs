using KafeProject.Date;
using KafeProject.Models;
using KafeProject.View.User_Menu;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace KafeProject.View.User_Menu
{
    /// <summary>
    /// Логика взаимодействия для MenuFood.xaml
    /// </summary>
    public partial class MenuFood : UserControl
    {
        string categoyButtonId;
        string categoyButtonName;
        public delegate void ButtonClick(MenuFoodParams x);
        public static event ButtonClick foodEvent;
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
            glawMenuMethod();
        }
        void glawMenuMethod(object sender = null, RoutedEventArgs e = null)
        {
            TovarMenu.Children.Clear();
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var i in db.Foods.Where(f => f.Id == f.ParentCategoryId))
                {
                    Grid grid = new Grid();
                    grid.Margin = new Thickness(10, 10, 0, 35);
                    grid.Height = 155;
                    grid.Width = double.NaN;

                    Image im = new Image();
                    im.MouseDown += new MouseButtonEventHandler(allCategory);
                    im.Style = (Style)this.TryFindResource("Image_Style");
                    im.Source = new BitmapImage(new Uri("/Images/FoodImage/se.png", UriKind.RelativeOrAbsolute));
                    im.Uid = i.Id.ToString();
                    StackPanel st = new StackPanel();
                    st.Style = (Style)this.TryFindResource("StackPanel_Style");

                    TextBlock text1 = new TextBlock();
                    text1.Style = (Style)this.TryFindResource("TextBlock_Style");
                    text1.Text = i.Name;
                    grid.Children.Add(im);
                    st.Children.Add(text1);
                    grid.Children.Add(st);
                    TovarMenu.Children.Add(grid);
                }
            }
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
            Kategory_button_dynamic();
            glawMenuMethod();
        }
        private void Dynamik_but(object sender, RoutedEventArgs e)
        {
            allCategory(Convert.ToInt32((sender as Button).Uid));
        }
        private void allCategory(object sender=null, RoutedEventArgs e = null)
        {
            TovarMenu.Children.Clear();
            string uidImage = (sender as Image).Uid.ToString();
            using (ApplicationContext db = new ApplicationContext())
            {
                //(sender as Image).Uid.ToString()
                if (db.Foods.Where(f => Convert.ToInt32(uidImage) == f.ParentCategoryId).Count() == 0)
                {
                    //MessageBox.Show("id еды =" + (sender as Image).Uid.ToString());
                    Kategory_button_dynamic();
                    glawMenuMethod();
                    var foodInfo = db.Foods.Where(f => f.Id== Convert.ToInt32(uidImage));
                    foodEvent(new MenuFoodParams 
                    {Name =foodInfo.Select(p=>p.Name).OrderBy(p=>p).LastOrDefault(),
                    Count=1,
                    Price=foodInfo.Select(p=>p.Price).OrderBy(p=>p).LastOrDefault(),
                    Id= Convert.ToInt32(uidImage)
                    });
                }
                else
                {
                    Kategory_button_dynamic();
                    categoryButMethod(Convert.ToInt32(uidImage));
                    foreach (var i in db.Foods.Where(f => Convert.ToInt32(uidImage) == f.ParentCategoryId))
                    {
                        Grid grid = new Grid();
                        grid.Margin = new Thickness(10, 10, 0, 35);
                        grid.Height = 155;
                        grid.Width = double.NaN;

                        Image im = new Image();
                        im.MouseDown += new MouseButtonEventHandler(allCategory);
                        im.Style = (Style)this.TryFindResource("Image_Style");
                        im.Source = new BitmapImage(new Uri("/Images/FoodImage/se.png", UriKind.RelativeOrAbsolute));
                        im.Uid = i.Id.ToString();
                        StackPanel st = new StackPanel();
                        st.Style = (Style)this.TryFindResource("StackPanel_Style");

                        TextBlock text1 = new TextBlock();
                        text1.Style = (Style)this.TryFindResource("TextBlock_Style");
                        text1.Text = i.Name;
                        grid.Children.Add(im);
                        st.Children.Add(text1);
                        grid.Children.Add(st);
                        TovarMenu.Children.Add(grid);

                    }
                }
            }
        }
        private void allCategory(int x)
        {
            TovarMenu.Children.Clear();
            string? uidImage = x.ToString();
            //(sender as Image).Uid.ToString();
            using (ApplicationContext db = new ApplicationContext())
            {
                //(sender as Image).Uid.ToString()
                if (db.Foods.Where(f => Convert.ToInt32(uidImage) == f.ParentCategoryId).Count() == 0)
                {
                    //MessageBox.Show("id еды =" + (sender as Image).Uid.ToString());
                    Kategory_button_dynamic();
                    glawMenuMethod();
                    
                }
                else
                {
                    Kategory_button_dynamic();
                    categoryButMethod(Convert.ToInt32(uidImage));
                    foreach (var i in db.Foods.Where(f => Convert.ToInt32(uidImage) == f.ParentCategoryId))
                    {
                        Grid grid = new Grid();
                        grid.Margin = new Thickness(10, 10, 0, 35);
                        grid.Height = 155;
                        grid.Width = double.NaN;

                        Image im = new Image();
                        im.MouseDown += new MouseButtonEventHandler(allCategory);
                        im.Style = (Style)this.TryFindResource("Image_Style");
                        im.Source = new BitmapImage(new Uri("/Images/FoodImage/se.png", UriKind.RelativeOrAbsolute));
                        im.Uid = i.Id.ToString();
                        StackPanel st = new StackPanel();
                        st.Style = (Style)this.TryFindResource("StackPanel_Style");

                        TextBlock text1 = new TextBlock();
                        text1.Style = (Style)this.TryFindResource("TextBlock_Style");
                        text1.Text = i.Name;
                        grid.Children.Add(im);
                        st.Children.Add(text1);
                        grid.Children.Add(st);
                        TovarMenu.Children.Add(grid);

                    }
                }
            }
        }
        void categoryButMethod(int idBut)
        {
            //Kategory_button_dynamic();
            using (ApplicationContext db = new ApplicationContext())
            {
                categoyButtonId = categoyButtonId + " " + idBut.ToString();
                categoyButtonName = categoyButtonName + " " + db.Foods.Where(f => f.Id == idBut).Select(l => l.Name).OrderBy(o => o).LastOrDefault();
                int dynamicId = db.Foods.Where(f => f.Id == idBut).Select(l => l.ParentCategoryId).OrderBy(o => o).LastOrDefault();
                if (dynamicId == idBut)
                {
                    creatingButtonsMethod();
                    return;
                }
                categoryButMethod(dynamicId);
            }
        }
        void creatingButtonsMethod()
        {
            categoyButtonId = categoyButtonId.Trim();
            categoyButtonName = categoyButtonName.Trim();
            MessageBox.Show(categoyButtonName);
            var idCategoryButtons = categoyButtonId.Split().Select(int.Parse).ToList();
            var nameCategoryButtons = categoyButtonName.Split();
            for (int i = 0; i < idCategoryButtons.Count(); i++)
            {
                Button butt = new Button();
                butt.Style = (Style)this.TryFindResource("Button_Kategory1");
                if (nameCategoryButtons[i].Length < 15)
                    for (; nameCategoryButtons[i].Length < 20;)
                        nameCategoryButtons[i] += ". ";
                butt.Content = nameCategoryButtons[i];
                butt.Click += new RoutedEventHandler(Dynamik_but);
                //butt.Tag = (KategoryMenu.Children.Count).ToString();
                butt.Uid = idCategoryButtons[i].ToString();
                KategoryMenu.Children.Add(butt);

            }
            categoyButtonId = "";
            categoyButtonName = "";
        }
        public void Kategory_button_dynamic()
        {
            KategoryMenu.Children.Clear();
            //VerticalAlignment="Center" Height="42" HorizontalAlignment="Left" Width="130.5" Margin="25,0"   Style="{DynamicResource Button_Kategory}" Click="Vse_Tovar_Click"
            Button butt = new Button();
            butt.Style = (Style)this.TryFindResource("Button_Kategory");
            butt.VerticalAlignment = VerticalAlignment.Center;
            butt.Uid = "0";
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
