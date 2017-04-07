using System;
using MvvmCross.Core.ViewModels;
using ReactiveUI;
using HelloWorld.Core.ViewModels;

namespace ViewModels
{
	public class SecondViewModel : MvxViewModel
	{
		private string name;
		public string Name
		{
			get { return name; }
			set
			{
				this.SetProperty(ref this.name, value);
			}
		}

		private string surname;
		public string Surname
		{
			get { return surname; }
			set
			{
				this.SetProperty(ref this.surname, value);
			}
		}

		public ReactiveCommand NavigateAndPassDataCommand { get; set; }

		public SecondViewModel()
		{
			this.NavigateAndPassDataCommand = ReactiveCommand.Create(() =>
				this.ShowViewModel<FirstViewModel>(
                     new { name = this.Name, surname = this.Surname }
				)
			);
		}
	}
}
