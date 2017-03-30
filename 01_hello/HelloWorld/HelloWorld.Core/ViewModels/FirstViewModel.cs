using MvvmCross.Core.ViewModels;

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
	}
}
