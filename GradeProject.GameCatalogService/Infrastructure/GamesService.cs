using GradeProject.GameCatalogService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Infrastructure
{
    public class GamesService
    {
        private readonly GamesRepository _repo;

        public GamesService(GamesRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<GameInfo>> GetAll()
        {
            return await _repo.Where(_ => true);
        }

        public async Task<GameInfo> GetById(string id)
        {
            return await _repo.Single(g => g.Id == Guid.Parse(id));
        }

        public async Task<List<GameInfo>> GetByTags(List<string> tags, int count, int page)
        {
            return await _repo
                .Where(g => g.Tags.Any(tag => tags.Contains(tag)), count, page);
        }
    }
}
