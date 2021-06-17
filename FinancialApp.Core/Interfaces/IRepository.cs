using System;
using System.Collections.Generic;
using System.Text;

namespace FinancialApp.Core.Interfaces
{
    public interface IRepository<TEntity>
    {
        IReadOnlyList<TEntity> Filter(Func<TEntity, bool> predicate);

        IReadOnlyList<TEntity> GetAll();

        TEntity GetById(long id);

        TEntity Add(TEntity entity);

        int SaveChanges();
    }
}
