using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_eventz.DTOs.TransactionsDto
{
    public class CreateTransactionDto
    {
        public int EventId { get; set; }
        public int TicketId { get; set; }
        public string TransactionReference { get; set; } =  string.Empty;
        public int Quantity { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
    }
}