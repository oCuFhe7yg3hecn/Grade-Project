using System.Collections.Generic;
using System.Threading.Tasks;
using GradeProject.GameCatalogService.Models;
using GradeProject.GameCatalogService.Models.DTO;

namespace GradeProject.GameCatalogService.Infrastructure.Services
{
    public interface IGamesService
    {
        Task<List<GameInfoDTO>> GetAllAsync();

        Task<GameInfo> GetByIdAsync(string id);

        Task<List<GameInfo>> GetByTagsAsync(List<string> tags, int count, int page);

        Task<PaginatedResponse<GameInfo>> GetByCategoriesAsync(List<string> categories, PagingOptions opts);

        Task AddGameAsync(GameInfo newGame);
    }
}
