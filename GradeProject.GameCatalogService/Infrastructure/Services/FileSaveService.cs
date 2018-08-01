using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace GradeProject.GameCatalogService.Infrastructure.Services
{
    public class FileSaveService : IFilesSaveService
    {
        private readonly IHostingEnvironment _env;

        public FileSaveService(IHostingEnvironment env)
        {
            _env = env;
        }

        public async Task SaveFile(string path, IFormFile file)
        {
            var srvFileName = $"{_env.WebRootPath}/{path}";
            using (var stream = new FileStream(srvFileName, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

        }
    }
}
