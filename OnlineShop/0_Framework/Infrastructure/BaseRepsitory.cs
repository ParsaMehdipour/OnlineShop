using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using _0_Framework.Domain;
using Microsoft.EntityFrameworkCore;

namespace _0_Framework.Infrastructure
{
    public class BaseRepsitory<TKey , TEntity> :IRepsoitory<TKey , TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        public BaseRepsitory(DbContext context)
        {
            _context = context;
        }

        public TEntity GetById(TKey id)
        {
            return _context.Find<TEntity>(id);
        }

        public List<TEntity> Get()
        {
            return _context.Set<TEntity>().ToList();
        }

        public void Create(TEntity entity)
        {
            _context.Add(entity);
        }

        public bool Exists(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().Any(expression);
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
