using GradeProject.GameCatalogService.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Infrastructure.Repos
{
    public class CategoryRepository
    {
        private readonly IMongoCollection<Category> _categories;

        public CategoryRepository(MongoDbContext context)
        {
            _categories = context.Categories;
        }

        public async Task<List<Category>> AllAsync()
        {
            return await _categories.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Category> Single(Expression<Func<Category, bool>> filter) =>
            await _categories.Find(filter).FirstAsync();

        public async Task<List<Category>> Where(Expression<Func<Category, bool>> filter, int count = 10, int page = 1)
        {
            return await _categories.Find(filter)
                        .Skip(count * (page <= 0 ? 1 : page - 1))
                        .Limit(count)
                        .ToListAsync();
        }

        public async Task Add(Category newGame) =>
            await _categories.InsertOneAsync(newGame);

        public async Task<bool> UpdateOne(UpdateDefinition<Category> updateDefinition, Guid gameId)
        {
            var res = await _categories.UpdateOneAsync(u => u.Id == gameId,
                                                            updateDefinition);

            return res.IsAcknowledged && res.ModifiedCount == 1;
        }

        public async Task<bool> Delete(Guid id)
        {
            var deleteResult = await _categories.DeleteOneAsync(g => g.Id == id);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount == 1;
        }

    }
}
