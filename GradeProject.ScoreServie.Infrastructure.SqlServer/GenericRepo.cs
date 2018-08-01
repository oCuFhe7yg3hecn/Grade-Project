using GradeProject.ScoresService.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GradeProject.ScoreServie.Infrastructure.SqlServer
{
    public class GenericRepo<T> : IRepository<T> where T : class
    {
        private readonly ScoresContext _context;

        public GenericRepo(ScoresContext context)
        {
            _context = context;
        }

        public Task AddOneAsync(T newEntity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckASync(Expression<Func<T, bool>> statement)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOneAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<T> SingleAsync(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOneAsync(Expression<Func<T, bool>> filter, T entiy)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> WhereAsync(Expression<Func<T, bool>> filter, int count = 10, int page = 1)
        {
            throw new NotImplementedException();
        }
    }

}