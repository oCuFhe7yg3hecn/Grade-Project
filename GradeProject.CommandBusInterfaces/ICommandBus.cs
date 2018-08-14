using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GradeProject.CommandBusInterfaces
{
    public interface ICommandBus
    {
        Task SubmitAsync<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
