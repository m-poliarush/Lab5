using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuManager.DB.Models;

namespace BusinessLogic.Models
{
    public class OrderBusinessModel
    {
        public int OrderID { get; set; }

        public int TotalCost { get; private set; }

        private ObservableCollection<BaseMenuItem> _dishes = new();
        public ObservableCollection<BaseMenuItem> dishes
        {
            get => _dishes;
        }


        public void AddDish(BaseMenuItem dish)
        {
            _dishes.Add(dish);
            UpdateTotalCost();
        }
        public void RemoveDish(BaseMenuItem dish)
        {
            _dishes.Remove(dish);
            UpdateTotalCost();
        }
        public void ClearOrderList()
        {
            _dishes.Clear();
            UpdateTotalCost();
        }

        // Оновлення вартості замовлення
        private void UpdateTotalCost()
        {
            TotalCost = _dishes.Sum(dish => dish.Price);
        }
    }
}
