using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameRegService.Communication.Events
{
    public class TestEventConsumer : EventingBasicConsumer
    {
        public TestEventConsumer(IModel model) : base(model)
        {
        }
    }
}
