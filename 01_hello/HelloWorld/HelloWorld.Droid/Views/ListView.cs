using System;
using Android.Views.Accessibility;
using HelloWorld.Core.ViewModels;
using Android.App;
using Android.Views;
using System.Reactive;
using MvvmCross.Binding.Droid.Views;
using Android.OS;
using MvvmCross.Binding.BindingContext;

namespace HelloWorld.Droid.Views
{
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

            var listView = FindViewById<MvxListView>(Resource.Id.listView);

            var bindingSet = this.CreateBindingSet<ListView, ListViewModel>();

            bindingSet.Bind(listView).To(x => x.Elements).For(x=>x.ItemsSource);

            bindingSet.Apply();

        }
    }
}
