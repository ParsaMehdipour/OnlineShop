using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace _0_Framework.Domain
{
    public interface IRepsoitory<TKey,TEntity> where TEntity : class
    {
        TEntity GetById(TKey id);
        List<TEntity> Get();
        void Create(TEntity entity);
        bool Exists(Expression<Func<TEntity, bool>> expression);
        void SaveChanges();
    }
}
