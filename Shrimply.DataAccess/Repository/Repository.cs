using Shrimply.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shrimply.DataAccess.Repository.IRepository;
using Shrimply.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Shrimply.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ShrimplyStoreDbContext _shrimplyStoreDbContext;
        internal DbSet<T> _dbSet;
        public Repository(ShrimplyStoreDbContext shrimplyStoreDbContext)
        {
            _shrimplyStoreDbContext = shrimplyStoreDbContext;
            _dbSet = _shrimplyStoreDbContext.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            IQueryable<T> query;
            if (tracked)
            {
                query = _dbSet;
            }
            else
            {
                query = _dbSet.AsNoTracking();
            }
            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) 
                {
                    query = query.Include(property);
                }
            }
            return query.ToList();
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
