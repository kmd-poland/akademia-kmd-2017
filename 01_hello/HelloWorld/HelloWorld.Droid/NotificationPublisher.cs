using Android.App;
using Android.Content;
using Android.Content.PM;

namespace HelloWorld.Droid.Views
{
    [BroadcastReceiver]
    public class NotificationPublisher : BroadcastReceiver
    {
        public static string NOTIFICATION_ID = "notification-id";
        public static string NOTIFICATION = "notification";

        public override void OnReceive (Context context, Intent intent)
        {
            NotificationManager notificationManager = (NotificationManager)context.GetSystemService (Context.NotificationService);

            Android.App.Notification notification = intent.GetParcelableExtra (NOTIFICATION) as Android.App.Notification;
            int id = intent.GetIntExtra (NOTIFICATION_ID, 0);
            notificationManager.Notify (id, notification);
        }

        public void CancelAlarm (Context context)
        {
            var alarmManager = (AlarmManager)context.GetSystemService (Context.AlarmService);
            Intent intent = new Intent (context, typeof (NotificationPublisher));
            PendingIntent alarmIntent = PendingIntent.GetBroadcast (context, 0, intent, 0);

            alarmManager.Cancel (alarmIntent);

            // Disable <see cref="NotificationPublisher"/> so that it doesn't automatically restart when the device is rebooted.
            // TODO: you may need to reference the context by ApplicationActivity.class
            ComponentName receiver = new ComponentName (context, Java.Lang.Class.FromType(typeof (NotificationPublisher)));
            PackageManager pm = context.PackageManager;
            pm.SetComponentEnabledSetting (receiver, ComponentEnabledState.Disabled, ComponentEnableOption.DontKillApp);
        }
    }
}
