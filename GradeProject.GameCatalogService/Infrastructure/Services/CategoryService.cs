using GradeProject.GameCatalogService.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Infrastructure.Repos
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _ctgRepo;
        private readonly IRepository<GameInfo> _gameRepo;

        public CategoryService(IRepository<Category> ctgRepo, IRepository<GameInfo> gameRepo)
        {
            _ctgRepo = ctgRepo;
            _gameRepo = gameRepo;
        }

        public async Task<List<Category>> AllAsync() =>
            await _ctgRepo.WhereAsync(_ => true);

        public async Task AddAsync(string name) =>
            await _ctgRepo.AddOneAsync(new Category(name));

        public async Task<bool> DeleteCategory(string name)
        {
            var updateFilter = new UpdateDefinitionBuilder<GameInfo>()
                                .Pull(x => x.Categories, name);

            var updateGames = await _gameRepo.UpdateManyAsync(g => g.Categories.Contains(name),
                                                              updateFilter);

            var res = await _ctgRepo.DeleteOneAsync(c => c.Name == name);
            return res;
        }
    }
}
