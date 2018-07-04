using GradeProject.ProfileService.Communication.Commands;
using GradeProject.ProfileService.Infrastructure;
using GradeProject.ProfileService.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeProject.ProfileService.Communication.CommandHandlers
{
    public class AddProfileCommandHandler : ICommandHandler<AddProfileCommand>
    {
        private readonly UserService _gameSvc;

        public AddProfileCommandHandler(UserService gameService)
        {
            _gameSvc = gameService;
        }

        public async Task ExecuteAsync(AddProfileCommand command) => await _gameSvc.CreateUser(command.UserInfo);
    }
}
