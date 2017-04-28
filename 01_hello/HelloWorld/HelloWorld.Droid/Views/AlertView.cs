using Android.App;
using Android.OS;
using MvvmCross.Binding.BindingContext;
using HelloWorld.Core.ViewModels;
using Android.Widget;
using MvvmCross.Platform.Converters;
using System;

namespace HelloWorld.Droid.Views
{
    [Activity (Label = "KMD AKADEMIA")]
    public class AlertView : BaseView<AlertViewModel>
    {
        protected override int LayoutResource => Resource.Layout.FirstView;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            var radius = FindViewById<EditText>(Resource.Id.radius);
            var height = FindViewById<EditText>(Resource.Id.height);
            var volume = FindViewById<TextView>(Resource.Id.volume);

            var button = FindViewById<Button>(Resource.Id.button_calculate);
	
			var bSet = this.CreateBindingSet<AlertView, AlertViewModel>();

            bSet.Bind(radius)
                .To(vm => vm.Radius);


            bSet.Bind(height)
                .To(vm => vm.Height);
			

            bSet.Bind(volume)
                .To(vm => vm.Volume);

            button.Click += (sender, e) => ViewModel.CalculateVolume();
       	
			bSet.Apply();
        }
    }


	
}
