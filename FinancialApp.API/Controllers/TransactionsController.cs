using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinancialApp.API.Models;
using FinancialApp.Core;
using FinancialApp.Core.Entities;
using FinancialApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinancialApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TransactionDto>> Get([FromQuery] int? amount)
        {
            var serviceResult = amount == null
                ? _transactionService.GetTransactions(DateTime.Today)
                : _transactionService.GetTransactions(DateTime.Today, amount.Value);
            if (serviceResult.ResponseCode != ResponseCode.Success)
            {
                return BadRequest(serviceResult.Error);
            }

            return Ok(serviceResult.Result.Select(r => new TransactionDto
            {
                Amount = r.Amount,
                Date = r.TransactionDate,
                Description = r.Description,
                Account = r.Account.Name
            }));
        }
    }
}
