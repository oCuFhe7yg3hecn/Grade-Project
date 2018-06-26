using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GradeProject.ProfileService.Infrastructure.Repos
{
    public class GenericRepo<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _coll;

        public GenericRepo(MongoDbContext context, string collectionName)
        {
            _coll = context.GetClollection<T>(collectionName);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> filter = null)
        {
            var count = filter == null ? await _coll.CountAsync(filter) : await _coll.CountAsync(_ => true);

            return Convert.ToInt32(count);
        }

        public async Task<T> SingleAsync(Expression<Func<T, bool>> filter) =>
            await _coll.Find(filter).FirstAsync();

        public async Task<List<T>> WhereAsync(Expression<Func<T, bool>> filter, int count = 10, int page = 1)
        {
            return await _coll.Find(filter)
                        .Skip(count * (page <= 0 ? 1 : page - 1))
                        .Limit(count)
                        .ToListAsync();
        }

        public async Task AddOneAsync(T newGame) =>
            await _coll.InsertOneAsync(newGame);

        public async Task<bool> UpdateOneAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> updateDefinition)
        {
            var res = await _coll.UpdateOneAsync(filter,
                                                 updateDefinition);

            return res.IsAcknowledged && res.ModifiedCount == 1;
        }

        public async Task<bool> UpdateManyAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> updateDefinition)
        {
            var res = await _coll.UpdateManyAsync(filter,
                                                  updateDefinition);

            return res.IsAcknowledged && res.ModifiedCount > 0;
        }

        public async Task<bool> DeleteOneAsync(Expression<Func<T, bool>> filter)
        {
            var deleteResult = await _coll.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount == 1;
        }

        public async Task<bool> CheckASync(Expression<Func<T, bool>> statement)
        {
            var user = await SingleAsync(statement);
            return user != null ? true : false;
        }
    }
}
