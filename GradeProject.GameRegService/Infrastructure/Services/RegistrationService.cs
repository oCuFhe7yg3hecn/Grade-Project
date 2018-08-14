using AutoMapper;
using GradeProject.GameRegService.Communication;
using GradeProject.GameRegService.Models;
using GradeProject.GameRegService.Utils;
using GradeProject.ProfileService.Infrastructure.Repos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GradeProject.GameRegService.Infrstructure
{
    public class RegistrationService : IRegistrationService
    {
        private readonly HttpClient _httpClient;
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;
        private readonly IRepository<GameRegInfo> _gameInfoRepo;

        public RegistrationService(
            IMapper mapper,
            IEventBus eventBus,
            IRepository<GameRegInfo> gameInfoRepo)
        {
            _mapper = mapper;
            _httpClient = new HttpClient();
            _eventBus = eventBus;
            _gameInfoRepo = gameInfoRepo;
        }

        public async Task<bool> RegisterGame(string gameUrl)
        {
            //var response = await _httpClient.GetStringAsync($"{gameUrl}/register-discover");
            //var responseObject = JsonConvert.DeserializeObject<RegisterInfo>(response);

            //var gameInfo = _mapper.Map<GameInfo>(responseObject);

            var gameInfo = new GameInfo() { Name = "Test new Rabbit Mq" };

            //Add some game validation logic here
            var bindata = ObjectConverter.ObjectToBytes(gameInfo);
            _eventBus.Publish(bindata);

            return true;
        }
    }
}
