using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinancialApp.Core.Interfaces;
using FinancialApp.Data;

namespace FinancialApp.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected FinancialAppContext Context { get; }

        protected BaseRepository(FinancialAppContext context)
        {
            Context = context;
        }
        public abstract IReadOnlyList<TEntity> Filter(Func<TEntity, bool> predicate);

        public IReadOnlyList<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public abstract TEntity GetById(long id);
        public TEntity Add(TEntity entity)
        {
           var entry = Context.Add(entity);
           return entry.Entity;
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}
