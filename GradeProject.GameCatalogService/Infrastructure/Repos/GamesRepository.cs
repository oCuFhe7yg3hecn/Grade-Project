using GradeProject.GameCatalogService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


// Will be deleted in future
// namespace GradeProject.GameCatalogService.Infrastructure
// {
// public class GamesRepository
// {
//     private readonly IMongoCollection<GameInfo> _games;

//     public GamesRepository(MongoDbContext context)
//     {
//         _games = context.Games;
//     }

//     public async Task<int> CountAsync(Expression<Func<GameInfo, bool>> filter = null)
//     {
//         var count = filter == null ? await _games.CountAsync(filter) : await _games.CountAsync(_ => true);
//         return Convert.ToInt32(count);
//     }

//     public async Task<GameInfo> Single(Expression<Func<GameInfo, bool>> filter) =>
//         await _games.Find(filter).FirstAsync();

//     public async Task<List<GameInfo>> Where(Expression<Func<GameInfo, bool>> filter, int count = 10, int page = 1)
//     {
//         return await _games.Find(filter)
//                     .Skip(count * (page <= 0 ? 1 : page - 1))
//                     .Limit(count)
//                     .ToListAsync();
//     }

//     public async Task Add(GameInfo newGame) =>
//         await _games.InsertOneAsync(newGame);

//     public async Task<bool> UpdateOne(UpdateDefinition<GameInfo> updateDefinition, Guid gameId)
//     {
//         var res = await _games.UpdateOneAsync(u => u.Id == gameId,
//                                       updateDefinition);

//         return res.IsAcknowledged && res.ModifiedCount == 1;
//     }

//     public async Task<bool> UpdateManyAsync(UpdateDefinition<GameInfo> updateDefinition)
//     {
//         var res = await _games.UpdateManyAsync(_ => true, updateDefinition);
//         return res.IsAcknowledged && res.ModifiedCount > 0;
//     }

//     public async Task<bool> Delete(Expression<Func<GameInfo, bool>> filter)
//     {
//         var deleteResult = await _games.DeleteOneAsync(filter);
//         return deleteResult.IsAcknowledged && deleteResult.DeletedCount == 1;
//     }

//     }
// }
