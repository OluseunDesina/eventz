using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using web_api_eventz.Data;
using web_api_eventz.DTOs.EventsDto;
using web_api_eventz.Helpers;
using web_api_eventz.Interfaces;
using web_api_eventz.Models;

namespace web_api_eventz.Repository
{
    public class EventRepository : IEventRepository
    {
        private ApplicationDbContext _context;
        public EventRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Event> CreateEvent(Event eventModel)
        {
            await _context.AddAsync(eventModel);
            await _context.SaveChangesAsync();
            return eventModel;
        }

        public async Task<Event?> DeleteEvent(int Id, string userId)
        {
            var eventTo = await _context.Events.FirstOrDefaultAsync(e => e.EventId == Id && e.OrganizerId == userId);
            if (eventTo == null)
            {
                return null;
            }
            _context.Events.Remove(eventTo);
            await _context.SaveChangesAsync();
            return eventTo;
        }

        public async Task<Event?> DuplicateEvent(int Id, string userId)
        {
            var existingEvent = await _context.Events.FirstOrDefaultAsync(e => e.EventId == Id && e.OrganizerId == userId);
            if (existingEvent == null)
            {
                return null;
            }
            var newEvent = new Event
            {
                EventName = $"copy of {existingEvent.EventName}",
                OrganizerId = existingEvent.OrganizerId,
                EventDescription = existingEvent.EventDescription,
                Location = existingEvent.Location,
                Category = existingEvent.Category,
                Images = existingEvent.Images,
                Visibility = existingEvent.Visibility,
                EventDate = existingEvent.EventDate,
                Tickets = existingEvent.Tickets,
                Status = "Draft"
            };
            await _context.AddAsync(newEvent);
            await _context.SaveChangesAsync();
            return newEvent;
        }

        public async Task<bool> EventExist(int Id)
        {
            return await _context.Events.AnyAsync(e => e.EventId == Id);
        }

        public async Task<bool> EventWasCreatedByUser(int Id, string userId)
        {
            return await _context.Events.AnyAsync(e => e.EventId == Id && e.OrganizerId == userId);
        }

        public async Task<Event?> GetEventById(int id)
        {
            var selectedEvent = await _context.Events.Include(e => e.Tickets).Include(e => e.Tickets).FirstOrDefaultAsync(e => e.EventId == id);

            if (selectedEvent == null)
            {
                return null;
            }

            return selectedEvent;
            throw new NotImplementedException();
        }

        public async Task<List<Event>> GetEventList()
        {
            return await _context.Events.Include(e => e.Tickets).ToListAsync();
        }

        public async Task<List<Event>> GetUserEventList(string userId)
        {
            return await _context.Events.AsQueryable().Where(e => e.OrganizerId == userId).Include(e => e.Tickets).ToListAsync();
        }

        public async Task<Event?> UpdateEvent(int eventId, string userId, Event eventModel)
        {
            var oldEvent = await _context.Events.FirstOrDefaultAsync(e => e.EventId == eventId && e.OrganizerId == userId);
            if (oldEvent == null)
            {
                return null;
            }
            oldEvent.EventName = eventModel.EventName;
            oldEvent.EventDescription = eventModel.EventDescription;
            oldEvent.Location = eventModel.Location;
            oldEvent.Category = eventModel.Category;
            oldEvent.Images = eventModel.Images;
            oldEvent.Visibility = eventModel.Visibility;
            oldEvent.Status = eventModel.Status;
            oldEvent.EventDate = eventModel.EventDate;
            await _context.SaveChangesAsync();
            return oldEvent;
        }
    }
}