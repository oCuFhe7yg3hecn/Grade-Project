using RabbitMQ.Client.Events;

namespace GradeProject.GameCatalogService.Communication
{
    public interface IEventBus
    {
        void Register(string queueName, EventingBasicConsumer consumer);
        void Publish(string queueName, string data);
    }
}
