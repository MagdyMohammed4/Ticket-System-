using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Application.Contract;
using TicketSystem.Context;
using TicketSystem.Models;

namespace TicketSystem.Infrastructure
{
    public class UserRepository : Repository<AppUser, int>, IUserRepository
    {
        public UserRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
        }

        public Task<AppUser> GetUserByUserNameAsync(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
