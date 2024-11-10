using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_eventz.Models;

namespace web_api_eventz.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<TicketHistory>> GetTicketHistory();
        Task<List<TicketHistory>> GetMyTicketHistory(string userId);
        Task<TicketHistory?> GetTicketHistoryById(int id);
        Task<TicketHistory?> BuyTicket(TicketHistory ticketHistory);

    }
}