using GradeProject.GameCatalogService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Infrastructure
{
    public class GamesRepository
    {
        private readonly IMongoCollection<GameInfo> _games;

        public GamesRepository(MongoDbContext context)
        {
            _games = context.Games;
        }

        public async Task<GameInfo> Single(Expression<Func<GameInfo, bool>> filter) =>
            await _games.Find(filter).FirstAsync();

        public async Task<List<GameInfo>> Where(Expression<Func<GameInfo, bool>> filter, int count = 10, int page = 1)
        {
            //var lastId = _games.Find(_ => true).First().Id;
            //for (int i = 0; i < page-1; i++)
            //{
            //    var games = _games.Find(g => g.Id == lastId).Limit(count).ToList();
            //    lastId = games.LastOrDefault().Id;
            //}
            return await _games.Find(filter)
                        .Skip(count * (page - 1))
                        .Limit(count)
                        .ToListAsync();
        }

        public async Task Add(GameInfo newGame) =>
            await _games.InsertOneAsync(newGame);

        public async Task<bool> UpdateOne(UpdateDefinition<GameInfo> updateDefinition, Guid gameId)
        {
            var res = await _games.UpdateOneAsync(u => u.Id == gameId,
                                          updateDefinition);

            return res.IsAcknowledged && res.ModifiedCount == 1;
        }

        public async Task<bool> Delete(Guid id)
        {
            var deleteResult = await _games.DeleteOneAsync(g => g.Id == id);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount == 1;
        }

        //public async Task AddFriend(string userId, string friendId)
        //{
        //    var userGuid = Guid.Parse(userId);
        //    var friendGuid = Guid.Parse(friendId);

        //    var update = new UpdateDefinitionBuilder<GameInfo>()
        //        .Push(u => u.Friends, friendGuid);

        //    await _games.UpdateOneAsync(u => u.Id == userGuid,
        //                          update,
        //                          new UpdateOptions() { IsUpsert = false });

        //    update = new UpdateDefinitionBuilder<GameInfo>()
        //        .Push(u => u.Friends, userGuid);

        //    await _games.UpdateOneAsync(u => u.Id == friendGuid,
        //                          update,
        //                          new UpdateOptions() { IsUpsert = false });
        //}

    }
}
