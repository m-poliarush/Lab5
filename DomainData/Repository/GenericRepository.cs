using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MenuManager.DB;
using Microsoft.EntityFrameworkCore;

namespace DomainData.Repository
{
    public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : class
    {
        protected readonly DbSet<TModel> _dbSet;

        public GenericRepository(MenuContext context)
        {
            _dbSet = context.Set<TModel>();
        }

        public TModel GetById(int id, params Expression<Func<TModel, object>>[] includes)
        {
            IQueryable<TModel> query = _dbSet;

            foreach (var include in includes)
                query = query.Include(include);

            return query.FirstOrDefault(e => EF.Property<int>(e, "ID") == id);
        }
        public List<TModel> GetAll(params Expression<Func<TModel, object>>[] includes)
        {
            IQueryable<TModel> query = _dbSet;

            foreach (var include in includes)
                query = query.Include(include);

            return query.ToList();
        }

        public void Create(TModel model)
        {
            _dbSet.Add(model);
        }

        public void Update(TModel model)
        {
            _dbSet.Update(model);
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

    }
}
