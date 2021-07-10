using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KafeProject.View.Components
{
    /// <summary>
    /// Interaction logic for CompPasswordBox.xaml
    /// </summary>
    public partial class CompPasswordBox : UserControl
    {
        public static readonly DependencyProperty 
            PasswordProperty =
            DependencyProperty.Register("Password",
                typeof(string),
                typeof(CompPasswordBox),
                new PropertyMetadata(string.Empty));

        public CompPasswordBox()
        {
            InitializeComponent();
        }

        public string Password
        {
            get {
                return (string)GetValue(PasswordProperty);
            }
            set {
                SetValue(PasswordProperty, value);
            }
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = passwordBox.Password;
        }
    }
}
