using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialApp.API.Models
{
    public class SummaryDto
    {
        public double TotalIncome { get; set; }

        public double TotalExpenses { get; set; }

        public double Total { get; set; }
    }
}
