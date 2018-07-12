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

        public async Task AddOneAsync(T newEntity)
        {
            await _context.Set<T>().AddAsync(newEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckASyncAsync(Expression<Func<T, bool>> statement)
        {
            return await _context.Set<T>().Where(statement).CountAsync() > 0;
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> filter = null)
        {
            return await _context.Set<T>().Where(filter).CountAsync();
        }

        public async Task<bool> DeleteOneAsync(Expression<Func<T, bool>> filter)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(filter);
            if (entity != null) { _context.Set<T>().Remove(entity); }
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<T> SingleAsync(Expression<Func<T, bool>> filter)
        {
            return _context.Set<T>().FirstOrDefaultAsync(filter);
        }

        public Task<bool> UpdateManyAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> updateDefinition)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOneAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> updateDefinition)
        {
            var user = new User();
            var user1 = new User();

            var diffProperties = new List<PropertyInfo>();

            foreach (var property in user.GetType().GetProperties())
            {
                var firstValue = property.GetValue(user, null);
                var secondValue = property.GetValue(user1, null);
                if (!firstValue.Equals(secondValue))
                {
                    diffProperties.Add(secondValue.GetType().GetProperty(property.Name));
                }
            }

            throw new NotImplementedException();
        }

        public async Task<List<T>> WhereAsync(Expression<Func<T, bool>> filter, int count = 10, int page = 1)
        {
            var items = await _context.Set<T>().CountAsync(_ => true);
            var skip = count * (page - 1);

            return await _context.Set<T>().Where(filter)
                                          .Skip(skip)
                                          .Take(10)
                                          .ToListAsync();
        }
    }

}