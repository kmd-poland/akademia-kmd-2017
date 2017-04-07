using System;
using Foundation;
using HelloWorld.Core.ViewModels;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace HelloWorld.iOS.Views
{
    public class FirstViewTableSource : MvxTableViewSource
    {
        private readonly string cellId = "firstViewCell";

        public FirstViewTableSource (UITableView tableView) : base(tableView)
        {
            tableView.RegisterClassForCellReuse (typeof (FirstViewCell), cellId);
        }

        public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
        {
            return UITableView.AutomaticDimension;
        }

        protected override UITableViewCell GetOrCreateCellFor (UITableView tableView, NSIndexPath indexPath, object item)
        {
            var cell = tableView.DequeueReusableCell (this.cellId, indexPath) as FirstViewCell;

            var listItem = item as ListItem;

            if (cell != null && listItem != null) {
                cell.Name = listItem.Name;
                cell.Surname = listItem.Surname;
            }

            return cell;
        }
    }
}
