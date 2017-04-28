using System;
using HelloWorld.Core.ViewModels;
using Android.Views.Accessibility;
using Android.App;
using Android.OS;
using Android.Widget;
using MvvmCross.Binding.BindingContext;

namespace HelloWorld.Droid.Views
{
    [Activity]
    public class StartView : BaseView<StartViewModel>
    {

        protected override int LayoutResource => Resource.Layout.StartView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var goToAlert = FindViewById<Button>(Resource.Id.button_alert);
            var goToCross = FindViewById<Button>(Resource.Id.button_cross);
            var goToRxUI = FindViewById<Button>(Resource.Id.button_rxui);
            var goToNotifications = FindViewById<Button> (Resource.Id.button_notifications);


            var bSet = this.CreateBindingSet<StartView, StartViewModel>();

            bSet.Bind(goToAlert)
                .To(vm => vm.GoToAlertView);


            bSet.Bind(goToCross)
                .To(vm => vm.GoToMvxCanExecuteView);

            bSet.Bind(goToRxUI)
                .To(vm => vm.GoToRxUICanExecuteView);

            bSet.Bind(goToNotifications)
                .To (vm => vm.GoToNotificationsView);

            bSet.Apply();
        }
    }
}
