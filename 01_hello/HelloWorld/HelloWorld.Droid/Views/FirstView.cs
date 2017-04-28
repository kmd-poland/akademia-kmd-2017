using Android.App;
using Android.Content;
using Android.OS;
using MvvmCross.Binding.BindingContext;
using HelloWorld.Core.ViewModels;
using Android.Widget;
using ReactiveUI;
using System.Reactive;
using Android.Support.Design.Widget;
using System;
using System.Reactive.Linq;
using MvvmCross.Platform.Droid.Platform;
using Android.Runtime;
using Java.Lang;
using Android.Content.PM;
using HelloWorld.Core.Services;
using MvvmCross.Platform;
using System.Reactive.Disposables;
using Java.Sql;

namespace HelloWorld.Droid.Views
{
    [Activity (Label = "View for FirstViewModel")]
    public class FirstView : BaseView<FirstViewModel>
    {
        private INotificationService notifications = Mvx.Resolve<INotificationService>();

        protected override int LayoutResource => Resource.Layout.FirstView;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SupportActionBar.SetDisplayHomeAsUpEnabled (false);
        }
    }
}
