using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_eventz.Models;

namespace web_api_eventz.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket> AddTicket(Ticket ticketModel);
        Task<Ticket?> EditTicket(int ticketId, Ticket ticketModel);
        Task<Ticket?> DeleteTicket(int ticketId);
        Task<List<Ticket>> GetTickets();
        Task<Ticket?> GetTicketById(int ticketId);
    }
}