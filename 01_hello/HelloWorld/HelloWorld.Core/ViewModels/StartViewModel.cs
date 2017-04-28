using System;
using System.Reactive;
using MvvmCross.Core.ViewModels;
using ReactiveUI;
namespace HelloWorld.Core.ViewModels
{
    public class StartViewModel : MvxViewModel
    {

        public ReactiveCommand<Unit, bool> GoToAlertView { get; }
        public ReactiveCommand<Unit, bool> GoToMvxCanExecuteView { get; }
        public ReactiveCommand<Unit, bool> GoToRxUICanExecuteView { get; }
        public ReactiveCommand<Unit, bool> GoToPhotoPlugin { get; }

        public StartViewModel()
        {
            GoToAlertView = ReactiveCommand.Create(() => this.ShowViewModel<AlertViewModel>());
            GoToMvxCanExecuteView = ReactiveCommand.Create(() => this.ShowViewModel<MvxCommandViewModel>());
            GoToRxUICanExecuteView = ReactiveCommand.Create(() => this.ShowViewModel<RxUICommanViewModel>());
            GoToPhotoPlugin = ReactiveCommand.Create(() => this.ShowViewModel<PhotoPluginViewModel>());
        }
    }
}
