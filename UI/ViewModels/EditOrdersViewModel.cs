using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuManager.DB;
using MenuManager.DB.Models;
using MenuManager.Repository.OrdersRepository;

namespace Lab4.ViewModels
{
    public class EditOrdersViewModel
    {
        private OrdersRepository ordersRepository;
        public List<Order> Orders { get; }
        public EditOrdersViewModel(MenuContext context)
        {
            ordersRepository = new(context);
            Orders = ordersRepository.GetOrders().ToList();
        }

    }
}
