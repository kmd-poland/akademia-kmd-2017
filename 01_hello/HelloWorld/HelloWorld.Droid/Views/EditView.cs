
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HelloWorld.Core.ViewModels;
using MvvmCross.Binding.BindingContext;

namespace HelloWorld.Droid.Views
{
    [Activity(Label = "EditView")]
    public class EditView : BaseView<EditViewModel>
    {
        protected override int LayoutResource => Resource.Layout.EditView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var value = FindViewById<EditText>(Resource.Id.value);
            var save = FindViewById<Button>(Resource.Id.button_save);

            var bindingSet = this.CreateBindingSet<EditView, EditViewModel>();

            bindingSet.Bind(value).To(x => x.Value);
            bindingSet.Bind(save).To(x => x.Save);
            bindingSet.Apply();

        }
    }
}
