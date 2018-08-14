using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Infrastructure.Services
{
    public interface IFilesSaveService
    {
        Task SaveFile(string path, IFormFile file);
    }
}
