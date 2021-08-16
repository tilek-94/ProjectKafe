using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using KafeProject.ViewModels.Base;

namespace KafeProject.ViewModels
{
    public class ClassForChane: ViewModel
    {
        
        public Action CloseAction { get; set; }
        public ICommand ButtonForAutorization { get; set; }
        public ICommand ButtonForClear { get; set; }


    }
}
