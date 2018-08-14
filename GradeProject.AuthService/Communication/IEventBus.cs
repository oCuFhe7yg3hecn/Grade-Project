using GradeProject.AuthService.Models.Account.Register;
using RabbitMQ.Client.Events;

namespace GradeProject.AuthService.Communication
{
    public interface IEventBus
    {
        void SendProfile(ProfileRegisterModel profile);
    }
}
