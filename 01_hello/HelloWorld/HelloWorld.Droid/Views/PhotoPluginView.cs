
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
using MvvmCross.Binding.BindingContext;
using HelloWorld.Core.ViewModels;
using MvvmCross.Platform.Converters;
using Android.Graphics;
using System.Globalization;
using System.IO;
using Converters;

namespace HelloWorld.Droid.Views
{
    [Activity]
    public class PhotoPluginView : BaseView<PhotoPluginViewModel>
    {
        protected override int LayoutResource => Resource.Layout.PhotoPlugin;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			var takePhoto = FindViewById<Button>(Resource.Id.take_photo);
			var photo = FindViewById<ImageView>(Resource.Id.photo);

			var bSet = this.CreateBindingSet<PhotoPluginView, PhotoPluginViewModel>();
			//bSet.Bind(takePhoto)
			//	.To(vm => vm.TakePhotoCommand);

			bSet.Bind(photo)
				.To(vm => vm.Bytes);
				//.WithConversion(new InMemoryImageConverter());

			bSet.Apply();

			takePhoto.Click += (sender, e) =>
			{
				this.ViewModel.TakePictureSynch();
			};
		}
    }



}
