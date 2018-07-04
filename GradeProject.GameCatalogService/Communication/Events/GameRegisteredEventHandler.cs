using GradeProject.GameCatalogService.Infrastructure;
using GradeProject.GameCatalogService.Infrastructure.Services;
using GradeProject.GameCatalogService.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Communication.Events
{
    public class GameRegisteredEventHandler
    {
        private readonly GamesService _gameSvc;

        public GameRegisteredEventHandler(IModel model, GamesService gameService)
        {
            _gameSvc = gameService;
        }

        public async void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var data = Encoding.Default.GetString(ea.Body);
            var gameInfo = JsonConvert.DeserializeObject<GameInfo>(data);
            await _gameSvc.AddGameAsync(gameInfo);
        }
    }
}
