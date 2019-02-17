using GradeProject.GameCatalogService.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GradeProject.GameCatalogService.Controllers
{
    [Produces("application/json")]
    [Route("api/Tags")]
    public class TagsController : Controller
    {
        private readonly ILogger<TagsController> _logger;
        private readonly ICatalogService _gameSvc;

        public TagsController(ILogger<TagsController> logger, ICatalogService gameSvc)
        {
            _logger = logger;
            _gameSvc = gameSvc;
        }

    }
}