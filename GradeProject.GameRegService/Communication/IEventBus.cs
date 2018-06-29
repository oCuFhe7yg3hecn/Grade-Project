using GradeProject.GameRegService.Models;
using RabbitMQ.Client.Events;

namespace GradeProject.GameRegService.Communication
{
    public interface IEventBus
    {
        void AddToProfileService(GameInfo game);
    }


}
