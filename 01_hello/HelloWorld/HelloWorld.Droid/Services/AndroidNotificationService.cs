using System;
using Android.App;
using HelloWorld.Core.Domain;
using HelloWorld.Core.Services;
using Android.Content;
using HelloWorld.Droid.Views;
using MvvmCross.Platform.Droid.Platform;
using Java.Util;
using System.Threading.Tasks;

namespace HelloWorld.Droid.Services
{
    public class AndroidNotificationService : INotificationService
    {
        private Context ctx;

        public AndroidNotificationService (Context context)
        {
            this.ctx = context;
        }

        public Task ScheduleNotification (CoreNotification coreNotification)
        {
            var task = new TaskFactory ().StartNew (() => {
                // todo return an identifier(s) for a notification(s) to store in db to be able to cancel them later
                // todo create notifications for all occrrences using AlarmManager.SetRepeating method
                var notification = this.GetNotification (coreNotification, DateTime.Now.Date);
                Intent notificationIntent = new Intent (this.ctx, typeof (NotificationPublisher));
                notificationIntent.PutExtra (NotificationPublisher.NOTIFICATION_ID, coreNotification.Id);
                notificationIntent.PutExtra (NotificationPublisher.NOTIFICATION, notification);
                PendingIntent pendingIntent = PendingIntent.GetBroadcast (this.ctx, 0, notificationIntent, PendingIntentFlags.UpdateCurrent);

                var firingCal = Calendar.Instance;
                var currentCal = Calendar.Instance;

                // todo set time according to occurrence
                firingCal.Set (CalendarField.Hour, 14); // At the hour you wanna fire
                firingCal.Set (CalendarField.Minute, 03); // Particular minute
                firingCal.Set (CalendarField.Second, 17); // particular second
                if (firingCal.CompareTo (currentCal) < 0) {
                    firingCal.Add (CalendarField.DayOfMonth, 1);
                }
                var triggerTime = firingCal.TimeInMillis; // DateTime.Now.FromLocalToUnixTime();

                AlarmManager alarmManager = (AlarmManager)this.ctx.GetSystemService (Context.AlarmService);
                alarmManager.SetRepeating (AlarmType.RtcWakeup, triggerTime, AlarmManager.IntervalFifteenMinutes /* or explicit value of millis, for example 10000*/, pendingIntent);
                // or
                //alarmManager.SetExact();
                // or others
            });

            return task;
        }

        public void CancelNotification (int id)
        {
            var alarmManager = (AlarmManager)this.ctx.GetSystemService (Context.AlarmService);
            Intent intent = new Intent (this.ctx, typeof (NotificationPublisher));
            PendingIntent alarmIntent = PendingIntent.GetBroadcast (this.ctx, 0, intent, 0);

            alarmManager.Cancel (alarmIntent);
        }


        /// <summary>
        /// Creates a single notification, an instance of <see cref="Android.App.Notification"/> class.
        /// </summary>
        /// <returns>Android notification created from <b>notification</b> parameter.</returns>
        /// <param name="notification"><b>CoreNotification</b></param>
        private Notification GetNotification (CoreNotification notification, DateTime occurrence)
        {
        	var builder = new Notification.Builder (this.ctx);
        	builder.SetContentTitle (notification.Title);
        	builder.SetContentText (notification.Message + this.FormatOccurrence(occurrence));
        	builder.SetSmallIcon (Resource.Drawable.Icon);
        	return builder.Build ();
        }

        private string FormatOccurrence(DateTime occurrence)
        {
            return $"(Data przyjęcia: {occurrence.ToString("g")})";
        }
    }
}
