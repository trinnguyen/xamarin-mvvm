using System;
namespace Mvvm
{
	public abstract class ViewModelBase : BindableBase
	{
		private string _title;
		public string Title
		{
			get { return _title; }
			set
			{
				SetProperty(ref _title, value);
			}
		}

		public abstract void Load();
	}
}