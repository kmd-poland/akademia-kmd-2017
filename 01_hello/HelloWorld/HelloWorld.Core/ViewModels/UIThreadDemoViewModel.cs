using System;
using MvvmCross.Core.ViewModels;
using System.Reactive;
using ReactiveUI;
using Acr.UserDialogs;
using System.Threading.Tasks;

namespace HelloWorld.Core.ViewModels
{
    public class UIThreadDemoViewModel : MvxViewModel
    {

        public ReactiveCommand<Unit, Unit> BlockingLongOp { get; }
        public ReactiveCommand<Unit, Unit> BackgroundLongOp { get; }


        public UIThreadDemoViewModel()
        {
            BlockingLongOp = ReactiveCommand.Create(() =>
            {
                LongRunningTask();
                UserDialogs.Instance.Alert("DONE!");

            });

            BackgroundLongOp = ReactiveCommand.CreateFromTask(async () =>
           {
               await Task.Run(() => LongRunningTask());
           	   UserDialogs.Instance.Alert("DONE!");
           });
        }




        private void LongRunningTask()
        {
            for (int i = 0; i < 100000; i++)
                for (int j = 0; j < 300; j++)
                {
                    var dummyOp = Math.Log10(i) * Math.Log10(j);
                }
        }

    }
}
