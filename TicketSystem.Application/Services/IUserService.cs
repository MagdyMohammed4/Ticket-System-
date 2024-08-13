using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketSystem.DTOs;
using TicketSystem.Models;

namespace TicketSystem.Application.Services
{
    public interface IUserService
    {
        Task<UserDtos> GetUserByIdAsync(int userId);
        Task<UserDtos> GetUserByUserNameAsync(string userName);
        Task<IQueryable<UserDtos>> GetAllUsersAsync();
        Task<UserDtos> AddUserAsync(UserDtos user);
        Task<UserDtos> UpdateUserAsync(UserDtos user);
        Task<UserDtos> DeleteUserAsync(int userId);
    }
}
