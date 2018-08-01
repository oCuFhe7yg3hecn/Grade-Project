using GradeProject.GameCatalogService.Communication.Commands;
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

namespace GradeProject.GameCatalogService.Communication.CommandHandlers
{
    public class GameRegisteredCommandHandler : ICommandHandler<RegisterGameCommand>
    {
        private readonly ICatalogService _catalogSvc;

        public GameRegisteredCommandHandler(ICatalogService catalogSvc)
        {
            _catalogSvc = catalogSvc;
        }

        //public async Task ExecuteAsync(RegisterGameCommand command) => await _gameSvc.AddGameAsync(command.GameInfo);
        public async Task ExecuteAsync(RegisterGameCommand command) => await Task.FromResult(0);
    }
}
