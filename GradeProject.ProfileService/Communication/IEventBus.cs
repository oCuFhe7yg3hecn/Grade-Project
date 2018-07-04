using GradeProject.ProfileService.Communication.CommandHandlers;
using GradeProject.ProfileService.Communication.Commands;
using RabbitMQ.Client.Events;

namespace GradeProject.ProfileService.Communication
{
    public interface IEventBus
    {
        //Commands to Events
        void Register(string routingKey, AddProfileCommand command);
        void Publish(byte[] data);
    }
}
