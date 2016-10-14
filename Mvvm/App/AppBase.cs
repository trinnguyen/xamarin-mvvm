using System;
using Autofac;
using Autofac.Core;

namespace Mvvm
{
	public abstract class AppBase
	{
		public IContainer Container { get; private set; }

		public IPresenter Presenter { get; private set; }

		private IModule _platformModule;

		protected AppBase()
		{
		}

		/// <summary>
		/// Init app with platform platformModule.
		/// </summary>
		/// <param name="platformModule">Platform module.</param>
		public void Init(IModule platformModule)
		{
			_platformModule = platformModule;

			//Initialize all Components and startup
			this.InitializeComponents();
		}

		protected virtual ContainerBuilder CreateAutofacBuilder()
		{
			return new ContainerBuilder();
		}

		protected virtual void OnInitialized()
		{
		}

		protected abstract void RegisterCoreComponents(ContainerBuilder builder);

		protected abstract void Startup();

		private void InitializeComponents()
		{
			//2 - Init Autofac with components and modules
			Log.Info("Start init Autofac components and modules");
			InitAutofac();
			Log.Info("Finished setting up Autofac");

			//3 - Finished
			Log.Info("OnInitialized");
			OnInitialized();

			//4 - StartUp
			Log.Info("Startup: show first screen");
			using (var scope = Container.BeginLifetimeScope())
			{
				//create log
				Log.Logger = scope.Resolve<ILogger>();

				//assign presenter
				Presenter = scope.Resolve<IPresenter>();

				//startup
				Startup();
			}
		}

		private void InitAutofac()
		{
			ContainerBuilder builder = CreateAutofacBuilder();

			//register specific components in project
			RegisterCoreComponents(builder);

			//register default logger
			builder.Register((c) => new DefaultLogger()).As<ILogger>().SingleInstance();

			//register platform module
			if (_platformModule != null)
			{
				builder.RegisterModule(_platformModule);
			}

			//build
			Container = builder.Build(Autofac.Builder.ContainerBuildOptions.None);
		}
	}
}