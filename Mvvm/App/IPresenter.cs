using System;
namespace Mvvm
{
	public interface IPresenter
	{
		void Show<T>() where T : ViewModelBase;

		void Close(ViewModelBase viewModel);
	}
}