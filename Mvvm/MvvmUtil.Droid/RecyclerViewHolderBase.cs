using System;
using Android.Views;

namespace MvvmUtil.Droid
{
	public abstract class RecyclerViewHolderBase<T> : Android.Support.V7.Widget.RecyclerView.ViewHolder
	{
		public Android.Content.Context Context { get; private set; }

		protected RecyclerViewHolderBase(Android.Content.Context context, View view) : base(view)
		{
			Context = context;

			if (view.Background == null)
			{
				AddRippleEffect(view);				
			}
		}

		public abstract void Populate(T cellViewModel, int totalRows, int position);

		static void AddRippleEffect(View view)
		{
			int[] attrs = new int[] { Android.Resource.Attribute.SelectableItemBackground };
			Android.Content.Res.TypedArray typedArray = view.Context.ObtainStyledAttributes(attrs);
			int backgroundResource = typedArray.GetResourceId(0, 0);

			//set resource
			view.SetBackgroundResource(backgroundResource);
		}
	}
}

