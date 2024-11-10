using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_api_eventz.Data;
using web_api_eventz.Interfaces;
using web_api_eventz.Models;

namespace web_api_eventz.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private ApplicationDbContext _context;
        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Ticket> AddTicket(Ticket ticketModel)
        {
            await _context.Tickets.AddAsync(ticketModel);
            await _context.SaveChangesAsync();
            return ticketModel;
        }

        public async Task<Ticket?> DeleteTicket(int ticketId)
        {
            var existingTicket = await _context.Tickets.FirstOrDefaultAsync(ticket => ticket.TicketID == ticketId);
            if (existingTicket == null)
            {
                return null;
            }
            _context.Tickets.Remove(existingTicket);
            await _context.SaveChangesAsync();
            return existingTicket;
        }

        public async Task<Ticket?> EditTicket(int ticketId, Ticket ticketModel)
        {
            var existingTicket = await _context.Tickets.FirstOrDefaultAsync(ticket => ticket.TicketID == ticketId);
            if (existingTicket == null)
            {
                return null;
            }

            existingTicket.Ticketname = ticketModel.Ticketname;
            existingTicket.TicketType = ticketModel.TicketType;
            existingTicket.Price = ticketModel.Price;
            existingTicket.Quantity = ticketModel.Quantity;
            existingTicket.Availability = ticketModel.Availability;
            existingTicket.DiscountCode = ticketModel.DiscountCode;
            existingTicket.SalesStartDate = ticketModel.SalesStartDate;
            existingTicket.SalesEndDate = ticketModel.SalesEndDate;
            await _context.SaveChangesAsync();
            return existingTicket;
        }

        public async Task<Ticket?> GetTicketById(int ticketId)
        {
            var exixtingTicket = await _context.Tickets.FirstOrDefaultAsync(ticket => ticket.TicketID == ticketId);
            if (exixtingTicket == null)
            {
                return null;
            }
            return exixtingTicket;
        }

        public async Task<List<Ticket>> GetTickets()
        {
            return await _context.Tickets.AsQueryable().ToListAsync();
        }
    }
}