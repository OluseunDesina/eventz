using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api_eventz.DTOs.Accounts;
using web_api_eventz.Extensions;
using web_api_eventz.Interfaces;
using web_api_eventz.Mappers;
using web_api_eventz.Models;

namespace web_api_eventz.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _siginManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> siginManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _siginManager = siginManager;
        }



        [HttpPut]
        [Route("user/notifications-settings")]
        [Authorize]
        public async Task<IActionResult> SetNotificationSettings([FromBody] UpdateUserDto appUser)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var username = User.GetUsername();
            if (username == null)
            {
                return Unauthorized("Permissions");
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == username);
            if (user == null)
            {
                return NotFound("User not found");
            }

            user.FirstName = appUser.FirstName;
            user.LastName = appUser.LastName;
            user.ContactDetails = appUser.ContactDetails;
            user.ProfilePicture = appUser.ProfilePicture;
            user.Preferences = appUser.Preferences;
            user.NotificationSettings = appUser.NotificationSettings;
            user.UpdatedAt = DateTime.Now;

            await _userManager.UpdateAsync(user);

            return Ok(user.ToUpdateUserDtoFromUser());
        }

        [HttpPut]
        [Route("user/edit-profile")]
        [Authorize]
        public async Task<IActionResult> editProfile([FromBody] UpdateUserDto appUser)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var username = User.GetUsername();
            if (username == null)
            {
                return Unauthorized("Permissions");
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(user => user.UserName == username);
            if (user == null)
            {
                return NotFound("User not found");
            }

            user.FirstName = appUser.FirstName;
            user.LastName = appUser.LastName;
            user.ContactDetails = appUser.ContactDetails;
            user.ProfilePicture = appUser.ProfilePicture;
            user.Preferences = appUser.Preferences;
            user.NotificationSettings = appUser.NotificationSettings;
            user.UpdatedAt = DateTime.Now;

            await _userManager.UpdateAsync(user);

            return Ok(user.ToUpdateUserDtoFromUser());
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginRequest([FromBody] LoginRequestDto loginInfo)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appUser = await _userManager.FindByNameAsync(loginInfo.Username);
            if (appUser == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var passwordMatch = await _siginManager.CheckPasswordSignInAsync(appUser, loginInfo.Password, false);
            if (passwordMatch.Succeeded == false)
            {
                return Unauthorized("Invalid credentials");
            }
            return Ok(appUser.ToRegisterResponseFromUser(_tokenService.CreateToken(appUser)));
        }

        [HttpPost]
        [Route("register/user")]
        public async Task<IActionResult> RegisterAppUser([FromBody] RegisterAppUserDto appUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var appUser = new AppUser
                {
                    FirstName = appUserDto.FirstName,
                    LastName = appUserDto.LastName,
                    Email = appUserDto.Email,
                    UserName = appUserDto.UserName,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                var createSuccess = await _userManager.CreateAsync(appUser, appUserDto.Password);

                if (createSuccess.Succeeded == true)
                {
                    // Add Role to user
                    var roleSucceed = await _userManager.AddToRoleAsync(appUser, "user");

                    if (roleSucceed.Succeeded == true)
                    {
                        return Ok(appUser.ToRegisterResponseFromUser(_tokenService.CreateToken(appUser)));
                    }
                    else
                    {
                        return StatusCode(500, roleSucceed.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createSuccess.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
    }
}