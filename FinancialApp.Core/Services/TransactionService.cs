using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinancialApp.Core.Entities;
using FinancialApp.Core.Interfaces;

namespace FinancialApp.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<Transaction> _transactionRepository;
        private readonly IRepository<Account> _accountRepository;

        public TransactionService(IRepository<Transaction> transactionRepository, IRepository<Account> accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }
        public ServiceResult<IReadOnlyList<Transaction>> GetTransactions(DateTime startDate, int amount = 5)
        {
            var transactions = _transactionRepository
                .Filter(t => t.TransactionDate.Month == startDate.Month && t.TransactionDate.Year == startDate.Year)
                .OrderByDescending(t => t.TransactionDate)
                .Take((int)amount)
                .ToList();

            return ServiceResult<IReadOnlyList<Transaction>>.SuccessResult(transactions);
        }

        public ServiceResult<Transaction> Add(Transaction transaction)
        {
            var account = _accountRepository.GetById(transaction.AccountId);
            if (account == null)
            {
                return ServiceResult<Transaction>.ErrorResult($"No se encontró una cuenta con el id {transaction.AccountId}");
            }

            var result = _transactionRepository.Add(transaction);
            account.Amount += transaction.Amount;
            _transactionRepository.SaveChanges();
            return  ServiceResult<Transaction>.SuccessResult(result);
        }
    }
}
