using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeProject.GameCatalogService.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GradeProject.GameCatalogService.Controllers
{
    [Produces("application/json")]
    [Route("api/Games")]
    public class GamesController : Controller
    {
        private readonly GamesService _gamesSvc;
        private readonly ILogger<GamesController> _logger;

        public GamesController(GamesService gamesSvc, ILogger<GamesController> logger)
        {
            _gamesSvc = gamesSvc;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Games were requested!");
            var games = await _gamesSvc.GetAll();
            return Ok(games);
        }

        [HttpGet]
        [Route("{id:required}")]
        public async Task<IActionResult> Get(string id)
        {
            var game = await _gamesSvc.GetById(id);
            return Ok(game);
        }

        // Into tags Controller
        //[HttpGet]
        //[Route("{tags}")]
        //public async Task<IActionResult> GetByTags(string tags, [FromQuery]int count = 10, [FromQuery]int page = 1)
        //{
        //    var tagsList = tags.Split(",").Select(x => x.Trim()).ToList();
        //    var games = await _gamesSvc.GetByTags(tagsList, count, page);
        //    return Ok(new
        //    {
        //        Page = page,
        //        Items = games.Count,
        //        Games = games,
        //    });
        //}
    }
}