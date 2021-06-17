using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinancialApp.Core.Entities;
using FinancialApp.Core.Interfaces;
using FinancialApp.Core.Models;

namespace FinancialApp.Core.Services
{
    public class ReportService : IReportService
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IRepository<Account> _accountRepository;

        public ReportService(IRepository<Transaction> transactionRepository, IRepository<Account> accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }
        public ServiceResult<SummaryResult> GetSummary()
        {
            var incomeTransactions = _transactionRepository.Filter(x => x.Amount > 0);
            var expensesTransactions = _transactionRepository.Filter(x => x.Amount < 0);
            var income = incomeTransactions.Sum(x => x.Amount * x.Account.ConversionRate);
            var expenses = expensesTransactions.Sum(x => x.Amount * x.Account.ConversionRate);
            return ServiceResult<SummaryResult>.SuccessResult(new SummaryResult
            {
                TotalIncome = income,
                TotalExpenses = expenses,
                Total = income + expenses
            });
        }

        public ServiceResult<IReadOnlyList<Account>> GetAccounts()
        {
            var accounts = _accountRepository.GetAll();
            return ServiceResult<IReadOnlyList<Account>>.SuccessResult(accounts);
        }
    }
}
