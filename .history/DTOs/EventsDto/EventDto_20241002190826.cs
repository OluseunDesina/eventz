using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_eventz.Models;

namespace web_api_eventz.DTOs.EventsDto
{
    public class EventDto
    {
                public int EventId { get; set; }
        public string OrganizerId { get; set; } = string.Empty;
        public string EventName { get; set; } = string.Empty;
        public string EventDescription { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public List<string>? Images { get; set; }
        public int Popularity { get; set; }
        public float Ratings { get; set; }
        // public List<Ticket>? Tickets { get; set; }
        public string Visibility { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime EventDate { get; set; } = DateTime.Now;
        public List<Ticket>? Tickets { get; set; }
    }
}