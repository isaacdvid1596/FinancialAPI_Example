using System;
using System.Collections.Generic;
using System.Text;
using FinancialApp.API.Controllers;
using FinancialApp.API.Models;
using FinancialApp.Core;
using FinancialApp.Core.Entities;
using FinancialApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FinancialApp.Tests
{
    public class AccountsControllerTests
    {
        [Fact]
        public void GetAccounts_ReturnsOk()
        {
            var accounts = new List<Account>
            {
                new Account
                {
                    Amount = 200,
                    ConversionRate = 0.24,
                    Currency = "USD",
                    Name = "Account 1"
                },
                new Account
                {
                    Amount = 300,
                    ConversionRate = 0.24,
                    Currency = "USD",
                    Name = "Account 2"
                }
            };

            var transactionService = new Mock<ITransactionService>();
            var reportService = new Mock<IReportService>();
            reportService.Setup(x => x.GetAccounts())
                .Returns(ServiceResult<IReadOnlyList<Account>>.SuccessResult(accounts));

            var controller = new AccountsController(reportService.Object, transactionService.Object);

            var response = controller.GetAccounts();

            Assert.IsType<ActionResult<IEnumerable<AccountDto>>>(response);
            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public void GetAccounts_UnexpectedError_ReturnsBadRequest()
        {
            var transactionService = new Mock<ITransactionService>();
            var reportService = new Mock<IReportService>();
            reportService.Setup(x => x.GetAccounts())
                .Returns(ServiceResult<IReadOnlyList<Account>>.ErrorResult("Error!"));

            var controller = new AccountsController(reportService.Object, transactionService.Object);

            var response = controller.GetAccounts();

            Assert.IsType<ActionResult<IEnumerable<AccountDto>>>(response);
            Assert.IsType<BadRequestObjectResult>(response.Result);
        }
    }
}
