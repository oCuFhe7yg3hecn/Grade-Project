using GradeProject.GameCatalogService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Infrastructure.Services
{
    public interface IGameService
    {
        Task<List<GameInfo>> GetAllAsync(PagingOptions pageOptions);

        Task<GameInfo> GetByIdAsync(string id);

        Task<List<GameInfo>> GetByTagsAsync(List<string> tags, int count, int page);

        Task<PaginatedResponse<GameInfo>> GetByCategoriesAsync(List<string> categories, PagingOptions opts);

        Task AddGameAsync(GameInfo games);
    }
}
