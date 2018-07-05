using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeProject.GameCatalogService.Infrastructure;
using GradeProject.GameCatalogService.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GradeProject.GameCatalogService.Controllers
{
    [Produces("application/json")]
    [Route("api/Tags")]
    public class TagsController : Controller
    {
        private readonly ILogger<TagsController> _logger;
        private readonly IGamesService _gameSvc;

        public TagsController(ILogger<TagsController> logger, IGamesService gameSvc)
        {
            _logger = logger;
            _gameSvc = gameSvc;
        }

    }
}