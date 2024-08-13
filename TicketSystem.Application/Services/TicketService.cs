using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Application.Contract;
using TicketSystem.Models;

namespace TicketSystem.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketService(IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;

        }

        public async Task<Ticket> CreateTicketWithMobileNumber(string mobileNumber, string htmlImage)
        {
            return await _unitOfWork.Tickets.CreateTicketWithMobileNumber(mobileNumber, htmlImage);
        }

        public async Task<Ticket> GetUserTicketDetailsByMobileNumber(string mobileNumber)
        {
            return await _unitOfWork.Tickets.GetTicketByMobileNumber(mobileNumber);
        }

        public async Task<IQueryable<Ticket>> GetAllTicketsByNumberDesc()
        {
            return await _unitOfWork.Tickets.GetAllTicketsNumberDesc();
        }




        public async Task<Ticket> Create(Ticket ticket)
        {
            var existingTicket = await _unitOfWork.Tickets.GetTicketByMobileNumber(ticket.User.MobileNumber);
            if (existingTicket != null)
            {
                throw new Exception("Ticket already exists for this user.");
            }
            await _unitOfWork.Tickets.CreateAsync(ticket);
            await _unitOfWork.Tickets.SaveChangesAsync();
            return ticket;
        }

        public async Task<Ticket> Update(Ticket ticket)
        {
            var Data = await _unitOfWork.Tickets.GetByIdAsync(ticket.Id);
            if(Data == null) 
            {
                throw new Exception("Ticket Not Found!");
            }
            else
            {
                await _unitOfWork.Tickets.UpdateAsync(ticket);
                await _unitOfWork.Tickets.SaveChangesAsync();
                return ticket;
            }
        }

        public async Task<Ticket> Delete(Ticket ticket)
        {
            var oldTicket = await _unitOfWork.Tickets.GetByIdAsync(ticket.Id);
            if(oldTicket == null)
            {
                throw new Exception("Ticket Not Found!");
            }
            else
            {
                await _unitOfWork.Tickets.DeleteAsync(ticket);
                await _unitOfWork.Tickets.SaveChangesAsync();
                return ticket;
            }
        }

        public async Task<IQueryable<Ticket>> GetAll()
        {
            var Tickets = await _unitOfWork.Tickets.GetAllAsync();
            return Tickets;
        }

        public async Task<Ticket> GetById(int id)
        {
            var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);
            return ticket;
        }

       
    }
}
