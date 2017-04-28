using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using HelloWorld.Core.ViewModels;

namespace HelloWorld.iOS.Views
{
    [MvxFromStoryboard]
    public partial class FirstView : MvxViewController
    {
        public FirstView (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            var set = this.CreateBindingSet<FirstView, FirstViewModel> ();
            set.Bind (Label).To (vm => vm.Title);
            set.Bind (TextField).To (vm => vm.Time);
            set.Apply ();
        }
    }
}
