using System;
using Autofac;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeProject.ProfileService.Communication.Commands;
using GradeProject.ProfileService.Communication.CommandHandlers;

namespace GradeProject.ProfileService.Communication
{
    public class CommandBus : ICommandBus
    {
        public IContainer DependencyResolver { get; set; }

        public CommandBus()
        {
        }

        public async Task SubmitAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command != null)
            {
                var commandHandler = DependencyResolver.Resolve<ICommandHandler<TCommand>>();
                if (commandHandler != null) { await commandHandler.ExecuteAsync(command); }
            }
        }
    }
}
