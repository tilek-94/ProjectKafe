using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeProject.Models
{
    public class ACheck
    {
        public int CheckID { get; set; }
        public string CheckTable { get; set; }
        public string CheckPrice { get; set; }
        public string CheckDate { get; set; }
        public string IdCheck { get; set; }
        public int CheckStatus { get; set; }
    }
}
