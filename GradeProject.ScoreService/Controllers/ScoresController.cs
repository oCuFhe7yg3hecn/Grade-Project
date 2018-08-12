using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GradeProject.ScoreService.Infrastructure.Repos;
using GradeProject.ScoreService.Models;
using GradeProject.ScoreService.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GradeProject.ScoreService.Controllers
{
    [Produces("application/json")]
    [Route("api/Scores")]
    [EnableCors("AllowAll")]
    public class ScoresController : BaseController
    {
        private readonly IScoreService _scoreSvc;
        private readonly IMapper _mapper;

        public string GameName
        {
            get => GetUserClaim("GameName");
        }

        public ScoresController(
            IScoreService scoreSvc,
            IMapper mapper)
        {
            _scoreSvc = scoreSvc;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [HttpGet]
        [Route("user/{id:guid}")]
        public async Task<IActionResult> GetUserScores(string id)
        {
            var res = await _scoreSvc.GetUserScores(id);
            return Ok(res);
        }

        [HttpGet]
        [Route("game/{gameName}")]
        public async Task<IActionResult> GetGameTopScores(string gameName)
        {
            var res = await _scoreSvc.GetGameScores(gameName);
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> SetScores([FromBody]ScoresPostModel scoresPost)
        {
            var score = _mapper.Map<Score>(scoresPost);
            score.Game = "Game";

            await _scoreSvc.AddScore(score);
            return NoContent();
        }
    }
}