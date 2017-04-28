using System;
using System.Reactive;
using MvvmCross.Core.ViewModels;
using ReactiveUI;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Platform;
using HelloWorld.Core.Domain;

namespace HelloWorld.Core.ViewModels
{
    public class EditViewModel : MvxViewModel
    {
        string value;

        public string Value
        {
            get { return this.value; }
            set { this.SetProperty(ref this.value, value); }
        }

        public ReactiveCommand<Unit, Unit> Save { get; }
        public EditViewModel()
        {

            var canSave = this.WhenAny(x => x.Value, val => !string.IsNullOrEmpty(val.Value));
            //na Save wysylamy DataChangedMessage z zawartoscia, ListViewModel slucha wiadomosci i dodaje element do listy
            Save = ReactiveCommand.Create(() =>
            {
                Mvx.Resolve<IMvxMessenger>().Publish(new DataChangedMessage(this, Value));
                this.Close(this);
            }, canSave);
        }
    }
}
