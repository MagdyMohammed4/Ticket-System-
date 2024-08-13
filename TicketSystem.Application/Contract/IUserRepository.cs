using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.Models;

namespace TicketSystem.Application.Contract
{
    public interface IUserRepository: IRepository<AppUser,int>
    {
        Task<AppUser> GetUserByUserNameAsync(string userName);
    }
}
