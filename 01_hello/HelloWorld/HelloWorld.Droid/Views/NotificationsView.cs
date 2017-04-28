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
    public class NotificationsView : BaseView<NotificationsViewModel>
	{
		private EditText titleEditText, timeEditText, messageEditText;
		private Button addNotificationButton;
		private TextInputLayout titleInputLayout, timeInputLayout, messageInputLayout;
		private IDisposable titleInputErrorSubscription;

		private INotificationService notifications = Mvx.Resolve<INotificationService> ();

        protected override int LayoutResource => Resource.Layout.NotificationsView;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SupportActionBar.SetDisplayHomeAsUpEnabled (false);

			this.titleEditText = this.FindViewById<EditText> (Resource.Id.titleEditText);
			this.timeEditText = this.FindViewById<EditText> (Resource.Id.timeEditText);
			this.messageEditText = this.FindViewById<EditText> (Resource.Id.messageEditText);
			this.addNotificationButton = this.FindViewById<Button> (Resource.Id.addNotificationButton);

			this.titleInputLayout = this.FindViewById<TextInputLayout> (Resource.Id.titleEditLayout);
			this.timeInputLayout = this.FindViewById<TextInputLayout> (Resource.Id.timeinputLayout);
			this.messageInputLayout = this.FindViewById<TextInputLayout> (Resource.Id.messageEditLayout);

			this.SetBindings ();
		}

		private void SetBindings ()
		{
            var bindingSet = this.CreateBindingSet<NotificationsView, NotificationsViewModel> ();
			bindingSet
				.Bind (this.titleEditText)
				.To (vm => vm.Title);

			bindingSet
				.Bind (this.timeEditText)
				.To (vm => vm.Time);

			bindingSet
				.Bind (this.messageEditText)
				.To (vm => vm.Message);

			bindingSet
				.Bind (this.addNotificationButton)
				.To (vm => vm.AddNotification);

			bindingSet.Apply ();

			this.WhenAnyValue (t => t.ViewModel.Time)
				.Subscribe (time => {
					this.RunOnUiThread (() => {
						this.timeInputLayout.Error = time <= 0 ? "Wpisz coś." : "";
					});
				}).DisposeWith (this.Disposables);

			this.WhenAnyValue (t => t.ViewModel.Message)
				.Subscribe (message => {
					this.RunOnUiThread (() => {
						this.messageInputLayout.Error = string.IsNullOrEmpty (message) ? "Treść notyfikacji nie może być pusty. Wpisz coś." : "";
					});
				}).DisposeWith (this.Disposables);
		}

		protected override void OnResume ()
		{
			base.OnResume ();

			this.titleInputErrorSubscription = this.WhenAnyValue (t => t.ViewModel.Title)
				.Subscribe (title => {
					this.RunOnUiThread (() => {
						this.titleInputLayout.Error = string.IsNullOrEmpty (title) ? "Tytuł notyfikacji nie może być pusty. Wpisz coś." : "";
					});
				});
		}

		protected override void OnPause ()
		{
			base.OnPause ();

			this.titleInputErrorSubscription?.Dispose ();
		}
	}
}
