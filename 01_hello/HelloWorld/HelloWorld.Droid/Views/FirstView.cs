using Android.App;
using Android.OS;
using MvvmCross.Binding.BindingContext;
using HelloWorld.Core.ViewModels;
using Android.Widget;
using MvvmCross.Platform.Converters;
using System;

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

			var button = FindViewById<Button>(Resource.Id.button_download);
			var label = FindViewById<TextView>(Resource.Id.downloaded_string);

			var buttonNavigate = FindViewById<Button>(Resource.Id.button_navigate);

			var bSet = this.CreateBindingSet<FirstView, FirstViewModel>();

			bSet.Bind(dzielna)
				.To(vm => vm.Dzielna)
				.WithConversion(new FloatToStringConverter());

			bSet.Bind(dzielnik)
				.To(vm => vm.Dzielnik)
				.WithConversion(new FloatToStringConverter());

			bSet.Bind(iloraz)
				.To(vm => vm.Iloraz);

			bSet.Bind(button)
				.To(vm => vm.DownloadStringCommand)
				.CommandParameter("https://pastebin.com/raw/1f5KebFg");

			bSet.Bind(label)
				.To(vm => vm.DownloadedString);

			bSet.Bind(buttonNavigate)
			    .To(vm => vm.NavigateCommand);

			bSet.Apply();
        }
    }

	public class FloatToStringConverter : MvxValueConverter<float, string>
	{
		protected override string Convert(float value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value.ToString();
		}

		protected override float ConvertBack(string value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			float result = 0;
			float.TryParse(value, out result);
			     
			return result;
		}
	}
}
