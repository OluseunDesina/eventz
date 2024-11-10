using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace web_api_eventz.DTOs.Accounts
{
    public class RegisterAppUserDto
    {
        [Required]
        [MinLength(3)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MinLength(3)]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [MinLength(3)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}