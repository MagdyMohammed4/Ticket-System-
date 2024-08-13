using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Models;

namespace TicketSystem.Application.Contract
{
    public interface ITicketRepository:IRepository<Ticket,int>
    {
        Task<Ticket> CreateTicketWithMobileNumber(string mobileNumber, string htmlImage);
        Task<IQueryable<Ticket>> GetAllTicketsNumberDesc();
        Task<Ticket> GetTicketByMobileNumber(string mobileNumber);
    }
}
