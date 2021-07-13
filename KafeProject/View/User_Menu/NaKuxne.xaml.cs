using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KafeProject.View.User_Menu
{
    /// <summary>
    /// Логика взаимодействия для NaKuxne.xaml
    /// </summary>
    public partial class NaKuxne : UserControl
    {
          public NaKuxne()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(@"osk.exe");
            Komentary komentary = new Komentary();
            komentary.Owner = Application.Current.MainWindow;
            komentary.ShowDialog();
           
        }
    }
}
