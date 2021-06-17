using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialApp.API.Models
{
    public class AccountDto
    {
        public long AccountId { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public string Currency { get; set; }
    }
}
