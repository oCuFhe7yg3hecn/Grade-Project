using GradeProject.GameRegService.Communication.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameRegService.Communication
{
    public interface IIntegrationEventHandler<T> where T : IntegrationEvent
    {
    }
}
