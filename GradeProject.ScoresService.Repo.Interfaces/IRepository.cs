using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GradeProject.ScoresService.Repo.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<int> CountAsync(Expression<Func<T, bool>> filter = null);

        Task<T> SingleAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> WhereAsync(Expression<Func<T, bool>> filter, int count = 10, int page = 1);

        Task AddOneAsync(T newEntity);

        Task<bool> UpdateOneAsync(Expression<Func<T, bool>> filter, T entiy);

        Task<bool> DeleteOneAsync(Expression<Func<T, bool>> filter);

        Task<bool> CheckASync(Expression<Func<T, bool>> statement);
    }
}
