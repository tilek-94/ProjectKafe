using KafeProject.All_Windows;
using KafeProject.Date;
using KafeProject.Infrastructure.Commands;
using KafeProject.View.User_Menu;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace KafeProject.ViewModels
{
    class RazdelitViewModel : Base.ViewModel
    {
        public RazdelitViewModel()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var check = db.Orders.Where(i => i.CheckId == MenuFood.IdCheck).Join(db.Foods,
                    o => o.FoodId,
                    f => f.Id,
                    (o, f) => new RCheck
                    {
                        RID = o.Id,
                        RCheckCount = o.CountFood,
                        RCheckName = f.Name,
                        RFoodID = f.Id
                    });
                StolCount = db.Checks.Where(i => i.Id == MenuFood.IdCheck).Select(i => i.GuestsCount).OrderBy(i => i).FirstOrDefault() - 1;
                RCheckList = new ObservableCollection<RCheck>(check);
            }
        }

        private RCheck _SelectedFood = new RCheck();
        public RCheck SelectedFood
        {
            get { return _SelectedFood; }
            set
            {
                if (value != null)
                {
                    RCheckList.Remove(value);
                    if (value.RCheckCount > 1)
                    {
                        value.RCheckCount = value.RCheckCount - 1;
                        RCheckList.Add(value);
                        RCheckList = new ObservableCollection<RCheck>(RCheckList.OrderBy(f => f.RID));

                    }
                    if (SelectedRCheckList.Count == 0)
                        SelectedRCheckList.Add(new RCheck { RCheckCount = 1, RFoodID = value.RFoodID, RCheckName = value.RCheckName, RID = value.RID });
                    else
                    {
                        foreach (var t in SelectedRCheckList)
                            if (value.RID == t.RID)
                            {
                                var ttt = t;
                                ttt.RCheckCount += 1;
                                SelectedRCheckList.Remove(t);
                                SelectedRCheckList.Add(ttt);
                                SelectedRCheckList = new ObservableCollection<RCheck>(SelectedRCheckList.OrderBy(p => p.RID));
                                return;
                            }
                        SelectedRCheckList.Add(new RCheck { RCheckCount = 1, RFoodID = value.RFoodID, RCheckName = value.RCheckName, RID = value.RID });
                        SelectedRCheckList = new ObservableCollection<RCheck>(SelectedRCheckList.OrderBy(p => p.RID));
                    }
                }
            }
        }
        private RCheck _UnSelectedFood = new RCheck();
        public RCheck UnSelectedFood
        {
            get { return _UnSelectedFood; }
            set
            {
                if (value != null)
                {
                    SelectedRCheckList.Remove(value);
                    if (value.RCheckCount > 1)
                    {
                        value.RCheckCount = value.RCheckCount - 1;
                        SelectedRCheckList.Add(value);
                        SelectedRCheckList = new ObservableCollection<RCheck>(SelectedRCheckList.OrderBy(f => f.RID));

                    }
                    if (RCheckList.Count == 0)
                        RCheckList.Add(new RCheck { RCheckCount = 1, RFoodID = value.RFoodID, RCheckName = value.RCheckName, RID = value.RID });
                    else
                    {
                        foreach (var t in RCheckList)
                            if (value.RID == t.RID)
                            {
                                var ttt = t;
                                ttt.RCheckCount += 1;
                                RCheckList.Remove(t);
                                RCheckList.Add(ttt);
                                RCheckList = new ObservableCollection<RCheck>(RCheckList.OrderBy(p => p.RID));
                                return;
                            }
                        RCheckList.Add(new RCheck { RCheckCount = 1, RFoodID = value.RFoodID, RCheckName = value.RCheckName, RID = value.RID });
                        RCheckList = new ObservableCollection<RCheck>(RCheckList.OrderBy(p => p.RID));
                    }
                }
            }
        }
        private ObservableCollection<RCheck> _RCheckList = new ObservableCollection<RCheck>();

        public ObservableCollection<RCheck> RCheckList
        {
            get
            {

                return _RCheckList;
            }
            set
            {
                Set(ref _RCheckList, value);
            }
        }

        private ObservableCollection<RCheck> _SelectedRCheckList = new ObservableCollection<RCheck>();

        public ObservableCollection<RCheck> SelectedRCheckList
        {
            get
            {
                return _SelectedRCheckList;
            }
            set
            {
                Set(ref _SelectedRCheckList, value);
            }
        }

        private int _StolCount = 10;
        public int StolCount
        {
            get { return _StolCount; }
            set { Set(ref _StolCount, value); }
        }

        private int _RazStolCount = 1;
        public int RazStolCount
        {
            get { return _RazStolCount; }
            set { Set(ref _RazStolCount, value); }
        }

        private LambdaCommand _SendCheckCommand;
        public LambdaCommand SendCheckCommand =>
            _SendCheckCommand ?? (_SendCheckCommand = new LambdaCommand(ExecuteSendCheckCommand, CanExecuteSendCheckCommand));

        void ExecuteSendCheckCommand(object p)
        {
            Error er = new Error(SelectedRCheckList, RCheckList, StolCount, RazStolCount);
            er.ShowDialog();
        }
        bool CanExecuteSendCheckCommand(object p)
        {
            return true;

        }


        private LambdaCommand _PlusStolCommand;
        public LambdaCommand PlusStolCommand =>
            _PlusStolCommand ?? (_PlusStolCommand = new LambdaCommand(ExecutePlusStolCommand, CanExecutePlusStolCommand));

        void ExecutePlusStolCommand(object p)
        {
            StolCount--;
            RazStolCount++;
        }
        bool CanExecutePlusStolCommand(object p)
        {
            if (StolCount > 1)
            {
                return true;
            }
            return false;
        }


        private LambdaCommand _MinusStolCommand;
        public LambdaCommand MinusStolCommand =>
            _MinusStolCommand ?? (_MinusStolCommand = new LambdaCommand(ExecuteMinusStolCommand, CanExecuteMinusStolCommand));

        void ExecuteMinusStolCommand(object p)
        {
            StolCount++;
            RazStolCount--;
        }
        bool CanExecuteMinusStolCommand(object p)
        {
            if (RazStolCount > 1)
            {
                return true;
            }
            return false;
        }
    }
    public class RCheck
    {
        public int RID { get; set; }
        public string RCheckName { get; set; }
        public int RCheckCount { get; set; }
        public int RFoodID { get; set; }
    }

}