using System;
using Android.OS;
using Android.Widget;
using HelloWorld.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using Android.App;

namespace HelloWorld.Droid.Views
{
    [Activity]
    public class MvxCommandView : BaseView<MvxCommandViewModel>
    {
        protected override int LayoutResource => Resource.Layout.FirstView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var radius = FindViewById<EditText>(Resource.Id.radius);
            var height = FindViewById<EditText>(Resource.Id.height);
            var volume = FindViewById<TextView>(Resource.Id.volume);

            var button = FindViewById<Button>(Resource.Id.button_calculate);

            var bSet = this.CreateBindingSet<MvxCommandView, MvxCommandViewModel>();

            bSet.Bind(radius)
                .To(vm => vm.Radius);


            bSet.Bind(height)
                .To(vm => vm.Height);


            bSet.Bind(volume)
                .To(vm => vm.Volume);

            bSet.Bind(button)
                .To(vm => vm.CalculateVolume);



            bSet.Apply();
        }
    }
}
