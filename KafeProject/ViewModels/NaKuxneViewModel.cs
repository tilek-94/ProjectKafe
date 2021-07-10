using KafeProject.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KafeProject.ViewModels.Base;

namespace KafeProject.ViewModels
{
    class NaKuxneViewModel : ViewModel
    {
        public static ObservableCollection<MenuFoodParams> _FoodList = new ObservableCollection<MenuFoodParams>();
        public ObservableCollection<MenuFoodParams> FoodList
        {
            get { return _FoodList; }
            set { Set(ref _FoodList, value); }
        }

        public NaKuxneViewModel()
        {
            FoodList = _FoodList;
        }    
    }
}
