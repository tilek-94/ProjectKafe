using System.Windows;
using System.Windows.Controls;

namespace Kafe.All_Windows
{
    /// <summary>
    /// Логика взаимодействия для Calendar_Window.xaml
    /// </summary>
    public partial class Calendar_Window : Window
    {
        public Calendar_Window()
        {
            InitializeComponent();
        }
        private void Caleddar1_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Close();
        }
    }
}
