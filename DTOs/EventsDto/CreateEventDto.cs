using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_eventz.DTOs.EventsDto
{
    public class CreateEventDto
    {
        [Required]
        public string EventName { get; set; } = string.Empty;
        public string EventDescription { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Visibility { get; set; } = string.Empty;
        public DateTime EventDate { get; set; } = DateTime.Now;
        // public TimeSpan EventTime { get; set; }
    }
}