using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using KafeProject.Date;
using KafeProject.Infrastructure.Commands;
using KafeProject.Models;
using KafeProject.View.User_Menu;
using KafeProject.ViewModels.Base;

namespace KafeProject.ViewModels
{
    class MenuFoodViewModel  : ViewModel
    {

        #region Prop
        private ObservableCollection<MenuFoodParams> _FoodList = new ObservableCollection<MenuFoodParams>();
        public ObservableCollection<MenuFoodParams> FoodList
        {
            get { return _FoodList; }
            set { Set(ref _FoodList, value); }
        }
        private double _ItogSum = 0.00;
        public double ItogSum
        {
            get { return _ItogSum; }
            set { Set(ref _ItogSum, value); }
        }
        private double _ObsSum = 0.00;
        public double ObsSum
        {
            get { return _ObsSum; }
            set { Set(ref _ObsSum, value); }
        }
        private double _PercentSum = 10.00;
        public double PercentSum
        {
            get { return _PercentSum; }
            set { Set(ref _PercentSum, value); }
        }
        #endregion

        public MenuFoodViewModel()
        {
           // string checkStatus;
            ObsSum = 0.00;
            ItogSum = 0.00;
            MainWindow.menuCheck_ += MethodForDelegate;
            MenuFood.foodEvent += MethodForAddFood;
        }
        ~MenuFoodViewModel() => MenuFood.foodEvent -= MethodForAddFood;
        void MethodForAddFood(MenuFoodParams x) 
        {
            for(int y=0;y< FoodList.Count;y++ )
                if (FoodList[y].Id == x.Id) 
                {
                    var dynamic = FoodList[y];
                    FoodList.RemoveAt(y);
                    dynamic.Count += 1;
                    FoodList.Add(dynamic);
                    return;
                }
            FoodList.Add(x);
        }
        private void MethodForDelegate(int id) 
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                FoodList = new ObservableCollection<MenuFoodParams>();
                // checkStatus = db.Checks.Where(b => b.TableId == 1).OrderBy(t => t.Id).LastOrDefault().Status.ToString() ?? "";

                var ord = db.Orders.Where(d => d.CheckId == id);

                var result = ord.Join(db.Foods,
                    p => p.FoodId,
                    t => t.Id,
                    (p, t) => new {id=t.Id, Name = t.Name, Count = p.CountFood, CheckId = p.CheckId, Price = t.Price });

                foreach (var s in result)
                {
                    ItogSum += s.Count * s.Price;
                    ObsSum += ((s.Count * s.Price) / 10) * (PercentSum / 10);
                    FoodList.Add(new MenuFoodParams {Id=s.id, Name = s.Name, Count = s.Count, Price = s.Count * s.Price });
                    //NaKuxneViewModel._FoodList.Add(new Food { Name = s.Name, Count = s.Count, Price = s.Count * s.Price }); ;
                }
                ItogSum += ObsSum;
                //string checkStatus = db.Checks.Where(b => b.TableId == t.Id).OrderBy(t => t.Id).LastOrDefault().Status ?? "";
            }
            MainWindow.menuCheck_ -= MethodForDelegate;
        }
        #region Commands

        #region Check
        public delegate void MessageForKitchen(ObservableCollection<MenuFoodParams> x,int idForCheck=0);
        public static event MessageForKitchen paramsForKitchen;

        private LambdaCommand _SendCheckCommand;
        public LambdaCommand SendCheckCommand =>
            _SendCheckCommand ?? (_SendCheckCommand = new LambdaCommand(ExecuteSendCheckCommand, CanExecuteSendCheckCommand));

        void ExecuteSendCheckCommand(object p)
        {
            if(FoodList.Count>0)
                paramsForKitchen(FoodList,0);
            //Application.Current.Shutdown();
        }
        bool CanExecuteSendCheckCommand(object p)
        {
            return true;

        }
        #endregion

        #region Send

        private LambdaCommand _CheckPrintCommand;
        public LambdaCommand CheckPrintCommand =>
            _CheckPrintCommand ?? (_CheckPrintCommand = new LambdaCommand(ExecuteCheckPrintCommand, CanExecuteCheckPrintCommand));

        void ExecuteCheckPrintCommand(object p)
        {

        }

        bool CanExecuteCheckPrintCommand(object p)
        {
            return true;
        }

        #endregion

        #endregion


    }
}
