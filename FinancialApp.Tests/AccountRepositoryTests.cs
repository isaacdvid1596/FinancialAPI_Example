using System;
using System.Collections.Generic;
using System.Text;
using FinancialApp.Infrastructure.Repositories;
using Xunit;

namespace FinancialApp.Tests
{
    
    public class AccountRepositoryTests
    {
        [Theory]
        [InlineData(1,true)]
        [InlineData(100,false)]
        public void GetById_ExistingId_ReturnsCorrectAccount(long id,bool expectedResult)
        {

            //arrange

            var context = DbContextUtils.GetInMemoryContext();
            context.SeedAccounts();
            var accountRepository = new AccountRepository(context);

            //act
            var account = accountRepository.GetById(id);

            //assert

            if (expectedResult)
            {
                Assert.NotNull(account);
                Assert.Equal(id, account.Id);
            }
            else
            {
                Assert.Null(account);
            }
        }

        [Fact]
        public void Filter_ValidPredicate_ReturnsCorrectAccounts()
        {
            //arrange

            var context = DbContextUtils.GetInMemoryContext();
            context.SeedAccounts();
            var accountRepository = new AccountRepository(context);

            //act

            var accounts = accountRepository.Filter(x => x.Currency == "USD");

            //assert

            Assert.Contains(accounts, a => a.Currency == "USD");

        }
    }
}
