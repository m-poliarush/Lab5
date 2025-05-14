using DomainData.Repository;
using MenuManager.DB.Models;

namespace DomainData.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        public GenericRepository<DailyMenu> MenusRepository { get; }
        public GenericRepository<Dish> DishRepository { get; }
        public GenericRepository<ComplexDish> ComplexDishRepository { get; }
        public GenericRepository<Order> OrdersRepository { get; }

        public void Save();
    }
}
