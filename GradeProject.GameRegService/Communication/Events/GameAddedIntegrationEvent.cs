using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameRegService.Communication.Events
{
    public class GameAddedIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; set; }

        public GameAddedIntegrationEvent()
        {

        }
    }
}
