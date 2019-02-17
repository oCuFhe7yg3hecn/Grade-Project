using GradeProject.ProfileService.Communication.Commands;

namespace GradeProject.ProfileService.Communication
{
    public interface IEventBus
    {
        void Register(string routingKey, AddProfileCommand command);
        void Publish(byte[] data);
    }
}
