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
    public class GameRegisteredConsumer : EventingBasicConsumer
    {
        private readonly IGameService _gameSvc;

        public GameRegisteredConsumer(IModel model, IGameService gameService) : base(model)
        {
            _gameSvc = gameService;
            this.Received += Handle;
        }

        private void Handle(object sender, BasicDeliverEventArgs ea)
        {
            var data = Encoding.Default.GetString(ea.Body);
            var gameInfo = JsonConvert.DeserializeObject<GameInfo>(data);
            _gameSvc.AddGameAsync(gameInfo);
        }
    }
}
