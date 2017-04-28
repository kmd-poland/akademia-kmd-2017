using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using MvvmCross.Platform.Platform;
using UIKit;
using MvvmCross.Platform;
using HelloWorld.Core.Services;
using HelloWorld.iOS.Services;

namespace HelloWorld.iOS
{
    public class Setup : MvxIosSetup
    {
        public Setup (MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base (applicationDelegate, window)
        {
        }

        public Setup (MvxApplicationDelegate applicationDelegate, IMvxIosViewPresenter presenter)
            : base (applicationDelegate, presenter)
        {
        }

        protected override IMvxApplication CreateApp ()
        {
            return new Core.App ();
        }

        protected override IMvxTrace CreateDebugTrace ()
        {
            return new DebugTrace ();
        }

        protected override void InitializeIoC ()
        {
            base.InitializeIoC ();

            Mvx.RegisterSingleton<INotificationService> (new iOSNotificationService());
        }
    }
}
