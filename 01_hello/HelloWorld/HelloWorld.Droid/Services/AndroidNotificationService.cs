using System;
using Android.App;
using HelloWorld.Core.Domain;
using HelloWorld.Core.Services;
using Android.Content;
using HelloWorld.Droid.Views;
using MvvmCross.Platform.Droid.Platform;
using Java.Util;
using System.Threading.Tasks;
using Android.Support.V7.App;
using Android.Media;

namespace HelloWorld.Droid.Services
{
    public class AndroidNotificationService : INotificationService
    {
        private readonly static string KEY_TEXT_REPLY = "key_action1";

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

                Intent notificationIntent = new Intent (this.ctx, typeof (NotificationPublisher));
                var notification = this.GetNotification (coreNotification, DateTime.Now.Date, notificationIntent);
                notification.Defaults |= NotificationDefaults.Lights | NotificationDefaults.Sound | NotificationDefaults.Vibrate;

                notificationIntent.PutExtra (NotificationPublisher.NOTIFICATION_ID, coreNotification.Id); // here we put long but NotificationPublisher expects int
                notificationIntent.PutExtra (NotificationPublisher.NOTIFICATION, notification);
                var requestId = DateTime.Now.Millisecond;
                PendingIntent pendingIntent = PendingIntent.GetBroadcast (this.ctx, requestId, notificationIntent, PendingIntentFlags.CancelCurrent);

                var firingCal = Calendar.Instance;
                var currentCal = Calendar.Instance;

                // todo set time according to occurrence
                firingCal.Set (CalendarField.HourOfDay, DateTime.Now.Hour); // At the hour you wanna fire
                firingCal.Set (CalendarField.Minute, DateTime.Now.Minute); // Particular minute
                firingCal.Set (CalendarField.Second, DateTime.Now.Second + 4); // particular second
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
            PendingIntent alarmIntent = PendingIntent.GetBroadcast (this.ctx, id, intent, 0);

            alarmManager.Cancel (alarmIntent);
        }


        /// <summary>
        /// Creates a single notification, an instance of <see cref="Android.App.Notification"/> class.
        /// </summary>
        /// <returns>Android notification created from <b>notification</b> parameter.</returns>
        /// <param name="notification"><b>CoreNotification</b></param>
        private Notification GetNotification (CoreNotification notification, DateTime occurrence, Intent notificationIntent)
        {
        	var builder = new NotificationCompat.Builder (this.ctx);
        	builder.SetContentTitle (notification.Title);
        	builder.SetContentText (notification.Message + this.FormatOccurrence(occurrence));
            builder.SetTicker ("Ticker");
        	builder.SetSmallIcon (Resource.Drawable.Icon);

            builder.SetSound (RingtoneManager.GetDefaultUri(RingtoneType.Alarm));
            builder.SetPriority ((int)NotificationPriority.High);
            builder.SetVisibility ((int)NotificationVisibility.Public); // visible on locked screen

            var action = this.GetAction (builder, notificationIntent, () => { System.Diagnostics.Debug.WriteLine ("ACTION!"); });
            builder.AddAction (action);

        	return builder.Build ();
        }

        private NotificationCompat.Action GetAction(NotificationCompat.Builder builder, Intent notificationIntent, Action actualAction)
        {
            var remoteInput = new Android.Support.V4.App.RemoteInput.Builder (KEY_TEXT_REPLY).SetLabel ("Akcja 1").Build ();
            var pendingIntent = PendingIntent.GetBroadcast (this.ctx, 0, notificationIntent, PendingIntentFlags.UpdateCurrent);
            return new NotificationCompat.Action.Builder (Resource.Drawable.Icon, "Akcja 1", pendingIntent).AddRemoteInput (remoteInput).Build ();
        }

        private string FormatOccurrence(DateTime occurrence)
        {
            return $"(Data przyjęcia: {occurrence.ToString("g")})";
        }
    }
}
