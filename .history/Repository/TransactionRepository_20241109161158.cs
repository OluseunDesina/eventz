using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_api_eventz.Data;
using web_api_eventz.Interfaces;
using web_api_eventz.Models;
using Newtonsoft.Json;

namespace web_api_eventz.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;
        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TicketHistory?> BuyTicket(TicketHistory ticketHistory)
        {
            await _context.TicketHistorys.AddAsync(ticketHistory);
            var obj = new
            {
                userId = ticketHistory.AppUserId,
                transactionId = ticketHistory.Id,
                timeStamp = ticketHistory.PurchasedAt
            };
            string json = JsonConvert.SerializeObject(obj);
            // ticketHistory.Code = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));
            // ticketHistory.Code = "Code goes here";
            await _context.SaveChangesAsync();
            return ticketHistory;
        }

        public async Task<List<TicketHistory>> GetMyTicketHistory(string userId)
        {
            return await _context.TicketHistorys.AsQueryable().Where(th => th.AppUserId == userId).ToListAsync();
        }

        public async Task<List<TicketHistory>> GetTicketHistory()
        {
            return await _context.TicketHistorys.ToListAsync();
        }

        public async Task<TicketHistory?> GetTicketHistoryById(int id)
        {
            var transaction = await _context.TicketHistorys.FirstOrDefaultAsync(th => th.Id == id);
            if (transaction == null)
            {
                return null;
            }
            return transaction;
        }
    }
}