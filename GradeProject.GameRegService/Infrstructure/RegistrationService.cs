using AutoMapper;
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
    public class RegistrationService : IRegistrationService
    {
        private readonly HttpClient _httpClient;
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;

        public RegistrationService(IEventBus eventBus, IMapper mapper)
        {
            _mapper = mapper;
            _httpClient = new HttpClient();
            _eventBus = eventBus;
        }

        public async Task<bool> RegisterGame(string gameUrl)
        {
            var response = await _httpClient.GetStringAsync($"{gameUrl}/register-discover");
            var responseObject = JsonConvert.DeserializeObject<RegisterInfo>(response);

            var gameInfo = _mapper.Map<GameInfo>(responseObject);

            //Add some game validation logic here

            _eventBus.AddToProfileService(gameInfo);

            return true;
        }
    }
}
