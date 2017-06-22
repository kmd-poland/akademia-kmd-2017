using System;
using Android.OS;
using HelloWorld.Core.ViewModels;
using Android.Widget;
using MvvmCross.Binding.BindingContext;
using Android.Views.Accessibility;
using Android.App;
namespace HelloWorld.Droid.Views
{
    [Activity]
    public class ReactiveSearchView : BaseView<ReactiveSearchViewModel>
    {

        protected override int LayoutResource => Resource.Layout.ReactiveSearchView;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

            var query =  FindViewById<EditText>(Resource.Id.query);
            var result = FindViewById<TextView>(Resource.Id.result);

            var bSet = this.CreateBindingSet<ReactiveSearchView, ReactiveSearchViewModel>();
            bSet.Bind(query)
                .To(vm => vm.SearchQuery);

            bSet.Bind(result)
                .To(vm => vm.SearchResult);
			

			bSet.Apply();
		}
    }
}
