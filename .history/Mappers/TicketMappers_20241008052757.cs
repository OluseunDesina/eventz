using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_eventz.DTOs.TicketsDto;
using web_api_eventz.Models;

namespace web_api_eventz.Mappers
{
    public static class TicketMappers
    {

        public static TicketDto ToTicketDto (this Ticket ticketModel) {
            return new TicketDto {
                TicketID = ticketModel.Id,
                EventID = ticketModel.EventID,
                Ticketname = ticketModel.Ticketname,
                TicketType = ticketModel.TicketType,
                Price = ticketModel.Price,
                Quantity = ticketModel.Quantity,
                TicketsSold = ticketModel.TicketsSold,
                Availability = ticketModel.Availability,
                DiscountCode = ticketModel.DiscountCode,
                CreatedAt = ticketModel.CreatedAt,
                UpdatedAt = ticketModel.UpdatedAt,
                SalesStartDate = ticketModel.SalesStartDate,
                SalesEndDate = ticketModel.SalesEndDate,
            };
        }
        public static Ticket ToTicketFromCreateDto(this CreateTicketDto ticketDto) {
            return new Ticket {
                EventID = ticketDto.EventID,
                Ticketname = ticketDto.Ticketname,
                TicketType = ticketDto.TicketType,
                Price = ticketDto.Price,
                Quantity = ticketDto.Quantity,
                Availability = ticketDto.Availability,
                DiscountCode = ticketDto.DiscountCode,
                SalesStartDate = ticketDto.SalesStartDate,
                SalesEndDate = ticketDto.SalesEndDate,
            };
        }
        public static Ticket ToTicketFromUpdateDto(this UpdateTicketDto ticketDto) {
            return new Ticket {
                Ticketname = ticketDto.Ticketname,
                TicketType = ticketDto.TicketType,
                Price = ticketDto.Price,
                Quantity = ticketDto.Quantity,
                Availability = ticketDto.Availability,
                DiscountCode = ticketDto.DiscountCode,
                SalesStartDate = ticketDto.SalesStartDate,
                SalesEndDate = ticketDto.SalesEndDate,
            };
        }
    }
}