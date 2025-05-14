using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuManager.DB.Models;

namespace BusinessLogic.Models
{
    public class ComplexDishBusinessModel : BaseMenuItemBusinessModel
    {
        public List<Dish> DishList { get; set; } = new();
        public void AddDish(Dish dish)
        {
            if (dish != null)
            {
                DishList.Add(dish);
            }
        }
        public ComplexDishBusinessModel()
        {

        }
        public ComplexDishBusinessModel(List<Dish> list)
        {
            DishList = list;
        }
    }
}
