using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;
using HelloWorld.Core.Domain;
using ReactiveUI;
using System.Reactive;
namespace HelloWorld.Core.ViewModels
{
    public class ListViewModel : MvxViewModel
    {
        //token jest potrzebny, aby trzymac subskrypcje w klasie. Inaczej garbage collector usunie zmienną lokalną i nigdy nie otrzymamy wiadomości
        MvxSubscriptionToken dataChangedToken;

        public ReactiveList<string> Elements { get; }
        public ReactiveCommand<Unit, bool> GoToEdit { get; }

        public ListViewModel()
        {
            Elements = new ReactiveList<string>();

            //sluchamy wiadomosci DataChangedMessage i dodajemy to listy Elements jej zawartosc
            //zwykle nie ma potrzeby przekazywania zawrtosci w wiadomosci - samo polecnie moze po prostu odswiezac listę z bazy danych
            dataChangedToken = Mvx.Resolve<IMvxMessenger>().Subscribe<DataChangedMessage>(message =>
                                                                                          Elements.Add(message.Content));

            GoToEdit = ReactiveCommand.Create(() => this.ShowViewModel<EditViewModel>());
        }
    }
}
