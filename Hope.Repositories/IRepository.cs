using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Repositories
{
    public interface IRepository <TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> GetAll();

        // IEnumerable<TEntity> GetAll2();
        IQueryable<TEntity> Find(Expression<Func<TEntity,bool>> expression,params Expression<Func<TEntity, object>>[] includes);

    }
}
