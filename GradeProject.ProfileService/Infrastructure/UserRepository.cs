using AutoMapper;
using GradeProject.ProfileService.Models;
using GradeProject.ProfileService.Models.DTO;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GradeProject.ProfileService.Infrastructure
{
    public class UserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(MongoDbContext context)
        {
            _users = context.GetClollection<User>("Users");
        }

        public async Task<List<User>> GetUsers(Expression<Func<User, bool>> filter) =>
            await _users.Find(filter).ToListAsync();

        public async Task Insert(User user) =>
            await _users.InsertOneAsync(user);

        public async Task Update(User user)
        {
            var update = new UpdateDefinitionBuilder<User>()
                .Set(u => u.FirstName, user.FirstName)
                .Set(u => u.LastName, user.LastName)
                .Set(u => u.MiddleName, user.MiddleName)
                .Set(u => u.DOB, user.DOB)
                .Set(u => u.ImageURL, user.ImageURL)
                .Set(u => u.Slogan, user.Slogan);

            await _users.UpdateOneAsync(u => u.Id == user.Id,
                                        update,
                                        new UpdateOptions() { IsUpsert = true });
        }

        public async Task AddFriend(string userId, string friendId)
        {
            var userGuid = Guid.Parse(userId);
            var friendGuid = Guid.Parse(friendId);

            var update = new UpdateDefinitionBuilder<User>()
                .Push(u => u.Friends, friendGuid);

            await _users.UpdateOneAsync(u => u.Id == userGuid,
                                  update,
                                  new UpdateOptions() { IsUpsert = false });

            update = new UpdateDefinitionBuilder<User>()
                .Push(u => u.Friends, userGuid);

            await _users.UpdateOneAsync(u => u.Id == friendGuid,
                                  update,
                                  new UpdateOptions() { IsUpsert = false });
        }

        public async Task Delete(string id)
        {
            var userGuid = Guid.Parse(id);

            await _users.DeleteOneAsync(u => u.Id == userGuid);

            var updateDefinition = new UpdateDefinitionBuilder<User>()
                .Pull(u => u.Friends, userGuid);

            await _users.UpdateManyAsync(u => u.Friends.Contains(userGuid), updateDefinition);
        }

    }
}
