using KafeProject.Infrastructure.Commands;
using KafeProject.Date;
using System;
using System.Linq;
using System.Threading.Tasks;
using KafeProject.Models;
using MySql.Data.MySqlClient;
using System.Windows;

namespace KafeProject.ViewModels
{
    public class PayWindowVM : ClassForChane
    {
        private bool CanCloseApplicationExecat(object p) => true;
        MainWindow mainWindow;
        internal PayWindowVM()
        {
            ButtonForAutorization = new LambdaCommand(ClickButton, CanCloseApplicationExecat);
            ButtonForClear = new LambdaCommand(ClickClear, CanCloseApplicationExecat);
            LoadBase();
        }

        private async void LoadBase()
        {
            await Task.Run(() => DateL());
        }

        private void DateL()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
            }
        }
        private void ClickButton(object p)
        {
            int Id = 0;
            if (p.ToString() == "Удалить")
                Password = "";

            if (p.ToString() != "Удалить")
                Password += p.ToString();

            if (Password.Length == 4 || Password.Length > 4)
            {
               // MessageBox.Show(DateTime.Now.ToString());
                using (MySqlConnection connection = new MySqlConnection("datasource=localhost; port=3306;Initial Catalog='basakafe';username=kafe;password=1;CharSet=utf8;"))
                {
                    connection.Open();
                    string value = "";
                    MySqlCommand command = new MySqlCommand("SELECT id FROM `basakafe`.`waiters` WHERE pass='0000' LIMIT 1", connection);
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        value = reader[0].ToString();
                        break;
                    }
                    connection.Close();
                    Id = value!=""?Convert.ToInt32(value) :0;
                    if (Id > 0)
                    {
                        Password = "";
                        // id = Id;
                        connection.Open();
                        string value1 = "";
                        command = new MySqlCommand("SELECT id FROM `basakafe`.`regime` WHERE waiterId=2 LIMIT 1", connection);
                        reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            value1 = reader[0].ToString();
                            break;
                        }
                        connection.Close();
                        if (value1=="")
                        {
                            connection.Open();
                            command = new MySqlCommand("INSERT INTO `basakafe`.`regime` (`waiterId`,`startTime`,`endTime`) VALUES ('"+Id+"',CURRENT_TIMESTAMP(),CURRENT_TIMESTAMP());", connection);
                            reader = command.ExecuteReader();
                            connection.Close();
                        }
                        mainWindow = new MainWindow(Id);
                        mainWindow.Show();

                        CloseAction();
                    }
                    Password = "";

                }

                //using (ApplicationContext db = new ApplicationContext())
                //{

                //    Id = db.Waiters.Where(t => t.Pass == Password).Select(t => t.Id).FirstOrDefault();
                //    if (Id > 0)
                //    {
                //        Password = "";
                //        id = Id;
                //        if (db.Regime.Where(i => i.WaiterId == Id && i.StartTime < DateTime.Now).Count() == 0)
                //        {
                //            Regimes regime = new Regimes { StartTime = DateTime.Now, EndTime = DateTime.Now, WaiterId = Id };
                //            db.Regime.Add(regime);
                //            db.SaveChanges();
                //        }
                //        mainWindow = new MainWindow(Id);
                //        mainWindow.Show();

                //        CloseAction();
                //    }
                //    Password = "";

                //}

            }

        }
        private void ClickClear(object p)
        {
            if (Password.Length > 0)
                Password = Password[0..^1];
        }

    }
}
