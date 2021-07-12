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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Chasy chasy = new Chasy();
            chasy.ShowDialog();
        }
    }
}
