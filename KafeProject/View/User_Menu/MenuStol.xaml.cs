using KafeProject.All_Windows;
using KafeProject.Date;
using KafeProject.View.All_Windows;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace KafeProject.View.User_Menu
{
    /// <summary>
    /// Логика взаимодействия для MenuStol.xaml
    /// </summary>
    public partial class MenuStol : UserControl
    {
        int currentWaiter;
        string usersName;
        bool ifItIsHim;
        int selectedLocationId;
        string err;
        public delegate void Message1(int x);
        public static event Message1 menuStol_;
        public MenuStol(int x=0)// чтобы узнать какой официант
        {
            InitializeComponent();
            currentWaiter = x;
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Parol_Window parol_Window = new Parol_Window();
            parol_Window.Show();
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Knopki();
            Knopki_kategoryy();
        }
        void button_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Uid == "-2") 
            {
                int checkIdForTable = 0;
                //selectedLocationId
                //(sender as Button).Content;
                using (ApplicationContext db = new ApplicationContext())
                {
                    checkIdForTable=db.Checks.Where(g => g.DateTimeCheck > DateTime.Now.Date &&
                    g.TableId ==
                        db.Tables.Where(h =>
                        h.LocationId == selectedLocationId &&
                        h.Name == (sender as Button).Content.ToString()
                        ).Select(l => l.Id).OrderBy(j => j).LastOrDefault()
                    ).Select(d=>d.Id).OrderBy(s=>s).LastOrDefault();
                }
                menuStol_(checkIdForTable);
                return;
            }
            Kolichestvo_Bluda kolichestvo_Bluda = new Kolichestvo_Bluda(Convert.ToInt32((sender as Button).Uid));
            kolichestvo_Bluda.ShowDialog();
        }
        
        void button_Category_Click(object sender, RoutedEventArgs e)
        {
            dynamicButton(sender);
        }
        public void Knopki()
        {
            dynamicButton();
        }
        public void Knopki_kategoryy()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var t in db.Locations)
                {
                    Button button = new Button();
                    button.Style = (Style)this.TryFindResource("Button_Kategory1");
                    string catName = t.Name;
                    if (catName.Length < 10)
                        for (; catName.Length < 15;)
                            catName += ". ";
                    button.Tag = t.Id;
                    button.Content = catName;
                    button.Click += new RoutedEventHandler(button_Category_Click);
                    Stol_Category.Children.Add(button);
                }
            }
        }
        private bool methodForResult(int p) 
        {
            int g = 0;
            using (ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    g =db.Checks?.Where(b => b.TableId == p&&b.DateTimeCheck> DateTime.Now.Date)?.Select(p => p.Id).OrderBy(a=>a)?.LastOrDefault() ?? 0;
                    g = db.Checks?.Where(b => b.Id == g && b.DateTimeCheck > DateTime.Now.Date)?.Select(p => p.Status).OrderBy(a => a)?.LastOrDefault() ?? 0;
                    usersName = db.Waiters.Where(rt => rt.Id == db.Checks.Where(b => b.TableId == p && b.DateTimeCheck > DateTime.Now.Date).Select(l => l.WaiterId).OrderBy(t => t).LastOrDefault()).OrderBy(t => t.Id).Last().Name;

                    if (db.Checks.Where(b => b.TableId == p && b.DateTimeCheck > DateTime.Now.Date).Select(s=>s.WaiterId).OrderBy(t => t).LastOrDefault() == currentWaiter)
                        ifItIsHim = true;
                    else
                        ifItIsHim = false;
                    if(g!=0)
                        return true;
                    return false;
                }
                catch (Exception ex)
                {
                    err =usersName+" "+ ex.Message;
                    return false;
                }
                
            }
           
        }
        void dynamicButton(object sender = null)
        {
            Stol_Panel.Children.Clear();
            int? k = 1;
            if (sender != null)
                k = Convert.ToInt32((sender as Button).Tag);
            else
                using (ApplicationContext db = new ApplicationContext())
                {
                    k = db.Locations.Select(t=>t.Id).OrderBy(tt => tt).FirstOrDefault();
                }
            selectedLocationId = k ?? 1;
            using (ApplicationContext db = new ApplicationContext())
            {
                foreach (var tg in db.Tables.Where(t => t.LocationId == k))
                {
                    int p = tg.Id;
                    bool checkStatus = methodForResult(p);
                    Button butt = new Button();
                    butt.Style = (Style)this.TryFindResource("Button_Dynamik_Stol");
                    butt.Content = tg.Name;
                    butt.Tag = "пусто";
                    butt.Uid = p.ToString();
                    if (checkStatus)
                    {
                        butt.Tag = usersName;
                        if (ifItIsHim)
                        {
                            butt.Uid = "-2";
                        }
                        else
                        {
                            butt.Uid = "-1";
                            butt.IsEnabled = false;
                        }
                    }
                    butt.Click += new RoutedEventHandler(button_Click);
                    Stol_Panel.Children.Add(butt);
                }
            }
        }
    }
}
