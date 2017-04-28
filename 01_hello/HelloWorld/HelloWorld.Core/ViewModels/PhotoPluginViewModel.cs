using System;
using MvvmCross.Plugins.PictureChooser;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using ReactiveUI;
using System.Threading.Tasks;
using System.Reactive;
using System.Reactive.Linq;
using System.IO;

namespace HelloWorld.Core.ViewModels
{
	public class PhotoPluginViewModel : MvxViewModel
	{
		IMvxPictureChooserTask PictureChooser = Mvx.Resolve<IMvxPictureChooserTask>();
		public ReactiveCommand<Unit, Stream> TakePhotoCommand { get; set; }

		private byte[] _bytes;
		public byte[] Bytes
		{
			get { return _bytes; }
			set { _bytes = value; RaisePropertyChanged(() => Bytes); }
		}

		private void OnPicture(Stream pictureStream)
		{
			var memoryStream = new MemoryStream();
			pictureStream.CopyTo(memoryStream);
			Bytes = memoryStream.ToArray();
		}

		public PhotoPluginViewModel()
		{
			this.TakePhotoCommand = ReactiveCommand.CreateFromTask(() => PictureChooser.TakePicture(400, 75));
			this.TakePhotoCommand.Subscribe(x=>{
				this.OnPicture(x);
			});
		}
	}
}
