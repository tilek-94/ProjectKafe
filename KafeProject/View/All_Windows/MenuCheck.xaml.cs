
using KafeProject.ViewModels;
using System.Windows;

namespace KafeProject.View.All_Windows
{
    /// <summary>
    /// Логика взаимодействия для MenuCheck.xaml
    /// </summary>
    public partial class MenuCheck : Window
    {
        public delegate void ButtonClick();
        public static event ButtonClick addStol;
        public MenuCheck()
        {
            InitializeComponent();
            MainWindow.timer.Stop();
            MainWindow.fuckAll_ += thisClose;
            ChecksLogic.cls += thisClose;
        }
        void thisClose(int j=0) 
        {
            Close();
            ChecksLogic.cls -= thisClose;
            MainWindow.fuckAll_ -= thisClose;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.t = 0;
            MainWindow.timer.Start();
            //addStol();
            Close();
        }
    }
}
