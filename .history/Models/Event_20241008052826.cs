using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_eventz.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string OrganizerId { get; set; } = string.Empty;
        public AppUser? Organizer { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string EventDescription { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public List<string>? Images { get; set; }
        public int Popularity { get; set; }
        public float Ratings { get; set; }
        public string Visibility { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime EventDate { get; set; } = DateTime.Now;
        public TimeSpan EventTime { get; set; }
        public List<Ticket>? Tickets { get; set; } = new List<Ticket>();
    }
}