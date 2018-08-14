using GradeProject.GameRegService.Communication.Commands;
using GradeProject.GameRegService.Communication.CommandHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace GradeProject.GameRegService.Communication
{
    public interface ICommandBus
    {
        IContainer DependencyResolver { get; set; }
        Task SubmitAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
