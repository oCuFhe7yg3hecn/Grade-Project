using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GradeProject.GameRegService.Communication;
using GradeProject.GameRegService.Communication.Events;
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
        private readonly IEventBus _eventBus;
        private readonly HttpClient _httpClient;

        public RegistrationController(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _httpClient = new HttpClient();
        }


        [HttpPost]
        public async Task<IActionResult> RegisterGame([FromBody]string gameUrl)
        {
            var response = await _httpClient.GetStringAsync($"{gameUrl}/register-discover");
            var responseObject = JsonConvert.DeserializeObject<GameInfo>(response);



            _eventBus.AddToProfileService(responseObject);
            return Ok();
        }

    }
}