using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using HelloWorld.Core.ViewModels;
using UIKit;
using CoreGraphics;

namespace HelloWorld.iOS.Views
{
    //[MvxFromStoryboard]
    public partial class FirstView : MvxTableViewController<FirstViewModel>
    {
        private FirstViewTableSource source;
        private UIBarButtonItem buttonAdd;

        public FirstView()
        {
            
        }

        public FirstView (IntPtr handle) : base (handle)
        {
            
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            this.EdgesForExtendedLayout = UIRectEdge.None;

            this.TableView.TableHeaderView = new UIView (new CGRect { Height = 0, Width = 0, X = 0, Y = 0 });

            this.buttonAdd = new UIBarButtonItem (UIBarButtonSystemItem.Add);
            this.NavigationItem.RightBarButtonItem = this.buttonAdd;

            this.source = new FirstViewTableSource (this.TableView);
            this.TableView.Source = this.source;
            this.TableView.EstimatedRowHeight = 45;
            this.TableView.RowHeight = UITableView.AutomaticDimension;

            var set = this.CreateBindingSet<FirstView, FirstViewModel> ();

            set.Bind (buttonAdd)
                .To (vm => vm.NavigateCommand);

            set.Bind (source)
                .For (x => x.ItemsSource)
                .To (vm => vm.ListItems);
			
            set.Apply ();
        }
    }
}
