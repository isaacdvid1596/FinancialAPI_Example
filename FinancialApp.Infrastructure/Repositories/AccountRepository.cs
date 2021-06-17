using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinancialApp.Core.Entities;
using FinancialApp.Data;

namespace FinancialApp.Infrastructure.Repositories
{
    public class AccountRepository : BaseRepository<Account>
    {
        public AccountRepository(FinancialAppContext context) : base(context)
        {
        }

        public override IReadOnlyList<Account> Filter(Func<Account, bool> predicate)
        {
            return Context.Account.Where(predicate).ToList();
        }

        public override Account GetById(long id)
        {
            return Context.Account.FirstOrDefault(x => x.Id == id);
;        }
    }
}
