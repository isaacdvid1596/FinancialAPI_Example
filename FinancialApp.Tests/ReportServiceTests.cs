using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinancialApp.Core;
using FinancialApp.Core.Entities;
using FinancialApp.Core.Interfaces;
using FinancialApp.Core.Services;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Moq;
using Xunit;

namespace FinancialApp.Tests
{
    public class ReportServiceTests
    {

        //moq
        [Fact]
        public void GetSummary_NonUSDTransactions_AreConvertedToUSD()
        {
            //arrange
            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    Account = new Account
                    {
                        Amount = 500,
                        ConversionRate = 0.25,
                        Name = "Account 1",
                        Currency = "HNL"
                    },
                    TransactionDate = DateTime.Today,
                    Amount = 100,
                    Description = "Test 1"
                },
                new Transaction
                {
                    Account = new Account
                    {
                        Amount = 2500,
                        ConversionRate = 0.25,
                        Name = "Account 2",
                        Currency = "HNL"
                    },
                    TransactionDate = DateTime.Today,
                    Amount = 400,
                    Description = "Test 1"
                },
                new Transaction
                {
                    Account = new Account
                    {
                        Amount = 1000,
                        ConversionRate = 0.25,
                        Name = "Account 3",
                        Currency = "HNL"
                    },
                    TransactionDate = DateTime.Today,
                    Amount = 200,
                    Description = "Test 1"
                }
            };

            var transactionRepositoryMock = new Mock<IRepository<Transaction>>();
            transactionRepositoryMock.Setup(t => t.Filter(It.IsAny<Func<Transaction, bool>>()))
                .Returns((Func<Transaction, bool> p) => transactions.Where(p).ToList());

            var accountRepositoryMock = new Mock<IRepository<Account>>();
            var reportService = new ReportService(transactionRepositoryMock.Object, accountRepositoryMock.Object);

            //act
            var summary = reportService.GetSummary();

            //assert
            var positiveTransactions = transactions.Where(x => x.Amount > 0).Sum(x => x.Amount * x.Account.ConversionRate);
            var negativeTransactions = transactions.Where(x => x.Amount < 0).Sum(x => x.Amount * x.Account.ConversionRate);

            Assert.True(summary.ResponseCode == ResponseCode.Success);
            Assert.Equal(positiveTransactions, summary.Result.TotalIncome);
            Assert.Equal(negativeTransactions, summary.Result.TotalExpenses);
            Assert.Equal(positiveTransactions + negativeTransactions, summary.Result.Total);
        }

        [Fact]
        public void GetAccounts_ReturnsAllAccounts()
        {
            //arrange
            var accounts = new List<Account>
            {
                new Account
                {
                    Amount = 200,
                    ConversionRate = 0.24,
                    Currency = "USD",
                    Name = "Account 1"
                },
                new Account
                {
                    Amount = 300,
                    ConversionRate = 0.24,
                    Currency = "USD",
                    Name = "Account 2"
                }
            };

            var transactionRepositoryMock = new Mock<IRepository<Transaction>>();
            var accountRepositoryMock = new Mock<IRepository<Account>>();
            accountRepositoryMock.Setup(x => x.GetAll())
                .Returns(accounts);

            var reportService = new ReportService(transactionRepositoryMock.Object, accountRepositoryMock.Object);
            
            //act
            var result = reportService.GetAccounts();

            //assert
            Assert.True(result.ResponseCode == ResponseCode.Success);
            Assert.Equal(accounts.Count, result.Result.Count);
            foreach (var account in result.Result)
            {
                Assert.Contains(accounts, x => x.Id == account.Id);
            }
        }
    }
}
