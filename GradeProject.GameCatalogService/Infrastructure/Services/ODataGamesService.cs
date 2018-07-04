using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GradeProject.GameCatalogService.Infrastructure.Repos;
using GradeProject.GameCatalogService.Models;
using GradeProject.GameCatalogService.Models.DTO;

namespace GradeProject.GameCatalogService.Infrastructure.Services
{
    public class ODataGamesService : IGamesService
    {
        private readonly IRepository<GameInfo> _gamesRepo;
        private readonly IMapper _mapper;

        public ODataGamesService(IRepository<GameInfo> gamesRepo, IMapper mapper)
        {
            _gamesRepo = gamesRepo;
            _mapper = mapper;
        }

        public Task<List<GameInfo>> GetAllAsync(PagingOptions pageOptions)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<GameInfoDTO>> GetAllAsync()
        {
            var games = await _gamesRepo.WhereAsync(_ => true);

            return games.Select(g => _mapper.Map<GameInfoDTO>(g)).ToList();
        }

        public Task<PaginatedResponse<GameInfo>> GetByCategoriesAsync(List<string> categories, PagingOptions opts)
        {
            throw new System.NotImplementedException();
        }

        public Task<GameInfo> GetByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<GameInfo>> GetByTagsAsync(List<string> tags, int count, int page)
        {
            throw new System.NotImplementedException();
        }
    }
}