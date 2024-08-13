using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Models;

namespace TicketSystem.Application.Services
{
    public interface ITicketService
    {
        Task<Ticket> Create(Ticket ticket);
        Task <Ticket> Update(Ticket ticket);
        Task<Ticket> Delete(Ticket ticket);
        Task <IQueryable<Ticket>>  GetAll();
        Task <Ticket> GetById(int id);
        Task<Ticket> CreateTicketWithMobileNumber(string mobileNumber, string htmlImage);
        Task<Ticket> GetUserTicketDetailsByMobileNumber(string mobileNumber);
        Task<IQueryable<Ticket>> GetAllTicketsByNumberDesc();
    }
}
