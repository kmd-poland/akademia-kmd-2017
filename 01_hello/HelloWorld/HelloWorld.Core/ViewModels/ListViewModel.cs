using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using HelloWorld.Core.Domain;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Linq;

namespace HelloWorld.Core.ViewModels
{
	public class ListElement
	{
		public string Value {get;set;}
		public ReactiveCommand SetTitleByMessage { get; }
		public ReactiveCommand<Unit, Unit> SetTitle { get; }

		public ListElement()
		{
			this.SetTitle = ReactiveCommand.Create(() => { });
			this.SetTitleByMessage = ReactiveCommand.Create(() => Mvx.Resolve<IMvxMessenger>().Publish(new SetTitleMessage(this, this.Value)));
		}
	}

    public class ListViewModel : MvxViewModel
    {
        //token jest potrzebny, aby trzymac subskrypcje w klasie. Inaczej garbage collector usunie zmienną lokalną i nigdy nie otrzymamy wiadomości
		MvxSubscriptionToken dataChangedToken;
        MvxSubscriptionToken setTitleToken;

		private string title;
		public string Title
		{
			get
			{
				return this.title;
			}
			set {
				this.SetProperty(ref this.title, value);
			}
		}

		public ReactiveList<ListElement> Elements { get; }
        public ReactiveCommand<Unit, bool> GoToEdit { get; }

        public ListViewModel()
        {
			Title = "TITLE";
			Elements = new ReactiveList<ListElement>();

            //sluchamy wiadomosci DataChangedMessage i dodajemy to listy Elements jej zawartosc
            //zwykle nie ma potrzeby przekazywania zawrtosci w wiadomosci - samo polecnie moze po prostu odswiezac listę z bazy danych
            dataChangedToken = Mvx.Resolve<IMvxMessenger>().Subscribe<DataChangedMessage>(message =>
			                                                                              Elements.Add(new ListElement() { Value = message.Content }));

			setTitleToken = Mvx.Resolve<IMvxMessenger>().Subscribe<SetTitleMessage>(message => this.Title = message.Title);

            GoToEdit = ReactiveCommand.Create(() => this.ShowViewModel<EditViewModel>());

			this.Elements.Changed
				.Select(x => this.whenAnyButtonClicked())
				.Switch()
			    .Subscribe(item => this.Title = item.Value);
        }

		private IObservable<ListElement> whenAnyButtonClicked()
		{
			return this.Elements.Select(item => item.SetTitle.Select(_ => item)).Merge();
		}
    }
}
