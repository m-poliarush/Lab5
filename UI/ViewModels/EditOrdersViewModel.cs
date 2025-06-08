using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Services.Interfaces;
using DomainData.UoW;
using MenuManager.DB;
using MenuManager.DB.Models;
using MenuManager.Repository.OrdersRepository;

namespace Lab4.ViewModels
{
    public class EditOrdersViewModel : BaseViewModel
    {
        private readonly IOrderService _orderService; 
        public List<OrderBusinessModel> Orders { get; }
        public EditOrdersViewModel(IOrderService service)
        {
            _orderService = service;
            Orders = new List<OrderBusinessModel>();
            var ordersEntitis = _orderService.GetAllOrders();
            foreach (var order in ordersEntitis) {
                Orders.Add(order);
            }

        }

    }
}
