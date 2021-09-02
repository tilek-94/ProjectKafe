using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeProject.Models
{
    public class HistoryFood
    {
        public int Id { get; set; }
        public string FoodName { get; set; }
        public int FoodCount { get; set; }
        public int CheckId { get; set; }
        public double FoodPrice { get; set; }
        public int Gram { get; set; }
    }
}
