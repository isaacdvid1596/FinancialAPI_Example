using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinancialApp.Core;
using FinancialApp.Core.Entities;
using FinancialApp.Core.Interfaces;
using FinancialApp.Core.Services;
using Moq;
using Xunit;

namespace FinancialApp.Tests
{
    public class ReportServiceTests
    {
        [Fact]
        public void GetSummary_NonUSDTransactions_AreConvertedToUSD()
        {

            //arrange

            /*
             * metodo tiene dependencias de transaction repository
             * se procede a falsificarlas con moq, y se crean metodos falsos
             * cuando alguien invoque filter, se invoca lo que esta en linea 71
             * se hace lo mismo para account pero en ese caso solo se declara el mock ya que no
             * se usa en el metodo get summary en el repo original.
             */

            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    Account = new Account()
                    {
                        Amount = 500,
                        ConversionRate = 0.25,
                        Name = "Account 1",
                        Currency = "HNL"
                    },
                    TransactionDate = DateTime.Today,
                    Amount = 100,
                    Description = "Test1"
                },new Transaction
                {
                    Account = new Account()
                    {
                        Amount = 400,
                        ConversionRate = 0.25,
                        Name = "Account 1",
                        Currency = "HNL"
                    },
                    TransactionDate = DateTime.Today,
                    Amount = 200,
                    Description = "Test2"
                },new Transaction
                {
                    Account = new Account()
                    {
                        Amount = 600,
                        ConversionRate = 0.25,
                        Name = "Account 1",
                        Currency = "HNL"
                    },
                    TransactionDate = DateTime.Today,
                    Amount = 300,
                    Description = "Test3"
                },
            };

            var transactionRepositoryMock = new Mock<IRepository<Transaction>>();
            transactionRepositoryMock.Setup(t => t.Filter(It.IsAny<Func<Transaction, bool>>()))
                .Returns((Func<Transaction, bool> p) => transactions.Where(p).ToList());

            var accountRepositoryMock = new Mock<IRepository<Account>>();
            var reportService = new ReportService(transactionRepositoryMock.Object, accountRepositoryMock.Object);



            //act

            var summary = reportService.GetSummary();

            //assert
            var positiveTransaction = transactions.Where(x => x.Amount > 0).Sum(x => x.Amount * x.Account.ConversionRate);
            var negativeTransaction = transactions.Where(x => x.Amount < 0).Sum(x => x.Amount * x.Account.ConversionRate);

            Assert.Equal(positiveTransaction,summary.Result.TotalIncome);
            Assert.Equal(negativeTransaction,summary.Result.TotalExpenses);
            Assert.Equal(positiveTransaction+negativeTransaction,summary.Result.Total);
            Assert.True(summary.ResponseCode == ResponseCode.Success);

        }

        [Fact]
        public void GetAccounts_ReturnsAllAccounts()
        {
            //arrange 

            var accounts = new List<Account>()
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
                },
                new Account
                {
                    Amount = 400,
                    ConversionRate = 0.24,
                    Currency = "USD",
                    Name = "Account 3"
                },
            };

            var transactionRepositoryMock = new Mock<IRepository<Transaction>>();
            var accountRepositoryMock = new Mock<IRepository<Account>>();
            accountRepositoryMock.Setup(a => a.GetAll())
                .Returns(accounts);

            var reportService = new ReportService(transactionRepositoryMock.Object, accountRepositoryMock.Object);

            //act

            var result = reportService.GetAccounts();

            //assert

            Assert.Equal(accounts.Count,result.Result.Count);
            Assert.True(result.ResponseCode == ResponseCode.Success);

            foreach (var account in accounts)
            {
                Assert.Contains(accounts, x => x.Id == account.Id);
            }
            
        }
    }
}
