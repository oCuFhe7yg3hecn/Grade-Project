using GradeProject.GameCatalogService.Communication.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Communication.CommandHandlers
{
    interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task ExecuteAsync(TCommand command);
    }
}
