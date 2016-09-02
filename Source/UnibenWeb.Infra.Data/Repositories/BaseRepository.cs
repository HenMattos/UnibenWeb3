using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using UnibenWeb.Domain.Interfaces.Repository;
using UnibenWeb.Infra.Data.Context;
using UnibenWeb.Infra.Data.Interfaces;

namespace UnibenWeb.Infra.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity: class
    {
        private readonly ContextManager _contextManager = ServiceLocator.Current.GetInstance<IContextManager>() as ContextManager;
        protected readonly UnibenContext Context = new UnibenContext();
        protected DbSet<TEntity> DbSet;

        public BaseRepository()
        {
            Context = _contextManager.GetContext();
            DbSet = Context.Set<TEntity>();
        }

        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual TEntity FindByID(int id)
        {
            return DbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll(int skip, int take)
        {
            return DbSet.ToList().Skip(skip).Take(take);
        }

        public virtual void Update(TEntity obj)
        {
            var entry = Context.Entry(obj);
            DbSet.Attach(obj);
            entry.State = EntityState.Modified;
        }

        public virtual void Remove(TEntity obj)
        {
            Context.Entry(obj).State = EntityState.Deleted;
        }

        public virtual IEnumerable<TEntity> SearchWhere(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public virtual void AttachObject(TEntity obj)
        {
            var entry = Context.Entry(obj);
            DbSet.Attach(obj);
        }

        public virtual void SaveChanges()
        {
            Context.SaveChanges();
        }

        public virtual void Dispose()
        {
            Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
