using GradeProject.GameCatalogService.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Infrastructure.Repos
{
    public class CategoryService
    {
        private readonly CategoryRepository _ctgRepo;
        private readonly GamesRepository _gameRepo;

        public CategoryService(CategoryRepository ctgRepo, GamesRepository gameRepo)
        {
            _ctgRepo = ctgRepo;
            _gameRepo = gameRepo;
        }

        public async Task<List<Category>> AllAsync() =>
            await _ctgRepo.Where(_ => true);

        public async Task AddAsync(string name) =>
            await _ctgRepo.Add(new Category(name));

        public async Task<bool> DeleteCategory(string name)
        {
            var updateFilter = new UpdateDefinitionBuilder<GameInfo>()
                                .Pull(x => x.Categories, name);

            var updateGames = await _gameRepo.UpdateManyAsync(updateFilter);
            var res = await _ctgRepo.Delete(c => c.Name == name);
            return res;
        }
    }
}
