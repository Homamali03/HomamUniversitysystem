using Hope.DomainEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly HopeUrhContext context;
        private DbSet<TEntity> dbSet;

        public Repository()
        {
            this.context = new HopeUrhContext();
            this.dbSet = context.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            dbSet.Add(entity);
            context.SaveChanges();
        }
        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.Update(entity);
            context.SaveChanges();
        }
        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            dbSet.Remove(entity);
            context.SaveChanges();
        }
        public IQueryable<TEntity> GetAll()
        {
            return dbSet.AsQueryable();
        }
        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = dbSet;
            foreach(var item  in includes)
            {
                query = query.Include(item);
            }
            return query.Where(expression);
        }
    }
}