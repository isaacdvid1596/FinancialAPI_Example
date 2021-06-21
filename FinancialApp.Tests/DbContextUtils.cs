using System;
using System.Collections.Generic;
using System.Text;
using FinancialApp.Core.Entities;
using FinancialApp.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace FinancialApp.Tests
{
    public static class DbContextUtils
    {
        public static FinancialAppContext GetInMemoryContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var dbContextOption = new DbContextOptionsBuilder<FinancialAppContext>()
                .UseSqlite(connection)
                .Options;

            var context = new FinancialAppContext(dbContextOption);
            context.Database.EnsureCreated();
            return context;
        }


        public static void SeedAccounts(this FinancialAppContext context)
        {
            context.Add(new Account
            {
                Amount = 500,
                ConversionRate = 0.25,
                Currency = "USD",
                Name = "Account 1"
            });
            context.Add(new Account
            {
                Amount = 550,
                ConversionRate = 0.25,
                Currency = "USD",
                Name = "Account 2"
            });
            context.Add(new Account
            {
                Amount = 450,
                ConversionRate = 1,
                Currency = "HNL",
                Name = "Account 3"
            });

            context.SaveChanges();
        }
    }
}
