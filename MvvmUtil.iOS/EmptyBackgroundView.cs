using System;
using UIKit;

namespace MvvmUtil.iOS
{
	public class EmptyBackgroundView : UIView
	{
		private UILabel _lbText;

		public EmptyBackgroundView()
		{
			_lbText = new UILabel()
			{
				TextAlignment = UITextAlignment.Center,
				TextColor = UIColor.Gray
			};
			AddSubview(_lbText);
		}

		public string EmptyText
		{
			get
			{
				return _lbText.Text;
			}
			set
			{
				_lbText.Text = value;
			}
		}

		public override void LayoutSubviews()
		{
			_lbText.Frame = this.Bounds;
			base.LayoutSubviews();
		}
	}
}

