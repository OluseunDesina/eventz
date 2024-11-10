using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_eventz.DTOs.EventsDto;
using web_api_eventz.Models;

namespace web_api_eventz.Mappers
{
    public static class EventMapper
    {
        public static EventDto ToEventDto(this Event eventModel) {
            return new EventDto {
                EventId = eventModel.Id,
                OrganizerId = eventModel.OrganizerId,
                EventName = eventModel.EventName,
                EventDescription = eventModel.EventDescription,
                Location = eventModel.Location,
                Category = eventModel.Category,
                Images = eventModel.Images,
                Popularity = eventModel.Popularity,
                Ratings = eventModel.Ratings,
                Visibility = eventModel.Visibility,
                Status = eventModel.Status,
                EventDate = eventModel.EventDate,
                Tickets = eventModel.Tickets.Select(t => t.ToTicketDto()).ToList()
            };
        }
        public static EventDto ToTicketEventDto(this Event eventModel) {
            return new EventDto {
                EventId = eventModel.Id,
                OrganizerId = eventModel.OrganizerId,
                EventName = eventModel.EventName,
                EventDescription = eventModel.EventDescription,
                Location = eventModel.Location,
                Category = eventModel.Category,
                Images = eventModel.Images,
                Popularity = eventModel.Popularity,
                Ratings = eventModel.Ratings,
                Visibility = eventModel.Visibility,
                Status = eventModel.Status,
                EventDate = eventModel.EventDate,
            };
        }
        public static Event ToEventFromCreateEventDto(this CreateEventDto eventDto) {
            return new Event {
                EventName = eventDto.EventName,
                EventDescription = eventDto.EventDescription,
                Location = eventDto.Location,
                Category = eventDto.Category,
                Visibility = eventDto.Visibility,
                EventDate = eventDto.EventDate,
                // EventTime = eventDto.EventTime,
            };
        }
        public static Event ToEventFromUpdateEventDto(this UpdateEventDto eventDto) {
            return new Event {
                EventName = eventDto.EventName,
                EventDescription = eventDto.EventDescription,
                Location = eventDto.Location,
                Category = eventDto.Category,
                Visibility = eventDto.Visibility,
                EventDate = eventDto.EventDate,
                // EventTime = eventDto.EventTime,
            };
        }
    }
}