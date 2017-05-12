using System;
using Android.Views.Accessibility;
using HelloWorld.Core.ViewModels;
using Android.App;
using Android.Views;
using System.Reactive;
using MvvmCross.Binding.Droid.Views;
using Android.OS;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;
using Android.Content;
using Android.Widget;

namespace HelloWorld.Droid.Views
{
	public class ListAdapter : MvxAdapter
	{ 
		public ListAdapter(Context context, IMvxAndroidBindingContext bindingContext)
			: base(context, bindingContext)
		{
		}

		protected override View GetBindableView(View convertView, object dataContext, int templateId)
		{
			var view = base.GetBindableView(convertView, dataContext, templateId) as MvxListItemView;

			var item = dataContext as ListElement;
			if (item != null)
			{
				var textView = view.FindViewById<TextView>(Resource.Id.text);
				textView.Text = item.Value;

				var button = view.FindViewById<Button>(Resource.Id.button);
				var button2 = view.FindViewById<Button>(Resource.Id.button2);

				var bindingSet = view.CreateBindingSet<MvxListItemView, ListElement>();
				bindingSet.Bind(button).To(el => el.SetTitle);
				bindingSet.Bind(button2).To(el => el.SetTitleByMessage);
				bindingSet.Apply();
			}
			return view;
		}
	}

    [Activity]
    public class ListView : BaseView<ListViewModel>
    {
        private const int EDIT_BTN_ID = 100;

        protected override int LayoutResource => Resource.Layout.ListView;


        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            var editItem = menu.Add(0, EDIT_BTN_ID, 0, "Edit");
            editItem.SetShowAsAction(ShowAsAction.Always);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == EDIT_BTN_ID)
            {
                this.ViewModel.GoToEdit.Execute(Unit.Default).Subscribe();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

			var title = this.FindViewById<TextView>(Resource.Id.title);

            var listView = FindViewById<MvxListView>(Resource.Id.listView);
			listView.ItemTemplateId = Resource.Layout.item_list;
			listView.Adapter = new ListAdapter(this, (IMvxAndroidBindingContext)this.BindingContext);
            
			var bindingSet = this.CreateBindingSet<ListView, ListViewModel>();

			bindingSet.Bind(title).To(x => x.Title).For(x => x.Text);
            bindingSet.Bind(listView).To(x => x.Elements).For(x=>x.ItemsSource);

            bindingSet.Apply();

        }
    }
}
