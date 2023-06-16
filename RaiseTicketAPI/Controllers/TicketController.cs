using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RaiseTicketAPI.Exceptions;
using RaiseTicketAPI.Interfaces;
using RaiseTicketAPI.Models;
using RaiseTicketAPI.Models.DTOs;
using RaiseTicketAPI.Services;

namespace RaiseTicketAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [EnableCors("AngularCORS")]
    public class TicketController : ControllerBase
    {
        private readonly IManageTicket _ticketService;

        public TicketController(IManageTicket ticketService)
        {
            _ticketService= ticketService;
        }
        [HttpPost]
        public async Task<ActionResult<Ticket?>> RaiseTicket(Ticket ticket)
        {
            try
            {
                var myTicket = await _ticketService.RaiseTicket(ticket);
                if (myTicket != null)
                    return Created("Ticket Added Successfully", myTicket);
                return BadRequest(new Error(1, "Unable to Add Ticket"));
            }
            catch (InvalidSqlException ex)
            {
                return BadRequest(new Error(3, ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult<TicketDTO>> ResolveTicket(TicketDTO ticketDTO)
        {
            var ticket=await _ticketService.ResolveTicket(ticketDTO);
            if (ticket != null)
                return Ok(ticketDTO);
            return BadRequest(new Error(2,"Can't Resolved"));
        }

        [HttpPost]
        public async Task<ActionResult<List<Ticket>?>> GetSolvedTickets()
        {
            var tickets=await _ticketService.GetSolvedTickets();
            if (tickets != null)
                return tickets;
            return BadRequest(new Error(4,"There is no Resolved Tickets"));
        }

        [HttpPost]
        public async Task<ActionResult<List<Ticket>?>> GetIssuedTickets()
        {
            var tickets = await _ticketService.GetIssuedTickets();
            if (tickets != null)
                return tickets;
            return BadRequest(new Error(5, "There is no Issued Tickets"));
        }
        [HttpGet]
        public async Task<ActionResult<List<Ticket>?>> GetAll()
        {
            var tickets=await _ticketService.GetAll();
            if (tickets != null) return Ok(tickets);
            return NotFound(new Error(6,"No Tickets found"));
        }

        [HttpPost]
        public async Task<ActionResult<Ticket?>> GetTicket(TicketDTO ticketDTO)
        {
            var ticket = await _ticketService.Get(ticketDTO.TicketNumber);
            if(ticket != null) return Ok(ticket);
            return NotFound(new Error(7,"NO such ticket"));
        }

        [HttpPost]
        public async Task<ActionResult<List<Ticket>?>> GetSpecificInternTickets(Ticket ticket)
        {
            var tickets = await _ticketService.GetSpecificInternTickets(ticket);
            if (tickets != null)
                return Ok(tickets);
            return NotFound(new Error(8,"There is no Tickets"));
        }

    }
}
