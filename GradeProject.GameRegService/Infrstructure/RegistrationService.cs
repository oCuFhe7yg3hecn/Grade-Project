using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GradeProject.GameRegService.Infrstructure
{
    public class RegistrationService
    {
        private readonly HttpClient _httpClient;

        public RegistrationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterGame(GameRegisterModel model)
        {
            var gameInfoResponse = await _httpClient.GetStringAsync(model.DocumentationUrl);
            var gameInfo = JsonConvert.DeserializeAnonymousType<>(gameInfoResponse);

            //Call RabitMQ to Catalog Service

            //Call RabitMQ to Auth Service
        }
    }
}
