using System;
using System.Collections.Generic;
using System.Text;
using FinancialApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialApp.Data.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasData(new List<Account>
            {
                new Account
                {
                    Id = -1,
                    Amount = 1500,
                    Currency = "USD",
                    Name = "Cuenta en dolares 1"
                },
                new Account
                {
                    Id = -2,
                    Amount = 1200,
                    Currency = "EUR",
                    Name = "Cuenta en euros única"
                },
                new Account
                {
                    Id = -3,
                    Amount = 500,
                    Currency = "USD",
                    Name = "Cuenta en dolares 2"
                }
            });
        }
    }
}
