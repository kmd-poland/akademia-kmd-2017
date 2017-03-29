using Android.App;
using Android.OS;
using MvvmCross.Binding.BindingContext;
using HelloWorld.Core.ViewModels;
using Android.Widget;

namespace HelloWorld.Droid.Views
{
    [Activity (Label = "KMD AKADEMIA")]
    public class FirstView : BaseView
    {
        protected override int LayoutResource => Resource.Layout.FirstView;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);



        }
    }
}
