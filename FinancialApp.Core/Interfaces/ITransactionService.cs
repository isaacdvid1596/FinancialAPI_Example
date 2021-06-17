using System;
using System.Collections.Generic;
using System.Text;
using FinancialApp.Core.Entities;

namespace FinancialApp.Core.Interfaces
{
    public interface ITransactionService
    {
        ServiceResult<IReadOnlyList<Transaction>> GetTransactions(DateTime startDate, int amount = 5);

        ServiceResult<Transaction> Add(Transaction transaction);
    }
}
