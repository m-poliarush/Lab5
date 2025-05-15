using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogic.Models;
using DomainData.UoW;
using MenuManager.DB.Models;

namespace BusinessLogic.Services
{
    public class OrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public List<OrderBusinessModel> GetAllOrders()
        {
            List<OrderBusinessModel> result = new List<OrderBusinessModel>();

            var orders = _unitOfWork.OrdersRepository.GetAll(o => o.dishes);

            foreach (var order in orders)
            {
                result.Add(_mapper.Map<OrderBusinessModel>(order));
            }
            return result;
        }
        public void CreateOrder(OrderBusinessModel order)
        {
            var orderEntity = _mapper.Map<Order>(order);

            _unitOfWork.OrdersRepository.Create(orderEntity);
            _unitOfWork.Save();
        }
    }
}
