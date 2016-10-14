using System;
using Mvvm;

namespace SampleAutofac.Droid
{
	class DroidPresenter : Mvvm.IPresenter
	{
		public DroidPresenter()
		{
		}

		public void Close(IViewModel viewModelBase)
		{

		}

		public void Show<T>() where T : IViewModel
		{
			
		}
	}
}