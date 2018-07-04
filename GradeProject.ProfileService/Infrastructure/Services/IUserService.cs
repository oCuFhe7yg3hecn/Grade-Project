using GradeProject.ProfileService.Models;
using GradeProject.ProfileService.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ProfileService.Infrastructure.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsersAsync();

        Task<User> GetUserByIdAsync(string id);

        Task CreateUser(UserInsertDTO newUser);

        Task<bool> UpdateUser(string id, UserInsertDTO newUser);

        Task<bool> AddFriend(string userId, string friendId);

        Task DeleteUser(string id);
    }
}
