using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_eventz.Models
{
    public class Ticket
    {
        public int TicketID { get; set; }
        public int EventID { get; set; } // Foreign Key to Event
        public Event Event { get; set; }
        public string Ticketname { get; set; } = string.Empty;
        public string TicketType { get; set; } = string.Empty; // Enum: Paid, Free, etc.
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int TicketsSold { get; set; }
        public bool Availability { get; set; }
        public List<string>? DiscountCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime SalesStartDate { get; set; }
        public DateTime SalesEndDate { get; set; }
    }
}