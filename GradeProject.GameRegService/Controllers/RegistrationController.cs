using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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

        [HttpPost]
        [Authorize(Roles = "developer")]
        public async Task<IActionResult> RegisterGame(string gameUrl)
        {
            //var client = new HttpClient();
            //var registerInfoendpoint = $"{gameUrl}/gradeproject-register-info";
            //var response = await client.GetAsync(registerInfoendpoint);

            //var response = JsonConvert.DeserializeObject<>(response);

            return Ok();
        }

    }
}