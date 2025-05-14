using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuManager.DB.Models
{
    public class ComplexDish : BaseMenuItem
    {
        public List<Dish> DishList { get; set; } = new();
        public void AddDish(Dish dish)
        {
            if(dish != null)
            {
                DishList.Add(dish);
            }
        }
        public ComplexDish()
        {

        }
        public ComplexDish(List<Dish> list)
        {
            DishList = list;
        }
    }
}
