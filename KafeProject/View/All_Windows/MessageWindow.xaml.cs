using System;
using System.Windows;
using System.Windows.Controls;

namespace KafeProject.All_Windows
{
    /// <summary>
    /// Логика взаимодействия для Klaviatura.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageWindow(string text)
        {
            InitializeComponent();
            TextName.Text = text;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
