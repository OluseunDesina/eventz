using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using web_api_eventz.DTOs.TransactionsDto;
using web_api_eventz.Extensions;
using web_api_eventz.Interfaces;
using web_api_eventz.Mappers;
using web_api_eventz.Models;

namespace web_api_eventz.Controllers
{
    [ApiController]
    [Route("/api/transactions")]
    public class TransactionController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(UserManager<AppUser> userManager, ITransactionRepository transactionRepository)
        {
            _userManager = userManager;
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetTransactions()
        {
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return Unauthorized();
            }
            var transactions =  await _transactionRepository.GetTicketHistory();
            return Ok(transactions);
            // return Ok(transactions.Select(t => t.ToTransactionDto()));
        }

        [HttpPost]
        [Route("/buy-ticket")]
        [Authorize]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDto transaction)
        {
            // Implement transaction logic here
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            var newTransaction = transaction.ToTransactionFromCreateDto();
            if (user == null)
            {
                return Unauthorized();
            }
            newTransaction.AppUserId = user.Id;
            newTransaction.PurchasedAt = DateTime.Now;
            // newTransaction.Code = "Code goes here.";
            var t = await _transactionRepository.BuyTicket(newTransaction);
            if (t == null)
            {
                return StatusCode(500);
            }
            // return Ok(t.ToTransactionDto());
            Console.WriteLine($"transaction: {0}", t);
            return Ok(t);
        }
    }
}