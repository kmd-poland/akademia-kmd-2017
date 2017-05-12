using MvvmCross.Core.ViewModels;
using System.Threading.Tasks;
using System;
using ReactiveUI;
using System.Reactive;
using HelloWorld.Core.Domain;
using HelloWorld.Core.Services;
using MvvmCross.Platform;

namespace HelloWorld.Core.ViewModels
{
    public class FirstViewModel : MvxViewModel
    {
        private INotificationService notificationService = Mvx.Resolve<INotificationService> ();

        private string title = "Tytuł powiadomienia!";
        public string Title {
            get { return title; }
            set { SetProperty (ref title, value); }
        }

        private string message = "Treść powiadomienia. Nazwa leku, dawka, godzina przyjęcia.";
        public string Message {
            get { return message; }
            set { SetProperty (ref message, value); }
        }

        private int? time = 666;
        public int? Time {
        	get { return time; }
        	set { SetProperty (ref time, value); }
        }

        public ReactiveCommand<Unit, Unit> AddNotification
        {
            get;
            private set;
        }

        public FirstViewModel()
        {
            this.AddNotification = ReactiveCommand.CreateFromTask<Unit, Unit>(async (a) => {
                var notification = await CreateNotificationAsync ();
                await this.notificationService.ScheduleNotification (notification);
                return Unit.Default;
            }, this.AreAllControlsValidObservable());
        }

        private async Task<CoreNotification> CreateNotificationAsync()
        {
            return await Task.FromResult(new CoreNotification(DateTime.Now.Millisecond, this.Title, this.Message, new RepeatPattern()));
        }

        private IObservable<bool> AreAllControlsValidObservable()
        {
            return this.WhenAnyValue (t => t.Title, t => t.Time, t => t.Message, (title, time, message) => !string.IsNullOrEmpty (title) && !string.IsNullOrEmpty (message) && time.HasValue && time.Value > 0);
        }
    }

    
}
