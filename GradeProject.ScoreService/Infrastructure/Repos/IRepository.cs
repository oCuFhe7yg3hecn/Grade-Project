using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GradeProject.ScoreService.Infrastructure.Repos
{
    public interface IRepository<T> where T : class
    {
        Task<int> CountAsync(Expression<Func<T, bool>> filter = null);

        Task<List<T>> AllAsync();

        Task<T> SingleAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> WhereAsync(Expression<Func<T, bool>> filter, int count = 10, int page = 1);

        Task AddOneAsync(T newGame);

        Task<bool> UpdateOneAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> updateDefinition, Guid gameId);
        Task<bool> UpdateManyAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> updateDefinition);

        Task<bool> DeleteOneAsync(Expression<Func<T, bool>> filter);
    }
}
