using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_eventz.DTOs.EventsDto;
using web_api_eventz.DTOs.TicketsDto;

namespace web_api_eventz.DTOs.TransactionsDto
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public string AppUserId { get; set; } = string.Empty;
        public int EventId { get; set; }
        public EventDto Event { get; set; }
        public int TicketId { get; set; }
        public TicketDto Ticket { get; set; }
        public string TransactionReference { get; set; } =  string.Empty;
        public int Quantity { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime PurchasedAt { get; set; } = DateTime.Now;
    }
}