using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Application.Contract;
using TicketSystem.Context;

namespace TicketSystem.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            Tickets = new TicketRepository(_context,this);
            Users = new UserRepository(_context);
        }
        public ITicketRepository Tickets {  get; private set; }

        public IUserRepository Users { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
