using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api_eventz.DTOs.Accounts;
using web_api_eventz.Models;

namespace web_api_eventz.Mappers
{
    public static class AppUserMapper
    {
        public static UpdateUserResponseDto ToUpdateUserDtoFromUser(this AppUser appUser)
        {
            return new UpdateUserResponseDto
            {
                Id = appUser.Id,
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                ProfilePicture = appUser.ProfilePicture,
                ContactDetails = appUser.ContactDetails,
                Preferences = appUser.Preferences,
                NotificationSettings = appUser.NotificationSettings,
                CreatedAt = appUser.CreatedAt,
                UpdatedAt = appUser.UpdatedAt,
            };
        }
        public static RegisterResponseDto ToRegisterResponseFromUser(this AppUser appUser, string Token)
        {
            return new RegisterResponseDto
            {
                Firstname = appUser.FirstName,
                LastName = appUser.LastName,
                Email = appUser.Email,
                Username = appUser.UserName,
                Token = Token
            };
        }
    }
}