using System;
using HelloWorld.Core.Domain;
using System.Threading.Tasks;

namespace HelloWorld.Core.Services
{
    public interface INotificationService
    {
        Task ScheduleNotification (CoreNotification notification);
        void CancelNotification (int id);
    }
}
