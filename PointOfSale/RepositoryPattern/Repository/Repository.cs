using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataSets.Interfaces;
using Microsoft.EntityFrameworkCore;
using PointOfSale.Data;

namespace PointOfSale.RepositoryPattern.Repository
{
    public class Repository<T> : IRepository<T> where T: class
    {
        private readonly DbSet<T> _dbSet;
        public Repository(ApplicationDbContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
        }
        public T Get(Guid id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> queryable = _dbSet;
            if (filter != null)
            {
                queryable = queryable.Where(filter);
            }

            if (includeProperties == null) return queryable.ToList();
            queryable = includeProperties.Split(new[] {','},
                StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(queryable, (current, item) 
                    => current.Include(item));

            return orderBy != null ? orderBy(queryable).ToList() : queryable.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> queryable = _dbSet;
            if (filter != null)
            {
                queryable = queryable.Where(filter);
            }

            if (includeProperties == null) return queryable.FirstOrDefault();
            queryable = includeProperties.Split(new []{','},
                    StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(queryable, (current, item)
                    => current.Include(item));

            return queryable.FirstOrDefault();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Remove(Guid id)
        {
            _dbSet.Remove(_dbSet.Find(id));
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
