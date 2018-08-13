using GradeProject.ProfileService.Communication.Commands;
using GradeProject.ProfileService.Communication.CommandHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace GradeProject.ProfileService.Communication
{
    public interface ICommandBus
    {
        Task SubmitAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
