using System;
using System.Linq.Expressions;
using Core.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using var context = new TContext();
            context.Set<TEntity>().Add(entity);
            context.SaveChanges();

            
        }

        public void Delete(TEntity entity)
        {
            using var context = new TContext();
            context.Set<TEntity>().Remove(entity);
            context.SaveChanges();

            
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using var context = new TContext();
            return context.Set<TEntity>().FirstOrDefault(filter);
           
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
             using var context = new TContext();
             return filter is null
                ? context.Set<TEntity>().ToList()
                : context.Set<TEntity>().Where(filter).ToList();

            
        }

        public void Update(TEntity entity)
        {
            using var context = new TContext();
            context.Set<TEntity>().Update(entity);
            context.SaveChanges(); 
        }
    }
}

