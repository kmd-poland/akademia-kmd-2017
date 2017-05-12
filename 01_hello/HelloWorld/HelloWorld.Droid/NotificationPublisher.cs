using Android.App;
using Android.Content;
using Android.Content.PM;
using System.Threading;
using MvvmCross.Core.ViewModels;
using HelloWorld.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Droid.Views;

namespace HelloWorld.Droid.Views
{
    [BroadcastReceiver]
    public class NotificationPublisher : BroadcastReceiver
    {
        public static string NOTIFICATION_ID = "notification-id";
        public static string NOTIFICATION = "notification";

        public override void OnReceive (Context context, Intent intent)
        {
            NotificationManager notificationManager = 
                (NotificationManager)context.GetSystemService (Context.NotificationService);

            var notification = intent.GetParcelableExtra (NOTIFICATION) as Notification;

            if (notification != null) {
                var id = intent.GetIntExtra (NOTIFICATION_ID, 0);
                notificationManager.Notify (id, notification);
            } else {
                System.Diagnostics.Debug.WriteLine (intent.Action);
                //var i = new Intent (context, typeof (NotifiedView));
                //i.SetFlags (ActivityFlags.NewTask);
                //context.StartActivity (i);

                //if (intent.Action == "Akcja 1") {
                    var request = new MvxViewModelRequest ();
                    request.ParameterValues = new System.Collections.Generic.Dictionary<string, string>();                        
                    request.ParameterValues.Add("medicationId", 987.ToString());
                    request.ViewModelType = typeof (NotifiedViewModel);
                    var requestTranslator = Mvx.Resolve<IMvxAndroidViewModelRequestTranslator> ();
                    var newActivity = requestTranslator.GetIntentFor (request);
                    newActivity.SetFlags (ActivityFlags.NewTask);
                    context.StartActivity(newActivity);
                //}
            }
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
