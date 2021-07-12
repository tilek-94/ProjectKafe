using System.Windows;

namespace KafeProject.View.User_Menu
{
    /// <summary>
    /// Логика взаимодействия для Komentary.xaml
    /// </summary>
    public partial class Komentary : Window
    {
        public Komentary()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Application curApp = Application.Current;
            Window mainWindow = curApp.MainWindow;
            this.Left = mainWindow.Left + (mainWindow.Width - this.ActualWidth) / 2-27;
            this.Top = mainWindow.Top + 25;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Otpravka_Kuxne_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
