using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnibenWeb.Domain.Interfaces.Repository
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        /*
         * TEntity: é uma entidade genérica, declarada como uma classe
         * IDisposable: Interface que poderá ser destruida
         */

        void Add(TEntity obj);
        TEntity FindByID(int id);
        IEnumerable<TEntity> GetAll(int skip, int take);
        void Update(TEntity obj);
        void Remove(TEntity obj);
        IEnumerable<TEntity> SearchWhere(Expression<Func<TEntity, bool>> predicate);
        void AttachObject(TEntity obj);
        void SaveChanges();

    }
}
