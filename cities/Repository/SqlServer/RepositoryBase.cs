﻿using cities.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace cities.Repository.SqlServer
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDbContext _context;

        public RepositoryBase(AppDbContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IEnumerable<T> FindAll(bool trackChanges) =>
            !trackChanges ?
              _context.Set<T>()
                .AsNoTracking() :
              _context.Set<T>();

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression,
        bool trackChanges,
        Func<IEnumerable<T>, IIncludableQueryable<T, object>> include = null)
        {
            // Get query
            IQueryable<T> query = _context.Set<T>();

            // Apply filter
            query = query.Where(expression);

            // Include
            if (include != null)
            {
                query = include(query);
            }

            // Tracking
            if (!trackChanges)
                query.AsNoTracking();

            return query;
        }
    }
}
