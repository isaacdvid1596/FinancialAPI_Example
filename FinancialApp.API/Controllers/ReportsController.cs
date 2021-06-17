using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialApp.API.Models;
using FinancialApp.Core;
using FinancialApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinancialApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("summary")]
        public ActionResult<SummaryDto> GetSummary()
        {
            var serviceResult = _reportService.GetSummary();
            if (serviceResult.ResponseCode != ResponseCode.Success)
            {
                return BadRequest(serviceResult.Error);
            }

            return Ok(new SummaryDto
            {
                TotalExpenses = serviceResult.Result.TotalExpenses,
                TotalIncome = serviceResult.Result.TotalIncome,
                Total = serviceResult.Result.Total
            });
        }
    }
}
