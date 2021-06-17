using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinancialApp.Core.Entities;
using FinancialApp.Core.Interfaces;
using FinancialApp.Data;
using Microsoft.EntityFrameworkCore;

namespace FinancialApp.Infrastructure.Repositories
{
    public class TransactionRepository : BaseRepository<Transaction>
    {
        public TransactionRepository(FinancialAppContext context) 
         : base(context)
        {
        }
        public override IReadOnlyList<Transaction> Filter(Func<Transaction, bool> predicate)
        {
            return Context.Transaction.Include(t => t.Account)
                .Where(predicate).ToList();
        }

        public override Transaction GetById(long id)
        {
            return Context.Transaction.FirstOrDefault(x => x.Id == id);
        }
    }
}
