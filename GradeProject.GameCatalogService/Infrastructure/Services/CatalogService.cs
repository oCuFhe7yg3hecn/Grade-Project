using AutoMapper;
using GradeProject.GameCatalogService.Infrastructure.Repos;
using GradeProject.GameCatalogService.Models;
using GradeProject.GameCatalogService.Models.DTO;
using GradeProject.GameCatalogService.Models.Exceptions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Infrastructure.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<GameInfo> _repo;

        public CatalogService(IRepository<GameInfo> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<GameInfoDTO>> GetAllAsync()
        {
            var games = await _repo.AllAsync();
            return games.Select(g => _mapper.Map<GameInfoDTO>(g)).ToList();
        }

        public async Task<GameInfo> GetByIdAsync(string id) =>
            await _repo.SingleAsync(g => g.Id == Guid.Parse(id));

        public async Task<List<GameInfo>> GetByTagsAsync(List<string> tags, int count, int page) =>
            await _repo.WhereAsync(g => g.Tags.Any(tag => tags.Contains(tag)),
                                  count,
                                  page);

        public async Task<PaginatedResponse<GameInfo>> GetByCategoriesAsync(List<string> categories, PagingOptions opts)
        {
            var itemsCount = await _repo.CountAsync(g => g.Categories.Any(category => categories.Contains(category)));
            if (itemsCount == 0) { return new PaginatedResponse<GameInfo>(opts.Page, opts.PageSize, itemsCount, null); }

            var games = await _repo.WhereAsync(g => g.Categories.Any(category => categories.Contains(category)),
                                               opts.PageSize,
                                               opts.Page);

            return new PaginatedResponse<GameInfo>(opts.Page, opts.PageSize, itemsCount / opts.PageSize, games);
        }

        public async Task RegisterGameAsync(GameInfo newGame) => await _repo.AddOneAsync(newGame);

    }
}
