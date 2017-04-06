
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

namespace Views
{
	[Activity(Label = "DRUGI WIDOK")]
	public class SecondView : BaseView
	{
		protected override int LayoutResource => Resource.Layout.SecondView;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

		}
	}
}
