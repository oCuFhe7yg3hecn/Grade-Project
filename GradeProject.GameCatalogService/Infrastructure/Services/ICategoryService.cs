using GradeProject.GameCatalogService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Infrastructure
{
    public interface ICategoryService
    {
        Task<List<Category>> AllAsync();

        Task AddAsync(string name);

        Task<bool> DeleteCategory(string name);
    }
}
