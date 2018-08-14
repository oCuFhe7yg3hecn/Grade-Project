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
            }
        }
    }
}
