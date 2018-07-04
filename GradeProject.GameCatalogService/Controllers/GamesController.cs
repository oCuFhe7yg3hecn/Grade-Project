using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeProject.GameCatalogService.Infrastructure;
using GradeProject.GameCatalogService.Models;
using GradeProject.GameCatalogService.Models.DTO;
using GradeProject.GameCatalogService.Models.Exceptions;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GradeProject.GameCatalogService.Controllers
{
    [Produces("application/json")]
    [Route("api/Games")]
    public class GamesController : ODataController
    {
        private readonly IGameService _gamesSvc;
        private readonly ILogger<GamesController> _logger;

        public GamesController(IGameService gamesSvc)
        {
            _gamesSvc = gamesSvc;
        }

        [HttpGet]
        [EnableQuery]
        //PagingOptions pageOptions
        public async Task<IQueryable<GameInfoDTO>> Get()
        {
            var games = await _gamesSvc.GetAllAsync();
            return games.AsQueryable();
        }

        [HttpGet]
        [Route("{id:required}")]
        public async Task<IActionResult> Get(string id)
        {
            var game = await _gamesSvc.GetByIdAsync(id);
            return Ok(game);
        }

        // Into tags Controller
        //[HttpGet]
        //[Route("{tags}")]
        // public async Task<IActionResult> GetByTags(string tags, [FromQuery]int count = 10, [FromQuery]int page = 1)
        // {
        //     var tagsList = tags.Split(",").Select(x => x.Trim()).ToList();
        //     var games = await _gamesSvc.GetByTags(tagsList, count, page);
        //     return Ok(new
        //     {
        //         Page = page,
        //         Items = games.Count,
        //         Games = games,
        //     });
        // }
    }
}