using System.Windows;

namespace KafeProject.All_Windows
{
    /// <summary>
    /// Логика взаимодействия для Arhiv_Check.xaml
    /// </summary>
    public partial class Arhiv_Check : Window
    {
        public Arhiv_Check()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Otpravka_Kuxne_Copy_Click(object sender, RoutedEventArgs e)
        {
            Chasy chasy = new Chasy();
            chasy.ShowDialog();
        }
    }
}
