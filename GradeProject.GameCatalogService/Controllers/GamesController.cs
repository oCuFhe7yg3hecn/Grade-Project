using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeProject.GameCatalogService.Infrastructure;
using GradeProject.GameCatalogService.Infrastructure.Services;
using GradeProject.GameCatalogService.Models;
using GradeProject.GameCatalogService.Models.DTO;
using GradeProject.GameCatalogService.Models.Exceptions;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using System.IO;

namespace GradeProject.GameCatalogService.Controllers
{
    [EnableCors("AllowAll")]
    [Produces("application/json")]
    [Route("api/Games")]
    [Authorize]
    public class GamesController : ODataController
    {
        private readonly ICatalogService _gamesSvc;
        private readonly ILogger<GamesController> _logger;
        private readonly IFilesSaveService _fileSaveSvc;
        private readonly IMapper _mapper;

        public GamesController(
            ICatalogService gamesSvc,
            ILogger<GamesController> logger,
            IMapper mapper,
            IFilesSaveService fileSaveSvc
            )
        {
            _gamesSvc = gamesSvc;
            _logger = logger;
            _mapper = mapper;
            _fileSaveSvc = fileSaveSvc;
        }


        public class UserScoreModel
        {
            public Guid UserId { get; set; }
            public double Scores { get; set; }
        }

        [HttpPost]
        [Route("PostTest")]
        public IActionResult PostScores([FromBody]UserScoreModel scores)
        {
            return Ok();
        }

        [HttpGet]
        //[Authorize]
        [EnableQuery]
        [EnableCors("AllowAll")]
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddGame([FromBody]GameRegisterModel newGame)
        {
            if(!ModelState.IsValid) { return BadRequest("Invalid Model"); }
            var gameInfo = _mapper.Map<GameInfo>(newGame);

            if (newGame.CoverImageUrl != null)
            {
                var fileName = $"images/Games/Covers/{Guid.NewGuid()}{Path.GetExtension(newGame.CoverImageUrl.FileName)}";
                await _fileSaveSvc.SaveFile(fileName, newGame.CoverImageUrl);
                gameInfo.CoverImageURL = fileName;
            }


            if (newGame.MultimediaFiles != null && newGame.MultimediaFiles.Count != 0)
            {
                foreach (var image in newGame.MultimediaFiles)
                {
                    var imageFileName = $"images/Games/{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                    await _fileSaveSvc.SaveFile(imageFileName, image);
                    gameInfo.MultiMedias.Add(imageFileName);
                }
            }

            await _gamesSvc.RegisterGameAsync(gameInfo);

            return Ok();

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