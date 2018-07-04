using AutoMapper;
using GradeProject.ProfileService.Infrastructure.Repos;
using GradeProject.ProfileService.Models;
using GradeProject.ProfileService.Models.DTO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ProfileService.Infrastructure
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly DefaultAvatarsFactory _avatarsFactory;

        public UserService(IRepository<User> userRepository, IMapper mapper, DefaultAvatarsFactory avatarsFactory)
        {
            this._userRepository = userRepository;
            _mapper = mapper;
            _avatarsFactory = avatarsFactory;
        }

        public async Task<List<User>> GetUsersAsync() =>
            await _userRepository.WhereAsync(_ => true);

        public async Task<User> GetUserByIdAsync(string id) =>
            await _userRepository.SingleAsync(u => u.Id == Guid.Parse(id));

        public async Task CreateUser(UserInsertDTO newUser)
        {
            var user = _mapper.Map<User>(newUser);
            user.ImageURL = _avatarsFactory.GetRandomAvatar(user.Gender);
            await _userRepository.AddOneAsync(user);
        }

        public async Task<bool> UpdateUser(string id, UserInsertDTO newUser)
        {
            var updateDeff = new UpdateDefinitionBuilder<User>()
               .Set(u => u.FirstName, newUser.FirstName)
               .Set(u => u.LastName, newUser.LastName)
               .Set(u => u.MiddleName, newUser.MiddleName)
               .Set(u => u.DOB, newUser.DOB)
               .Set(u => u.ImageURL, newUser.ImageUrl)
               .Set(u => u.Slogan, newUser.Slogan);

            return await _userRepository.UpdateOneAsync(u => u.Id == Guid.Parse(id), updateDeff);
        }

        public async Task<bool> AddFriend(string userId, string friendId)
        {
            var userGuid = Guid.Parse(userId);
            var friendGuid = Guid.Parse(friendId);

            var userExists = await _userRepository.CheckASync(u => u.Id == userGuid);
            var friendExists = await _userRepository.CheckASync(u => u.Id == friendGuid);

            if (userExists && friendExists)
            {
                //Should I check for impossible state in the system(user has friend in his list and friend not and opposite), or not?
                //var alreadyhas
                //if()

                var userUpdateDeff = new UpdateDefinitionBuilder<User>().Push(u => u.Friends, friendGuid);
                var friendUpdateDeff = new UpdateDefinitionBuilder<User>().Push(u => u.Friends, userGuid);

                var userRes = await _userRepository.UpdateOneAsync(u => u.Id == userGuid,
                                                     userUpdateDeff);

                var friendRes = await _userRepository.UpdateOneAsync(u => u.Id == friendGuid,
                                                          friendUpdateDeff);

                return userRes && friendRes;
            }
            else { throw new Exception("User with such id does not exists"); }
        }

        public async Task DeleteUser(string id) => await _userRepository.DeleteOneAsync(u => u.Id == Guid.Parse(id));
    }
}
