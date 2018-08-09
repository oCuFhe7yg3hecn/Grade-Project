using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeProject.ScoreService.Infrastructure.Repos;
using GradeProject.ScoreService.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GradeProject.ScoreService.Controllers
{
    [Produces("application/json")]
    [Route("api/Scores")]
    [EnableCors("AllowAll")]
    public class ScoresController : Controller
    {
        private readonly IRepository<UserScores> _userRepo;

        public ScoresController(IRepository<UserScores> userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _userRepo.WhereAsync(_ => true));
        }


    }
}