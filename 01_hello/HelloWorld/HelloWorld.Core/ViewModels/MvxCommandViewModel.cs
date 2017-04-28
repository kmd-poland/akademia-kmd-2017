using System;
using MvvmCross.Core.ViewModels;

namespace HelloWorld.Core.ViewModels
{
    public class MvxCommandViewModel : MvxViewModel
    {
        private double radius;
        public double Radius
        {
            get { return radius; }
            set { 
                this.SetProperty(ref this.radius, value);
                CalculateVolume.RaiseCanExecuteChanged();
            }
        }

        private double height;
        public double Height
        {
            get { return height; }
            set {
                this.SetProperty(ref this.height, value);
                CalculateVolume.RaiseCanExecuteChanged();
            }
        }

        private double volume;
        public double Volume
        {
            get { return volume; }
            private set { this.SetProperty(ref this.volume, value); }
        }



        public MvxCommand CalculateVolume { get; }


        public MvxCommandViewModel()
        {
            this.CalculateVolume = new MvxCommand(
                () => Volume = 1.0 / 3.0 * Math.PI * Radius * Radius * Height,
                () => this.Radius > 0  && this.Height > 0);

        }
    }
}
