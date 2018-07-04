using GradeProject.GameCatalogService.Communication.Commands;
using RabbitMQ.Client.Events;

namespace GradeProject.GameCatalogService.Communication
{
    public interface IEventBus
    {
        //Commands to Events
        void Register(string routingKey, RegisterGameCommand command);
        void Publish(string queueName, string data);
    }
}
