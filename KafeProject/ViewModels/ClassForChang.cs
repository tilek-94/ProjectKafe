using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using KafeProject.ViewModels.Base;

namespace KafeProject.ViewModels
{
    public class ClassForChane: ViewModel
    {
        private string _Password="";
        public string Password
        {
            get=> _Password;
            set {
                Password2 = value;
                if (_Password.Length < 5)
                {
                    
                    Set(ref _Password, value);
                }

            }
        }

        private string _Password2 = "";
        public string Password2
        {
            get { 
            return new string('*', _Password2.Length);
            }
            set
            {
               Set(ref _Password2, value);

            }
        }
        public Action CloseAction { get; set; }
        public ICommand ButtonForAutorization { get; set; }
        public ICommand ButtonForClear { get; set; }


    }
}
