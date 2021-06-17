using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialApp.API.Models
{
    public class TransactionDto
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public string Account { get; set; }

        public double Amount { get; set; }

        public DateTime Date { get; set; }
    }
}
