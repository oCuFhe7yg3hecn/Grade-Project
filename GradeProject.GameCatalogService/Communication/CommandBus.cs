using System;
using Autofac;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeProject.GameCatalogService.Communication.Commands;
using GradeProject.GameCatalogService.Communication.CommandHandlers;
using GradeProject.GameCatalogService.Filters;

namespace GradeProject.GameCatalogService.Communication
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
                else { throw new ApiError("There no specified command handler for this command."); }
            }
        }
    }
}
