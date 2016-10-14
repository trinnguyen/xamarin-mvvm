using System;
using Autofac;

namespace SampleAutofac
{
	public class SampleApp : Mvvm.AppBase
	{
		private static SampleApp _app;
		public static SampleApp App
		{
			get 
			{
				_app = _app ?? new SampleApp();
				return _app;
			}
		}

		protected override void RegisterCoreComponents(ContainerBuilder builder)
		{
			
		}

		protected override void Startup()
		{
			Presenter.Show<FirstViewModel>();	
		}
	}
}
