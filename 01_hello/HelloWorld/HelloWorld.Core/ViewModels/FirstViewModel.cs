using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using ReactiveUI;
using ViewModels;

namespace HelloWorld.Core.ViewModels
{
    public class FirstViewModel : MvxViewModel
    {
		private List<ListItem> listitems;
		public List<ListItem> ListItems
		{
			get { return this.listitems; }
			set { this.SetProperty(ref this.listitems, value); }
		}

		public ReactiveCommand NavigateCommand { get; set; }

		public FirstViewModel()
		{
			this.NavigateCommand = ReactiveCommand.Create(() => this.ShowViewModel<SecondViewModel>());
		}

		public void Init(string name, string surname)
		{
			// view model was started with parameter
			if (name != null && surname != null)
			{
				ListItems = new List<ListItem>();
				ListItems.Add(new ListItem(name, surname));
			}
		}
    }

	public class ListItem
	{
		public ListItem(string Name, string Surname)
		{
			this.Name = Name;
			this.Surname = Surname;
		}

		public string Name { get; set; }
		public string Surname { get; set; }
	}
}
