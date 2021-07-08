using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using KafeProject.Date;
using KafeProject.Infrastructure.Commands;
using KafeProject.Models;

namespace KafeProject.ViewModel
{
    class MenuFoodViewModel  : ViewModels.Base.ViewModel
    {
        private ObservableCollection<MenuFoodParams> _FoodList = new ObservableCollection<MenuFoodParams>();        public ObservableCollection<MenuFoodParams> FoodList
        {
            get { return _FoodList; }
            set { Set(ref _FoodList, value); }
        }

        public MenuFoodViewModel()
        {
            FoodList = new ObservableCollection<MenuFoodParams>();
            using (ApplicationContext db = new ApplicationContext())
            {
                var ord = db.Orders.Where(d => d.CheckId == 1);

                var result = ord.Join(db.Foods,
                    p => p.FoodId,
                    t => t.Id,
                    (p, t) => new { Name = t.Name, Count = p.CountFood, CheckId = p.CheckId, Price = t.Price });

                foreach (var s in result)
                {
                    FoodList.Add(new MenuFoodParams { Name = s.Name, Count = s.Count, Price = s.Count * s.Price });
                }
            }
        }

        private LambdaCommand _TestCommand;
        public LambdaCommand TestCommand =>
            _TestCommand ?? (_TestCommand = new LambdaCommand(ExecuteCommandName, CanExecuteCommandName));

        void ExecuteCommandName(object p)
        {
            Application.Current.Shutdown();
        }
        
        bool CanExecuteCommandName(object p)
        {
            return true;

        }
    }
}
