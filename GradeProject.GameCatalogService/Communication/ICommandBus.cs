using GradeProject.GameCatalogService.Communication.Commands;
using GradeProject.GameCatalogService.Communication.CommandHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;

namespace GradeProject.GameCatalogService.Communication
{
    public interface ICommandBus
    {
        IContainer DependencyResolver { get; set; }
        Task SubmitAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
