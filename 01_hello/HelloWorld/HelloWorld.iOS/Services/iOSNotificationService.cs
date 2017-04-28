using System;
using System.Threading.Tasks;
using HelloWorld.Core.Domain;
using HelloWorld.Core.Services;
using UIKit;
using Foundation;
namespace HelloWorld.iOS.Services
{
    public class iOSNotificationService : INotificationService
    {
        public iOSNotificationService ()
        {
        }

        public void CancelNotification (int id)
        {
            var idToCancel = @"some_id_to_cancel";
            UILocalNotification notificationToCancel = null;

            foreach (var notification in UIApplication.SharedApplication.ScheduledLocalNotifications) {
                if (notification.UserInfo.ObjectForKey (new NSString ("ID")).ToString () == idToCancel) {
                    notificationToCancel = notification;
                    break;
                }
            }

            if (notificationToCancel != null)
                UIApplication.SharedApplication.CancelLocalNotification (notificationToCancel);
        }

        public Task ScheduleNotification (CoreNotification notification)
        {
            var task = new TaskFactory ().StartNew (() => {
                var localNotification = new UILocalNotification ();
                localNotification.FireDate = NSDate.FromTimeIntervalSinceNow (60); //seconds
                localNotification.AlertBody = @"Your alert message";
                localNotification.TimeZone = NSTimeZone.DefaultTimeZone;
                localNotification.RepeatInterval = NSCalendarUnit.Day;
                //localNotification.RepeatCalendar = 

                UIApplication.SharedApplication.ScheduleLocalNotification (localNotification);
            });

            return task;
        }
    }
}
