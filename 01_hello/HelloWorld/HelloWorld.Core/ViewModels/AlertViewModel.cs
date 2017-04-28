using System;
using System.Net.Http;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using ReactiveUI;
using ViewModels;
using Acr.UserDialogs;

namespace HelloWorld.Core.ViewModels
{
	public class AlertViewModel : MvxViewModel
	{
        private double radius;
		public double Radius
		{
			get { return radius; }
			set { this.SetProperty(ref this.radius, value);	}
		}

		private double height;
		public double Height
		{
			get { return height; }
			set { this.SetProperty(ref this.height, value);	}
		}

        private double volume;
        public double Volume
        {
            get { return volume; }
            private set { this.SetProperty(ref this.volume, value); }
        }



        public void CalculateVolume()
        {
            if (Radius <= 0 || Height <= 0)
                UserDialogs.Instance.ShowError("Radius and height must be greater than 0!");
            else
                Volume = 1.0 / 3.0 * Math.PI * Radius * Radius * Height;
        }


		public AlertViewModel()
		{
            
          

		}
	}
}
