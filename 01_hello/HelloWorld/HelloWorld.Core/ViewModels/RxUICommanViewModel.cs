using System;
using System.Reactive;
using MvvmCross.Core.ViewModels;
using ReactiveUI;
using System.Diagnostics;
namespace HelloWorld.Core.ViewModels
{
    public class RxUICommanViewModel : MvxViewModel
    {
        private double radius;
        public double Radius
        {
            get { return radius; }
            set { this.SetProperty(ref this.radius, value); }
        }

        private double height;
        public double Height
        {
            get { return height; }
            set { this.SetProperty(ref this.height, value); }
        }

        private double volume;
        public double Volume
        {
            get { return volume; }
            private set { this.SetProperty(ref this.volume, value); }
        }



        public ReactiveCommand<Unit, double> CalculateVolume { get; }


        public RxUICommanViewModel()
        {

            //WhenAny pozwala nam obserwowac zmiany dowolnej ilosci wlasciwosci
            //w przykladzie ponizej - przy kazdej zmianie Height lub Radius zostanie policzony warunek (ostatni parametr)
            //dzieki takiej definicji cala logike mamy w jednym miejscu (settery nie maja zadnej dodatkowej logiki)
            //Wazna Uwaga: WhenAny / WhenAnyValue policzy sie w momencie gdy ktos go nasluchuje (np. jest warunkiem komendy) i zrobi to natychmiast,
            //             tazke dla inicjalnych wartosci (a wiec nie po pierwszej zmianie!) 
            var canCalculateVolume = this.WhenAny(vm => vm.Height, 
                                                  vm => vm.Radius,
                                                  (h, r) => h.Value > 0 && r.Value > 0);

            //komende definiujemy podajac jej nasza obserwowana zmiane jako warunek canexecute
            //sama komenda zadba o to, aby informowac o CanExecuteChanged - bez zadnej rozproszonej logiki
            //dodatkowo uzywamy tu komendy parametryzowanej - zwroci ona nam wynik (policzona objectosc stozka), ale nie przypisze do ViewModelu
            //w ten sposob na wyniku komendy moga dzialac takze inni
            this.CalculateVolume = ReactiveCommand.Create(
                     () => 1.0 / 3.0 * Math.PI * Radius * Radius * Height,
                      canCalculateVolume);

            //Subscribe na komendzie to nasluchiwanie jej wyniku. W tym przypadku policzona objetosc przypisujemy do wlasciwosci Volume
            this.CalculateVolume.Subscribe(calculatedVolume => Volume = calculatedVolume);

            //Podejscie alternatywne - Subscribe na WhenAny
            //do Subscribe trafi kazdy wynik z WhenAny - w tym przypadku objetosc liczona po zmianie wysokosci lub promienia
            //w samym subscribe mozemy zamiescic dowolna logike, w szczegolnosci przypisanie do wlasciwosci (gdybysmy chieli liczyc od razu, bez komendy)
            this.WhenAny(vm => vm.Height, vm => vm.Radius,
                         (h, r) => 1.0 / 3.0 * Math.PI * Radius * Radius * Height)
                .Subscribe(calculatedVolume =>
            {
                Debug.WriteLine($"Calculated volume is {calculatedVolume}");
            });
                              
        }
    }
}
