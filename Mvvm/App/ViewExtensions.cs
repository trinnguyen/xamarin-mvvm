using System;
using System.Collections.Generic;
using Autofac;
using Mvvm;

namespace GSM.CorePCL
{
	public static class ViewExtensions
	{
		/// <summary>
		/// Resolve ViewModel and notify when done
		/// </summary>
		/// <param name="hostPage">Host view.</param>
		/// <param name="viewModelType">View model type.</param>
		public static void BindViewModel(this IView hostPage, AppBase currentApp, Type viewModelType)
		{
			//Resolve ViewModel
			using (var scope = currentApp.Container.BeginLifetimeScope())
			{
				var viewModel = (ViewModelBase)scope.Resolve(viewModelType);
				hostPage.ViewModel = viewModel;

				//add notify
				viewModel.PropertyChanged += hostPage.OnPropertyChanged;

				//notify viewModel set
				hostPage.OnViewModelSet();

				//load
				viewModel.Load();

				//notify viewModel set
				hostPage.OnViewModelLoaded();
			}
		}

		/// <summary>
		/// Casts the view model.
		/// </summary>
		/// <returns>The view model.</returns>
		/// <param name="hostView">Host page.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T CastViewModel<T>(this IView hostView) where T : ViewModelBase
		{
			if (hostView.ViewModel != null && hostView.ViewModel is T)
			{
				return (T)hostView.ViewModel;
			}

			//log error
			Mvvm.Log.Error("{0} -> CastViewModel -> {1} -> FAILED", hostView.GetType().Name, typeof(T).Name);
			return default(T);
		}
	}
}
