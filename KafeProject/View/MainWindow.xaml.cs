using KafeProject.All_Windows;
using KafeProject.Date;
using KafeProject.View.All_Windows;
using KafeProject.View.User_Menu;
using KafeProject.ViewModels;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace KafeProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static DispatcherTimer timer = new DispatcherTimer();
        public static int t = 0,timeNull = 0;

        public delegate void MessageForCheck(int checkId, int tableId, int guestCount,int idWaiter);
        public static event MessageForCheck menuCheck_;
        public delegate void CloseAll(int i=0);
        public static event CloseAll fuckAll_;
        public delegate void CloseAllDel(int i = 0);
        public static event CloseAllDel clsd;
        
        public static int Id { get; set; }

        
        public MainWindow(int id=0)
        {
            InitializeComponent();
            //MessageBox.Show(DateTime.Now.ToString());
            Id = id;
            MenuStol menuFood = new MenuStol(id);
            GlawMenu.Children.Add(menuFood);
            NameoF();

        }
        void NameoF()
        {
            using (ApplicationContext db = new ApplicationContext()) 
            {
               OffName.Text = db.Waiters.Where(i => i.Id == Id).Select(i=>i.Name).OrderBy(i => i).LastOrDefault();
            }  

        }
        ~MainWindow() => clearingDelegatesFromBaktiar();
        void clearingDelegatesFromBaktiar()
        {
            MenuStol.menuStol_ -= menuStolMessage;
            Kolichestvo_Bluda.menuStolForDynamicCheck_ -= menuStolForDynamicCheck;
            Oplatit.menuStolOpen -= menuStolOpenForEndCheck;
            ChecksLogic.menuStol_ -= OpenCheck;
            MenuCheck.addStol -= menuStolOpenForEndCheck;
            MenuFoodViewModel.openStolWindow -= menuStolOpenForEndCheck;
            Error.clsRW -= menuStolOpenForEndCheck;
            User_Ofissiant.closeAll_ -= CloseSmena;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Error.clsRW += menuStolOpenForEndCheck;
            MenuFoodViewModel.showMessage += () => MessageBox.Show("!!! нету тов");
            //
            MenuStol.menuStol_ += menuStolMessage;
            Kolichestvo_Bluda.menuStolForDynamicCheck_ += menuStolForDynamicCheck;
            Oplatit.menuStolOpen += menuStolOpenForEndCheck;
            ChecksLogic.menuStol_ += OpenCheck;
            MenuCheck.addStol += menuStolOpenForEndCheck;////
            MenuFoodViewModel.openStolWindow += menuStolOpenForEndCheck;
            User_Ofissiant.closeAll_ += CloseSmena;

            timeCheck();

            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            
            
        }
        async void  timeCheck()
        {
            await Task.Run(()=> {
                using (ApplicationContext connetc = new ApplicationContext())
                {
                    timeNull = Convert.ToInt32(connetc.Options.Where(a => a.Key == "TimeValue").Select(s => s.Value).FirstOrDefault());

                }
            });   
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            t += 1;
            OffName1.Text = t.ToString();
            if (t == timeNull)
            {
                Button_Click_1(sender,new RoutedEventArgs());
                timer.Stop();
            }
        }

        void CloseSmena(int i) 
        {
            if (i==1)
            {
                if (clsd != null)
                {
                    clsd();
                }
                clearingDelegatesFromBaktiar();
                Parol_Window parol_Window = new Parol_Window();
                parol_Window.Show();
                this.Close();
            }
            else
            {
                Popup_Ofissiant.Visibility = Visibility.Hidden;
                Popup_Ofissiant.IsOpen = false;
            }
        }
        void OpenCheck(int x)
        {
            GlawMenu.Children.Clear();
            if (clsd != null)
            {
                clsd();
            }
            MenuFood m = new MenuFood(x);
            GlawMenu.Children.Add(m);
            menuCheck_(x, 0, 0, Id);
            if (MenuFood.IdCheck != 0)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Xheck.Text = db.Checks.Where(i => i.Id == MenuFood.IdCheck).Select(i => i.CheckCount)?.OrderBy(i => i)?.LastOrDefault().ToString() ?? "";
                }
            }
            else
            {
                Xheck.Text = "";
            }
            
        }
        void menuStolOpenForEndCheck()
        {
            GlawMenu.Children.Clear();
            if (clsd!=null)
            {
                clsd();
            }
            if (fuckAll_!=null)
            {
                fuckAll_(1);
            }    
            
            MenuStol menuFood = new MenuStol(Id);
            GlawMenu.Children.Add(menuFood);
            Xheck.Text = "";
            
        }
        void menuStolForDynamicCheck(int tableId,int guestCount) 
        {
            GlawMenu.Children.Clear();
            if (clsd!=null)
            {
                clsd();
            }
            
            MenuFood m = new MenuFood(0,tableId, guestCount);
            GlawMenu.Children.Add(m);
            menuCheck_(0, tableId, guestCount,Id);
            if (MenuFood.IdCheck != 0)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Xheck.Text = db.Checks.Where(i => i.Id == MenuFood.IdCheck).Select(i => i.CheckCount)?.OrderBy(i => i)?.LastOrDefault().ToString() ?? "";
                }
            }
            else
            {
                Xheck.Text = "";
            }
            
        }
        void menuStolMessage(int checkIdForMainWindow) 
        {
            GlawMenu.Children.Clear();
            if (clsd!=null)
            {
                clsd();
            }
            
            MenuFood m = new MenuFood(checkIdForMainWindow);
            GlawMenu.Children.Add(m);
            menuCheck_(checkIdForMainWindow,0,0, Id);
            if (MenuFood.IdCheck != 0)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Xheck.Text = db.Checks.Where(i => i.Id == MenuFood.IdCheck).Select(i => i.CheckCount)?.OrderBy(i => i)?.LastOrDefault().ToString() ?? "";
                }
            }
            else
            {
                Xheck.Text = "";
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(fuckAll_!=null) 
                fuckAll_();
            clearingDelegatesFromBaktiar();
            timer.Stop();
            this.Close();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            timer.Tick -= new EventHandler(timer_Tick);
            if (fuckAll_!=null)
                fuckAll_();
            clearingDelegatesFromBaktiar();
            Parol_Window parol_Window = new Parol_Window();
            parol_Window.Show();
            this.Close();
        }
        private void Check_Button_Click(object sender, RoutedEventArgs e)
        {

            MenuCheck m = new MenuCheck();
            m.ShowDialog();
            //Popup_Ofissiant.PlacementTarget = (sender as Button);
            //Popup_Ofissiant.Placement = PlacementMode.Bottom;
            //if (Popup_Ofissiant.IsOpen == false)
            //{
            //    Popup_Ofissiant.IsOpen = true;
            //}
            //else
            //{
            //    Popup_Ofissiant.IsOpen = false;
            //}
        }

        private async void Stol_Button_Click(object sender, RoutedEventArgs e)
        {
            GlawMenu.Children.Clear(); 
            if (clsd != null)
            {
                clsd();
            }
            MenuStol menuFood = new MenuStol(Id);
            
            GlawMenu.Children.Add(menuFood);
            
            
           
            Xheck.Text = "";
            
        }

        private  void Ofissiant_Button1111_Click(object sender, RoutedEventArgs e)
        {
            if (clsd!=null)
            {
                clsd();
            }
            GlawMenu.Children.Clear();
            MenuFood m = new MenuFood(0, 0, 1);
            GlawMenu.Children.Add(m);
            menuCheck_(0, 0, 1, Id);
            if (MenuFood.IdCheck != 0)
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    Xheck.Text = db.Checks.Where(i => i.Id == MenuFood.IdCheck).Select(i => i.CheckCount)?.OrderBy(i => i)?.LastOrDefault().ToString() ?? "";
                }
            }
            else
            {
                Xheck.Text = "";
            }
            
        }
        private void Ofissiant_Button_Click(object sender, RoutedEventArgs e)
        {
            //Popup_Ofissiant
            Popup_Ofissiant.PlacementTarget = Ofissiant_Button;
            Popup_Ofissiant.Placement = PlacementMode.Bottom;
            if (Popup_Ofissiant.IsOpen == false)
            {
                Popup_Ofissiant.IsOpen = true;
            }
            else
            {
                Popup_Ofissiant.IsOpen = false;
            }
        }

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            t = 0;
        }
    }
}
