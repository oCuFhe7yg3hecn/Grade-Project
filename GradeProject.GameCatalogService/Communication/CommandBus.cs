using System;
using Autofac;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeProject.GameCatalogService.Communication.Commands;
using GradeProject.GameCatalogService.Communication.CommandHandlers;
using GradeProject.GameCatalogService.Filters;
using GradeProject.GameCatalogService;
using GradeProject.CommandBusInterfaces;

namespace GradeProject.GameCatalogService.Communication
{
    public class CommandBus : ICommandBus
    {
        private readonly IComponentContext _componentContext;

        public CommandBus(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public async Task SubmitAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command != null)
            {
                var handler = _componentContext.Resolve<ICommandHandler<TCommand>>();
                if (handler != null) { await handler.ExecuteAsync(command); }
                else { throw new ApiError("There no specified command handler for this command."); }
            }
        }
    }
}
