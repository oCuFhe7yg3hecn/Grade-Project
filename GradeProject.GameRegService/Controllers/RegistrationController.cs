using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using GradeProject.GameRegService.Communication;
using GradeProject.GameRegService.Infrstructure;
using GradeProject.GameRegService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GradeProject.GameRegService.Controllers
{
    [Produces("application/json")]
    [Route("api/Registration")]
    public class RegistrationController : Controller
    {
        private readonly IRegistrationService _regSvc;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public RegistrationController(IRegistrationService regSvc, IMapper mapper)
        {
            _regSvc = regSvc;
            _httpClient = new HttpClient();
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> RegisterGame([FromBody]string gameUrl)
        {
            if (await _regSvc.RegisterGame(gameUrl)) { return Ok(); }
            return BadRequest("Something gone wrong");
        }

    }
}