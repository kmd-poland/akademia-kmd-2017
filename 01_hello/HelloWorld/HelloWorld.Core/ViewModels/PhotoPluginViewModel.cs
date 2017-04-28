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
		//public ReactiveCommand<Unit, Stream> TakePhotoCommand;

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
			//this.TakePhotoCommand = ReactiveCommand.CreateFromTask(async () => await TakePicture(), Observable.Return(true));
			//this.TakePhotoCommand.CanExecute.Subscribe(x=>{
			//    x.GetType();
			//});
		}

		public void TakePictureSynch()
		{
			PictureChooser.TakePicture(400, 95, OnPicture, () => { });
		}
	}
}
