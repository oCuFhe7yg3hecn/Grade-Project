using GradeProject.GameRegService.Communication.Commands;
using GradeProject.GameRegService.Infrastructure;
using GradeProject.GameRegService.Infrastructure.Services;
using GradeProject.GameRegService.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeProject.GameRegService.Communication.CommandHandlers
{
    public class GameRegisteredCommandHandler : ICommandHandler<RegisterGameCommand>
    {
        private readonly IGamesService _gameSvc;

        public GameRegisteredCommandHandler(IGamesService gameService)
        {
            _gameSvc = gameService;
        }

        public async Task ExecuteAsync(RegisterGameCommand command) => await _gameSvc.AddGameAsync(command.GameInfo);
    }
}
