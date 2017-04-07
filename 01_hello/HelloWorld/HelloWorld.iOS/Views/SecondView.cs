using System;
using MvvmCross.iOS.Views;
using ViewModels;
using UIKit;
using Cirrious.FluentLayouts.Touch;
using MvvmCross.Binding.BindingContext;
namespace HelloWorld.iOS.Views
{
    public class SecondView : MvxViewController<SecondViewModel>
    {
        private UITextField name, surname;
        private UIBarButtonItem saveButton;

        public SecondView ()
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            this.EdgesForExtendedLayout = UIRectEdge.None;
            this.View.BackgroundColor = UIColor.White;

            this.saveButton = new UIBarButtonItem (UIBarButtonSystemItem.Save);
            this.NavigationItem.RightBarButtonItem = saveButton;

            this.name = new UITextField { TranslatesAutoresizingMaskIntoConstraints = false, Placeholder = "Name" };
            this.surname = new UITextField { TranslatesAutoresizingMaskIntoConstraints = false, Placeholder = "Surname" };

            this.View.AddSubviews (this.name, this.surname);

            this.View.AddConstraints (
                this.name.AtLeftOf (this.View, 5),
                this.name.Height().EqualTo(40),
                this.name.AtTopOf (this.View, 5),
                this.name.AtRightOf (this.View, 5),
                this.surname.AtLeftOf (this.View, 5),
                this.surname.Height ().EqualTo (40),
                this.surname.AtRightOf (this.View, 5),
                this.surname.Below (this.name, 5)
                //,
                //this.surname.AtBottomOf (this.View, 5)
            );

            var set = this.CreateBindingSet<SecondView, SecondViewModel> ();

            set.Bind (saveButton).To (vm => vm.NavigateAndPassDataCommand);
            set.Bind (this.name).To (vm => vm.Name);
            set.Bind (this.surname).To (vm => vm.Surname);

            set.Apply ();
        }
    }
}
