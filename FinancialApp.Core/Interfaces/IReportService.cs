using System;
using System.Collections.Generic;
using System.Text;
using FinancialApp.Core.Entities;
using FinancialApp.Core.Models;

namespace FinancialApp.Core.Interfaces
{
    public interface IReportService
    {
        ServiceResult<SummaryResult> GetSummary();

        ServiceResult<IReadOnlyList<Account>> GetAccounts();
    }
}
