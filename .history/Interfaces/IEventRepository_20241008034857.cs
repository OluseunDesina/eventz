using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_eventz.DTOs.EventsDto;
using web_api_eventz.Helpers;
using web_api_eventz.Models;

namespace web_api_eventz.Interfaces
{
    public interface IEventRepository
    {
        Task<Event?> GetEventById(int id);
        Task<List<Event>> GetEventList();
        Task<List<Event>> GetUserEventList(string userId);
        Task<Event> CreateEvent(Event eventModel);
        Task<Event?> UpdateEvent(int Id, string userId, Event eventModel);
        Task<Event?> DeleteEvent(int Id, string userId);
        Task<Event?> DuplicateEvent(int Id, string userId);
        Task<bool> EventExist(int Id);
        Task<bool> EventWasCreatedByUser(int Id, string userId);
    }
}