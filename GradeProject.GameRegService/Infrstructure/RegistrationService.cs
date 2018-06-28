using GradeProject.GameRegService.Communication;
using GradeProject.GameRegService.Models;
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
        private readonly IEventBus _eventBus;

        public RegistrationService(HttpClient httpClient, IEventBus eventBus)
        {
            _httpClient = httpClient;
            _eventBus = eventBus;
        }

        public async Task<bool> RegisterGame(GameRegisterModel model)
        {
            var gameInfoResponse = await _httpClient.GetStringAsync(model.DocumentationUrl);
            var gameInfo = JsonConvert.DeserializeObject<GameInfo>(gameInfoResponse);

            //Add some game validation logic here

            _eventBus.AddToProfileService(gameInfo);

            return true;
        }
    }
}
