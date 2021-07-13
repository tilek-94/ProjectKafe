using KafeProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KafeProject.ViewModels.Base;
using KafeProject.Infrastructure.Commands;
using KafeProject.Date;
using System.Windows;

namespace KafeProject.ViewModels
{
    class NaKuxneViewModel : ViewModel
    {
        private ObservableCollection<MenuFoodParams> _FoodList = new ObservableCollection<MenuFoodParams>();
        public ObservableCollection<MenuFoodParams> FoodList
        {
            get { return _FoodList; }
            set { Set(ref _FoodList, value); }
        }
        private MenuFoodParams _Food = new MenuFoodParams();
        public MenuFoodParams Food
        {
            get { return _Food; }
            set { Set(ref _Food, value); }
        }
        int id = 0;
        public NaKuxneViewModel()
        {
            FoodList = new ObservableCollection<MenuFoodParams>();
            MenuFoodViewModel.paramsForKitchen += MethodForDelegate;
            //+= MethodForDelegate;
        }
        void MethodForDelegate(ObservableCollection<MenuFoodParams> x, int idForCheck) 
        {
            FoodList = x;
            id = idForCheck;
        }
        ~NaKuxneViewModel() => MenuFoodViewModel.paramsForKitchen -= MethodForDelegate;
        private LambdaCommand _SendCheckCommand;
        public LambdaCommand SendCheckCommand =>
            _SendCheckCommand ?? (_SendCheckCommand = new LambdaCommand(ExecuteSendCheckCommand, CanExecuteSendCheckCommand));

        void ExecuteSendCheckCommand(object p)
        {
            MessageBox.Show("привет");
            //if (id == 0) 
            //{
            //    using (ApplicationContext db = new ApplicationContext())
            //    {
            //        //g = db.Checks?.Where(b => b.TableId == p && b.DateTimeCheck > DateTime.Now.Date)?.Select(p => p.Id).OrderBy(a => a)?.LastOrDefault() ?? 0;

            //    }

            //    foreach (var i in FoodList) 
            //    {
            //        //i
            //    }
            //}
            //Application.Current.Shutdown();
        }
        bool CanExecuteSendCheckCommand(object p)
        {
            return true;

        }
    }
}
