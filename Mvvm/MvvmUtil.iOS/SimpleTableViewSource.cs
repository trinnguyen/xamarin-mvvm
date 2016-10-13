using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Foundation;
using UIKit;

namespace MvvmUtil.iOS
{
	public class SimpleTableViewSource<T> : UITableViewSource
	{
		private IEnumerable<T> _itemsSource;
		private UITableView _tableView;
		private Func<int, nfloat> _cellHeightProvider;
		private string _emptyText;

		protected string _reuseIdentifier;

		public event EventHandler<int> ItemIndexSelected;

		public event EventHandler<T> ItemSelected;

		public event EventHandler<NSIndexPath> RowItemSelected;

		public SimpleTableViewSource(UITableView tableView, UINib cellNib, string reuseIdentifier, nfloat estimatedRowHeight, string emptyText = null) : this(tableView, cellNib, reuseIdentifier, null, emptyText)
		{
			_tableView.EstimatedRowHeight = estimatedRowHeight;
		}

		public SimpleTableViewSource(UITableView tableView, UINib cellNib, string reuseIdentifier, string emptyText = null) : this(tableView, cellNib, reuseIdentifier, null, emptyText)
		{
			
		}

		public SimpleTableViewSource(UITableView tableView, UINib cellNib, string reuseIdentifier, Func<int, nfloat> cellHeightProvider, string emptyText)
		{
			_tableView = tableView;
			_reuseIdentifier = reuseIdentifier;
			_cellHeightProvider = cellHeightProvider;
			_emptyText = emptyText;

			if (!string.IsNullOrEmpty(_emptyText))
			{
				EmptyBackgroundView = new EmptyBackgroundView();
			}

			//register cell
			_tableView.RegisterNibForCellReuse(cellNib, _reuseIdentifier);

			//Auto Resize
			if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
			{
				_tableView.RowHeight = UITableView.AutomaticDimension;
			}
		}

		/// <summary>
		/// Gets or sets the empty background view.
		/// </summary>
		/// <value>The empty background view.</value>
		public EmptyBackgroundView EmptyBackgroundView { get; set; }

		/// <summary>
		/// Gets or sets the items source.
		/// </summary>
		/// <value>The items source.</value>
		public virtual IEnumerable<T> ItemsSource
		{
			get
			{
				return _itemsSource;
			}
			set
			{
				//if (_itemsSource == value)
				//	return;
				
				_itemsSource = value;
				ReloadTableData();
			}
		}

		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
		public int Count
		{
			get { return _itemsSource != null ? _itemsSource.Count() : 0; }
		}

		/// <summary>
		/// Gets the <see cref="T:MvvmUtil.iOS.SimpleTableViewSource`1"/> at the specified index.
		/// </summary>
		/// <param name="index">Index.</param>
		public T this[int index]
		{
			get
			{
				return ItemsSource != null ? ItemsSource.ElementAtOrDefault(index) : default(T);	
			}
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell(_reuseIdentifier);

			if (cell == null)
			{
				throw new Exception("Missing register NIB to UITableView before GetCell on UITableViewSource");
			}

			if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
			{
				cell.PreservesSuperviewLayoutMargins = false;
			}

			//Simple IBindableTableViewCell
			if (cell is IBindableIndexCell<T>)
			{
				var bindaleCell = cell as IBindableIndexCell<T>;
				bindaleCell.Bind(_itemsSource.ElementAt(indexPath.Row), tableView, indexPath);
			}

			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			var count = this.Count;

			//Add BackgroundView
			if (ItemsSource != null && ItemsSource.Count() == 0)
			{
				if (EmptyBackgroundView != null)
				{
					EmptyBackgroundView.EmptyText = _emptyText;
					_tableView.BackgroundView = EmptyBackgroundView;
				}
			}	else
			{
				_tableView.BackgroundView = null;
			}

			return count;
		}

		public override nint NumberOfSections(UITableView tableView)
		{
			return 1;
		}

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			//tableview height
			if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
			{
				return _tableView.RowHeight;
			}

			return _cellHeightProvider != null ? _cellHeightProvider(indexPath.Row) : _tableView.RowHeight;
		}

		public override nfloat EstimatedHeight(UITableView tableView, NSIndexPath indexPath)
		{
			return _cellHeightProvider != null ? _cellHeightProvider.Invoke(indexPath.Row) : _tableView.RowHeight;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow(indexPath, true);

			if (RowItemSelected != null)
			{
				RowItemSelected(this, indexPath);
			}

			if (ItemIndexSelected != null)
			{
				ItemIndexSelected(this, indexPath.Row);
			}

			if (ItemSelected != null)
			{
				ItemSelected(this, ItemsSource.ElementAtOrDefault(indexPath.Row));
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_itemsSource != null)
				{
					var collectionChanged = _itemsSource as INotifyCollectionChanged;
					if (collectionChanged != null)
					{
						collectionChanged.CollectionChanged -= HandleCollectionChanged;
						System.Diagnostics.Debug.WriteLine("Clear " + GetType().Name + " collectionChanged.CollectionChanged");
					}
				}
			}

			base.Dispose(disposing);
		}

		private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			//easyly reload Table
			ReloadTableData();
		}

		private void ReloadTableData()
		{
			_tableView.ReloadData();
		}
	}
}

