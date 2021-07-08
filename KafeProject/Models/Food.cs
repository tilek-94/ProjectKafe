using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeProject.Models
{
    public class Food:StandartAbstract
    {
       public double Price { get; set; }
       public string Image { get; set; }
       public int ParentCategoryId { get; set; }
    }
}
