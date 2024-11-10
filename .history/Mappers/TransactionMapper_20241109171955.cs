using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_eventz.DTOs.TransactionsDto;
using web_api_eventz.Models;

namespace web_api_eventz.Mappers
{
    public static class TransactionMapper
    {
        public static TransactionDto ToTransactionDto(this TicketHistory ticketHistoryModel)
        {
            return new TransactionDto
            {
                Id = ticketHistoryModel.Id,
                AppUserId = ticketHistoryModel.AppUserId,
                EventId = ticketHistoryModel.EventId,
                Event = ticketHistoryModel.Event.ToEventDto(),
                TicketId = ticketHistoryModel.TicketId,
                // Ticket = ticketHistoryModel.Ticket.ToTicketDto(),
                TransactionReference = ticketHistoryModel.TransactionReference,
                Quantity = ticketHistoryModel.Quantity,
                PaymentMethod = ticketHistoryModel.PaymentMethod,
                TotalAmount = ticketHistoryModel.TotalAmount,
                Code = ticketHistoryModel.Code,
                PurchasedAt = ticketHistoryModel.PurchasedAt
            };
        }

        public static TicketHistory ToTransactionFromCreateDto(this CreateTransactionDto transactionDto)
        {
            return new TicketHistory
            {
                EventId = transactionDto.EventId,
                TicketId = transactionDto.TicketId,
                TransactionReference = transactionDto.TransactionReference,
                Quantity = transactionDto.Quantity,
                PaymentMethod = transactionDto.PaymentMethod,
                TotalAmount = transactionDto.TotalAmount,
            };
        }
    }
}