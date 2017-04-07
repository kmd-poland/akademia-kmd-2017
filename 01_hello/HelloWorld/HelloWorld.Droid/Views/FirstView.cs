using Android.App;
using Android.OS;
using MvvmCross.Binding.BindingContext;
using HelloWorld.Core.ViewModels;
using Android.Widget;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Binding.Droid.BindingContext;
using Views;

namespace HelloWorld.Droid.Views
{
    [Activity (Label = "KMD AKADEMIA")]
    public class FirstView : BaseView
    {
        protected override int LayoutResource => Resource.Layout.FirstView;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

			var buttonAdd = FindViewById<Button>(Resource.Id.button_new_item);
			var list = FindViewById<MvxListView>(Resource.Id.list);

			list.Adapter = new ListAdapter(this, (IMvxAndroidBindingContext)this.BindingContext);
			list.ItemTemplateId = Resource.Layout.list_item;

			var bSet = this.CreateBindingSet<FirstView, FirstViewModel>();

			bSet.Bind(buttonAdd)
				.To(vm => vm.NavigateCommand);

			bSet.Bind(list)
			    .For(x => x.ItemsSource)
                .To(vm => vm.ListItems);

			bSet.Apply();
        }
    }
}
