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

			var dzielna = FindViewById<EditText>(Resource.Id.dzielna);
			var dzielnik = FindViewById<EditText>(Resource.Id.dzielnik);
			var iloraz = FindViewById<TextView>(Resource.Id.iloraz);

			var bSet = this.CreateBindingSet<FirstView, FirstViewModel>();

			bSet.Bind(dzielna)
				.To(vm => vm.Dzielna);

			bSet.Bind(dzielnik)
			    .To(vm => vm.Dzielnik);

			bSet.Bind(iloraz)
				.To(vm => vm.Iloraz);
			
			bSet.Apply();

        }
    }
}
