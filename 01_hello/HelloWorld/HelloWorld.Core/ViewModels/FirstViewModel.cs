using System;
using System.Net.Http;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using ReactiveUI;

namespace HelloWorld.Core.ViewModels
{
	public class FirstViewModel : MvxViewModel
	{
		private float dzielna;
		public float Dzielna
		{
			get { return dzielna; }
			set { 
				this.SetProperty(ref this.dzielna, value); 
                this.RaisePropertyChanged(nameof(Iloraz));
			}
		}

		private float dzielnik;
		public float Dzielnik
		{
			get { return dzielnik; }
			set { 
				this.SetProperty(ref this.dzielnik, value); 
				this.RaisePropertyChanged(nameof(Iloraz));
			}
		}

		public float Iloraz => Dzielna / Dzielnik;

		private string downloadedString;
		public string DownloadedString
		{
			get { return downloadedString; }
			set {
				this.SetProperty(ref this.downloadedString, value);
			}
		}



		static HttpClient Client = new HttpClient();

		public ReactiveCommand<string, string> DownloadStringCommand { get; set; }

		private async Task<string> DownloadString(string url)
		{
			var response = await Client.GetAsync(url);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadAsStringAsync();
				return result;
			}

			return "ERROR!";
		}

		public FirstViewModel()
		{
            this.DownloadStringCommand = ReactiveCommand.CreateFromTask<string, string>(async url => await DownloadString(url));
			this.DownloadStringCommand.Subscribe(x=>{
				DownloadedString = x;
			});
		}
	}
}
