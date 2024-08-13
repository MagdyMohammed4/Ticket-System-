using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Application.Contract;
using TicketSystem.Application.Services;
using TicketSystem.Models;

namespace TicketSystem.API.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TicketController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> CreateTicket( string mobileNumber,  string htmlImage)
        {
            try
            {
                var ticket = await _unitOfWork.Tickets.CreateTicketWithMobileNumber(mobileNumber, htmlImage);

                return Ok(ticket);

            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllTicketsNumberDesc()
        {
            var tickets = await _unitOfWork.Tickets.GetAllTicketsNumberDesc();
            return Ok(tickets);
        }

        [HttpGet]
        [Route("{mobileNumber}")]
        
        public async Task<IActionResult> GetTicketByMobileNumber(string mobileNumber)
        {
            var ticket = await _unitOfWork.Tickets.GetTicketByMobileNumber(mobileNumber);
            if (ticket == null)
            {
                return NotFound("Ticket not found for the given mobile number.");
            }

            return Ok(ticket);
        }









 


    }
}
