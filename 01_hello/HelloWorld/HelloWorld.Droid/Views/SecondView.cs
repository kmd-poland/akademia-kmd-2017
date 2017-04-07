
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
using HelloWorld.Droid;
using HelloWorld.Droid.Views;
using MvvmCross.Binding.BindingContext;
using ViewModels;

namespace Views
{
	[Activity(Label = "SecondView")]
	public class SecondView  : BaseView
	{
		protected override int LayoutResource => Resource.Layout.SecondView;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			var buttonAdd = FindViewById<Button>(Resource.Id.button_new_item);
			var inputName = FindViewById<EditText>(Resource.Id.edit_1);
			var inputSurname = FindViewById<EditText>(Resource.Id.edit_2);

			var bSet = this.CreateBindingSet<SecondView, SecondViewModel>();

			bSet.Bind(buttonAdd)
				.To(vm => vm.NavigateAndPassDataCommand);

			bSet.Bind(inputName)
			    .To(vm => vm.Name);

			bSet.Bind(inputSurname)
			    .To(vm => vm.Surname);

			bSet.Apply();
		}
	}
}
