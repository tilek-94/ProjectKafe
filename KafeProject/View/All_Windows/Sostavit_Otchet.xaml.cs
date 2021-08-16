using System.Windows;
using System.Windows.Input;

namespace KafeProject.All_Windows
{
    /// <summary>
    /// Логика взаимодействия для Sostavit_Otchet.xaml
    /// </summary>
    public partial class Sostavit_Otchet : Window
    {
        public Sostavit_Otchet()
        {
            InitializeComponent();
            MainWindow.timer.Stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.t = 0;
            MainWindow.timer.Start();
            this.Close();
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Chasy chasy = new Chasy();
            chasy.ShowDialog();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            
        }
    }
}
