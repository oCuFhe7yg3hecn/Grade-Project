using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Infrastructure.Repos
{
    interface IRepository<T> where T : class
    {
        Task<int> CountAsync(Expression<Func<T, bool>> filter = null);

        Task<T> SingleAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> WhereAsync(Expression<Func<T, bool>> filter, int count = 10, int page = 1);

        Task AddOneAsync(T newGame);

        Task<bool> UpdateOneAsync(UpdateDefinition<T> updateDefinition, Guid gameId);
        Task<bool> UpdateManyAsync(UpdateDefinition<T> updateDefinition);

        Task<bool> DeleteOneAsync(Expression<Func<T, bool>> filter);
    }
}
