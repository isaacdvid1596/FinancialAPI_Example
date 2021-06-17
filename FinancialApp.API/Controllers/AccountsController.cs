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
    public class AccountsController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly ITransactionService _transactionService;

        public AccountsController(IReportService reportService, ITransactionService transactionService)
        {
            _reportService = reportService;
            _transactionService = transactionService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AccountDto>> GetAccounts()
        {
            var serviceResult = _reportService.GetAccounts();
            if (serviceResult.ResponseCode != ResponseCode.Success)
            {
                return BadRequest(serviceResult.Error);
            }

            return Ok(serviceResult.Result.Select(r => new AccountDto
            {
                Amount = r.Amount,
                Name = r.Name,
                Currency = r.Currency,
                AccountId = r.Id
            }));
        }

        [HttpPost("{accountId}/transactions")]
        public ActionResult<TransactionDto> Post(int accountId, [FromBody] TransactionDto transactionDto)
        {
            var transaction = new Transaction
            {
                Amount = transactionDto.Amount,
                TransactionDate = transactionDto.Date,
                Description = transactionDto.Description,
                AccountId = accountId
            };

           var serviceResult = _transactionService.Add(transaction);
           if (serviceResult.ResponseCode != ResponseCode.Success)
           {
               return BadRequest(serviceResult.Error);
           }

           transactionDto.Id = serviceResult.Result.Id;
           return Ok(transactionDto);
        }
    }
}
