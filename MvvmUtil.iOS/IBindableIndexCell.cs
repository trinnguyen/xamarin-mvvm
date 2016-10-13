using System;
using UIKit;

namespace MvvmUtil.iOS
{
	public interface IBindableIndexCell<T>
	{
		void Bind(T cellViewModel, UITableView tableView, Foundation.NSIndexPath indexPath);
	}
}