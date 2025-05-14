using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainData.Repository;
using MenuManager.DB;
using MenuManager.DB.Models;

namespace DomainData.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue;

        private readonly MenuContext menuContext;
        private GenericRepository<DailyMenu> _menusRepository;
        private GenericRepository<Dish> _dishRepository;
        private GenericRepository<ComplexDish> _complexDishRepository;
        private GenericRepository<Order> _ordersRepository;

        public GenericRepository<DailyMenu> MenusRepository => _menusRepository ??= new GenericRepository<DailyMenu>(menuContext);
        public GenericRepository<Dish> DishRepository => _dishRepository ??= new GenericRepository<Dish>(menuContext);
        public GenericRepository<ComplexDish> ComplexDishRepository => _complexDishRepository ??= new GenericRepository<ComplexDish>(menuContext);
        public GenericRepository<Order> OrdersRepository => _ordersRepository ??= new GenericRepository<Order>(menuContext);


        public UnitOfWork(MenuContext context)
        {
            this.menuContext = context;
        }

        public void Save()
        {
            menuContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    menuContext.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~UnitOfWork()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
