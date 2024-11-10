using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_eventz.Models
{
    [Table("Tickets")]

    public class Ticket
    {
        public int Id { get; set; }
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
        public List<TicketHistory> TicketHistories { get; set; } = new List<TicketHistory>();
    }
}