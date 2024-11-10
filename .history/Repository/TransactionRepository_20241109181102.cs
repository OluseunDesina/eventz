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
        private readonly IEventRepository _eventRepository;
        private readonly ITicketRepository _ticketRepo;
        public TransactionRepository(
            ApplicationDbContext context,
            ITicketRepository ticketRepo,
            IEventRepository eventRepository
        )
        {
            _context = context;
            _ticketRepo = ticketRepo;
            _eventRepository = eventRepository;
        }
        public async Task<TicketHistory?> BuyTicket(TicketHistory ticketHistory)
        {
            var ticket = await _ticketRepo.GetTicketById(ticketHistory.TicketId);
            var eventExist = await _eventRepository.EventExist(ticketHistory.EventId);
            if (eventExist == false || ticket == null || ticket.Quantity < ticketHistory.Quantity || (ticket.Price * ticketHistory.Quantity) < ticketHistory.TotalAmount || ticket.EventID != ticketHistory.EventId)
            {
                return null;
            }

            await _context.TicketHistorys.AddAsync(ticketHistory);
            var obj = new
            {
                userId = ticketHistory.AppUserId,
                transactionId = ticketHistory.Id,
                timeStamp = ticketHistory.PurchasedAt
            };
            string json = JsonConvert.SerializeObject(obj);
            ticketHistory.Code = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

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