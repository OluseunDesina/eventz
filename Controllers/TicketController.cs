using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using web_api_eventz.DTOs.TicketsDto;
using web_api_eventz.Extensions;
using web_api_eventz.Interfaces;
using web_api_eventz.Mappers;
using web_api_eventz.Models;

namespace web_api_eventz.Controllers
{
    [ApiController]
    [Route("api/ticket")]
    public class TicketController : ControllerBase
    {
        private ITicketRepository _ticketRepo;
        private IEventRepository _eventRepo;
        private UserManager<AppUser> _usermanager;
        public TicketController(ITicketRepository ticketRepo, IEventRepository eventRepo, UserManager<AppUser> usermanager)
        {
            _ticketRepo = ticketRepo;
            _eventRepo = eventRepo;
            _usermanager = usermanager;
        }

        [HttpDelete]
        [Authorize]
        [Route("{ticketId:int}")]
        public async Task<IActionResult> EditTicket([FromRoute] int ticketId)
        {
            var ticket = await _ticketRepo.DeleteTicket(ticketId);
            if (ticket == null)
            {
                return NotFound("ticket not found");
            }
            return NoContent();
        }

        [HttpPut]
        [Authorize]
        [Route("{ticketId:int}")]
        public async Task<IActionResult> EditTicket([FromRoute] int ticketId, [FromBody] UpdateTicketDto ticketDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticket = await _ticketRepo.EditTicket(ticketId, ticketDto.ToTicketFromUpdateDto());
            if (ticket == null)
            {
                return NotFound("ticket not found");
            }
            return Ok(ticket.ToTicketDto());
        }

        [HttpGet]
        [Authorize]
        [Route("by-event/{eventId:int}")]
        public async Task<IActionResult> GetTicketsByEvent([FromRoute] int eventId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var eventExist = await _eventRepo.EventExist(eventId);
            if (!eventExist)
            {
                return NotFound("Event not found");
            }
            var tickets = await _ticketRepo.GetTicketsByEventId(eventId);
            return Ok(tickets);
            
        }

        [HttpGet]
        [Authorize]
        [Route("{ticketId:int}")]
        public async Task<IActionResult> GetTicketById([FromRoute] int ticketId)
        {
            var ticket = await _ticketRepo.GetTicketById(ticketId);
            if (ticket == null)
            {
                return NotFound("Ticket not found");
            }
            return Ok(ticket.ToTicketDto());
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllTickets()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tickets = await _ticketRepo.GetTickets();
            return Ok(tickets.Select(e => e.ToTicketDto()));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateTicket(CreateTicketDto ticketDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var username = User.GetUsername();
            var user = await _usermanager.FindByNameAsync(username);
            if (user == null)
            {
                return Unauthorized();
            }

            var eventExist = await _eventRepo.EventWasCreatedByUser(ticketDto.EventID, user.Id);
            if (eventExist == false)
            {
                return NotFound("Event not found");
            }

            var ticket = await _ticketRepo.AddTicket(ticketDto.ToTicketFromCreateDto());

            return CreatedAtAction(nameof(GetTicketById), new { TicketId = ticket.Id }, ticket.ToTicketDto());
        }
    }
}