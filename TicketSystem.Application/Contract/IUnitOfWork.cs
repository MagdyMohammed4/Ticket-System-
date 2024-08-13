using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSystem.Application.Contract
{
    public interface IUnitOfWork
    {
        ITicketRepository Tickets { get; }
        IUserRepository Users { get; }
        Task<int> SaveChangesAsync();
    }
}
