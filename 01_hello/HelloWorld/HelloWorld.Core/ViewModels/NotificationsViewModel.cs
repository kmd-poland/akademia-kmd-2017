using System;
using System.Reactive;
using System.Threading.Tasks;
using HelloWorld.Core.Domain;
using HelloWorld.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using ReactiveUI;

namespace HelloWorld.Core.ViewModels
{
    public class NotificationsViewModel : MvxViewModel
    {
        private INotificationService notificationService = Mvx.Resolve<INotificationService> ();

        private string title = "Tytuł powiadomienia!";
        public string Title {
            get { return title; }
            set { this.SetProperty (ref title, value); }
        }

        private string message = "Treść powiadomienia. Nazwa leku, dawka, godzina przyjęcia.";
        public string Message {
            get { return message; }
            set { this.SetProperty (ref message, value); }
        }

        private int? time = 666;
        public int? Time {
            get { return time; }
            set { this.SetProperty (ref time, value); }
        }

        public ReactiveCommand<Unit, Unit> AddNotification {
            get;
            private set;
        }

        public NotificationsViewModel ()
        {
            this.AddNotification = ReactiveCommand.CreateFromTask<Unit, Unit> (async (a) => {
                var notification = await CreateNotificationAsync ();
                await this.notificationService.ScheduleNotification (notification);
                return Unit.Default;
            }, this.AreAllControlsValidObservable ());
        }

        private async Task<CoreNotification> CreateNotificationAsync ()
        {
            return await Task.FromResult (new CoreNotification (DateTime.Now.Ticks, this.Title, this.Message, new RepeatPattern ()));
        }

        private IObservable<bool> AreAllControlsValidObservable ()
        {
            return this.WhenAnyValue (t => t.Title, t => t.Time, t => t.Message, (title, time, message) => !string.IsNullOrEmpty (title) && !string.IsNullOrEmpty (message) && time.HasValue && time.Value > 0);
        }
    }
}
