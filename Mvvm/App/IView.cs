using System;
using System.ComponentModel;
using Mvvm;

namespace GSM.CorePCL
{
	public interface IView
	{
		ViewModelBase ViewModel { get; set; }

		void OnViewModelSet();

		void OnViewModelLoaded();

		void OnPropertyChanged(object sender, PropertyChangedEventArgs e);
	}
}