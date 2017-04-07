using System;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using HelloWorld.Core.ViewModels;
using UIKit;
using Cirrious.FluentLayouts.Touch;

namespace HelloWorld.iOS.Views
{
    public class FirstViewCell : MvxTableViewCell
    {
        private UILabel NameTextView, SurnameTextView;

        public string Name {
            get { return this.NameTextView.Text; }
            set { this.NameTextView.Text = value; }
        }

        public string Surname {
            get { return this.SurnameTextView.Text; }
            set { this.SurnameTextView.Text = value; }
        }

        public FirstViewCell (IntPtr handle) : base(handle)
        {
            this.NameTextView = new UILabel { TranslatesAutoresizingMaskIntoConstraints = false };
            this.SurnameTextView = new UILabel { TranslatesAutoresizingMaskIntoConstraints = false };

            this.ContentView.AddSubviews (this.NameTextView, this.SurnameTextView);

            this.SetLayout ();
        }

        private void SetLayout()
        {
            this.ContentView.AddConstraints (
                this.NameTextView.AtLeftOf (this.ContentView, 5),
                this.NameTextView.AtTopOf (this.ContentView, 5),
                this.NameTextView.AtRightOf (this.ContentView, 5),
                this.SurnameTextView.AtLeftOf (this.ContentView, 5),
                this.SurnameTextView.Below (this.NameTextView, 5),
                this.SurnameTextView.AtRightOf (this.ContentView, 5),
                this.SurnameTextView.AtBottomOf(this.ContentView, 5),
                this.ContentView.Height ().GreaterThanOrEqualTo (40)
            );
        }
    }
}
