using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeProject.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public Food food { get; set; }
        public int ProductId { get; set; }
        public Product product { get; set; }
        public int CountPoduct { get; set; }
    }
}
