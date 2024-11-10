using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using web_api_eventz.DTOs.EventsDto;
using web_api_eventz.Extensions;
using web_api_eventz.Interfaces;
using web_api_eventz.Mappers;
using web_api_eventz.Models;

namespace web_api_eventz.Controllers
{
    [Route("api/events")]
    public class EventContoller: ControllerBase
    {
        private IEventRepository _eventRepo;
        private UserManager<AppUser> _userManager;
        public EventContoller(IEventRepository eventRepo, UserManager<AppUser> userManager)
        {
            _eventRepo = eventRepo;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("{eventId:int}/duplicate")]
        [Authorize]
        public async Task<IActionResult> DuplicateEvent([FromRoute] int eventId) {
            var userName = User.GetUsername();
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return Unauthorized();
            }

            var eventToDuplicate = await _eventRepo.DuplicateEvent(eventId, user.Id);
            if (eventToDuplicate == null)
            {
                return StatusCode(500, "Unable to duplicate event");
            }
            return CreatedAtAction(nameof(GetEventById), new { EventId = eventToDuplicate.EventId }, eventToDuplicate.ToEventDto());
        }

        [HttpDelete]
        [Route("{eventId:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteEvent([FromRoute] int eventId) {
            var userName = User.GetUsername();
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return Unauthorized();
            }
            var eventModel = await _eventRepo.DeleteEvent(eventId, user.Id);
            if (eventModel == null)
            {
                return NotFound("Event not found");
            }
            return NoContent();
        }

        [HttpPut]
        [Route("{eventId:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateEvent([FromRoute] int eventId, [FromBody] UpdateEventDto eventDto) {
            var userName = User.GetUsername();
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return Unauthorized();
            }

            var eventModel = await _eventRepo.UpdateEvent(eventId, user.Id, eventDto.ToEventFromUpdateEventDto());
            if (eventModel == null)
            {
                return NotFound();
            }
            return Ok(eventModel.ToEventDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDto eventDto) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return Unauthorized();
            }
            var eventModel = eventDto.ToEventFromCreateEventDto();
            eventModel.OrganizerId = user.Id;
            Console.WriteLine($"event to be creaated goes here: {eventModel.ToEventDto()}");
            var createdEvent = await _eventRepo.CreateEvent(eventModel);
            // return Ok(createdEvent);
            return CreatedAtAction(nameof(GetEventById), new { EventId = createdEvent.EventId }, createdEvent.ToEventDto());
        }

        [HttpGet]
        [Authorize]
        [Route("/my-organized-event")]
        public async Task<IActionResult> GetMyOrganizedEvents() {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return Unauthorized();
            }

            var events = await _eventRepo.GetUserEventList(user.Id);
            return Ok(events.Select(e => e.ToEventDto()));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllEvents() {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var events = await _eventRepo.GetEventList();
            return Ok(events.Select(e => e.ToEventDto()));
        }

        [HttpGet]
        [Route("{eventId:int}")]
        [Authorize]
        public async Task<IActionResult> GetEventById([FromRoute] int eventId) {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var selectedEvent = await _eventRepo.GetEventById(eventId);
            if (selectedEvent == null)
            {
                return NotFound("Event not found");
            }
            return Ok(selectedEvent.ToEventDto());
        }
    }
}