using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_eventz.DTOs.Accounts
{
    public class UpdateUserDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        [Required]
        public string ContactDetails { get; set; } = string.Empty;
        public string Preferences { get; set; } = string.Empty; // JSON
        public string NotificationSettings { get; set; } = string.Empty; // JSON
    }
}