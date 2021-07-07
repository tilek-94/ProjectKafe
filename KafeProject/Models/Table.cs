using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeProject.Models
{
    public class Table:StandartAbstract
    {
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }


    }
}
