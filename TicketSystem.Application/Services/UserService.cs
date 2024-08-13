using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSystem.Application.Contract;
using TicketSystem.DTOs;
using TicketSystem.Models;

namespace TicketSystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        
        public async Task<UserDtos> AddUserAsync(UserDtos userDto)
        {
            var user = MapToAppUser(userDto); 
            var addedUser = await _unitOfWork.Users.CreateAsync(user); 
            return MapToUserDto(addedUser); 
        }

        
        public async Task<UserDtos> DeleteUserAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User Not Found!");
            }

            await _unitOfWork.Users.DeleteAsync(user);
            return MapToUserDto(user); 
        }

        
        public async Task<IQueryable<UserDtos>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            return users.Select(user => MapToUserDto(user)).AsQueryable();
        }

        
        public async Task<UserDtos> GetUserByIdAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            return user == null ? null : MapToUserDto(user);
        }

        
        public async Task<UserDtos> GetUserByUserNameAsync(string userName)
        {
            var user = await _unitOfWork.Users.GetUserByUserNameAsync(userName);
            return user == null ? null : MapToUserDto(user);
        }

        
        public async Task<UserDtos> UpdateUserAsync(UserDtos userDto)
        {
            var existingUser = await _unitOfWork.Users.GetUserByUserNameAsync(userDto.UserName);
            if (existingUser == null)
            {
                throw new Exception("User Not Found!");
            }

            
            existingUser.UserName = userDto.UserName;
            existingUser.MobileNumber = userDto.MobileNumber;
            

            var updatedUser = await _unitOfWork.Users.UpdateAsync(existingUser);
            return MapToUserDto(updatedUser);
        }

        
        private AppUser MapToAppUser(UserDtos userDto)
        {
            return new AppUser
            {
                UserName = userDto.UserName,
                MobileNumber = userDto.MobileNumber,
                
            };
        }

        
        private UserDtos MapToUserDto(AppUser user)
        {
            return new UserDtos
            {
                
                UserName = user.UserName,
                MobileNumber = user.MobileNumber,
                
            };
        }
    }
}
