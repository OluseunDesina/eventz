using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace web_api_eventz.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public string ContactDetails { get; set; }= string.Empty;
        public string Preferences { get; set; }= string.Empty; // JSON
        public string NotificationSettings { get; set; }= string.Empty; // JSON
        public string UserType { get; set; }= string.Empty; // Enum: Attendee, Organizer
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public List<TicketHistory> TicketHistories { get; set; } = new List<TicketHistory>();
    }
}