
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace MvvmUtil.Droid
{
	/// <summary>
	/// Custom recycler view adapter.
	/// T is Model class
	/// C is ViewHoler
	/// </summary>
	public class CustomRecyclerViewAdapter<T> : RecyclerView.Adapter
	{
		protected Context _context;
		private int _rowId;
		private Func<Context, View, RecyclerViewHolderBase<T>> _actionCreateViewHolder;

		public IEnumerable<T> Items { get; private set; }

		public CustomRecyclerViewAdapter(Context ctx, int rowLayoutId, Func<Context, View, RecyclerViewHolderBase<T>> actionCreateViewHolder) : this(ctx)
		{
			_rowId = rowLayoutId;
			_actionCreateViewHolder = actionCreateViewHolder;
		}

		public CustomRecyclerViewAdapter(Context ctx)
		{
			_context = ctx;
		}

		public override int ItemCount
		{
			get
			{
				return Items != null ? Items.Count() : 0;
			}
		}

		public virtual void SetDataSource(IEnumerable<T> items)
		{
			Items = items;
			NotifyDataSetChanged();
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			var item = Items.ElementAtOrDefault(position);

			// Replace the contents of the view with that element
			RecyclerViewHolderBase<T> viewHolder = (RecyclerViewHolderBase<T>)holder;
			viewHolder.Populate(item, ItemCount, position);
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			View view = LayoutInflater.From(_context).Inflate(_rowId, parent, false);
			return _actionCreateViewHolder(_context, view);
		}
	}
}

