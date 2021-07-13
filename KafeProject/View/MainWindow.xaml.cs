using KafeProject.All_Windows;
using KafeProject.View.All_Windows;
using KafeProject.View.User_Menu;
using System.Windows;

namespace KafeProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void MessageForCheck(int x);
        public static event MessageForCheck menuCheck_;
        private int Id { get; set; }
        public MainWindow(int id=0)
        {
            InitializeComponent();
            MenuStol menuFood = new MenuStol(id);
            GlawMenu.Children.Add(menuFood);
        }
        ~MainWindow() => clearingDelegatesFromBaktiar();
        void clearingDelegatesFromBaktiar()
        {
            
            MenuStol.menuStol_ -= menuStolMessage;
            Kolichestvo_Bluda.menuStolForDynamicCheck_ -= menuStolForDynamicCheck;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MenuStol.menuStol_ += menuStolMessage;
            Kolichestvo_Bluda.menuStolForDynamicCheck_ += menuStolForDynamicCheck;
        }
        void menuStolForDynamicCheck(int tableId,int guestCount) 
        {
            MessageBox.Show(tableId + " " + guestCount);
            GlawMenu.Children.Clear();
            MenuFood m = new MenuFood();
            GlawMenu.Children.Add(m);
        }
        void menuStolMessage(int checkIdForMainWindow) 
        {
            MessageBox.Show(checkIdForMainWindow + "");
            GlawMenu.Children.Clear();
            MenuFood m = new MenuFood();
            GlawMenu.Children.Add(m);
            menuCheck_(checkIdForMainWindow);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            clearingDelegatesFromBaktiar();
            this.Close();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            clearingDelegatesFromBaktiar();
            Parol_Window parol_Window = new Parol_Window();
            parol_Window.Show();
            this.Close();
        }
    }
}
