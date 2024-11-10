using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_eventz.Models
{
    [Table("TicketHistorys")]
    public class TicketHistory
    {
        public string AppUserId { get; set; } = string.Empty;
        public AppUser AppUser { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public string TransactionReference { get; set; } =  string.Empty;
        public int Quantity { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime PurchasedAt { get; set; } = DateTime.Now;
    }
}