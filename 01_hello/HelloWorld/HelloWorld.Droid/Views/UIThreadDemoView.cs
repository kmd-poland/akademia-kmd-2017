using System;
using HelloWorld.Core.ViewModels;
using MvvmCross.Droid.Views;
using Android.App;
using Android.OS;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using Android.Views.Animations;

namespace HelloWorld.Droid.Views
{
    [Activity]
    public class UIThreadDemoView : BaseView<UIThreadDemoViewModel>
    {

        protected override int LayoutResource => Resource.Layout.UIThreadDemoView;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

            var blockingBtn = FindViewById<Button>(Resource.Id.button_blocking);
            var backgrondBtn = FindViewById<Button>(Resource.Id.button_background);
			

            var bSet = this.CreateBindingSet<UIThreadDemoView, UIThreadDemoViewModel>();

            bSet.Bind(blockingBtn)
                .To(vm => vm.BlockingLongOp);


            bSet.Bind(backgrondBtn)
                .To(vm => vm.BackgroundLongOp);

			
			bSet.Apply();
            RotateAnimation anim = new RotateAnimation(0, 360);
            anim.RepeatMode = RepeatMode.Restart;
            anim.RepeatCount = -1;
            anim.Duration = 5000;

            var fancyView = FindViewById(Resource.Id.fancyView);
            fancyView.StartAnimation(anim);
      
		}
    }
}
