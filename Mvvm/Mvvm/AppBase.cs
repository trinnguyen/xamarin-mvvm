using System;
using Autofac;
using Autofac.Core;

namespace Mvvm
{
	public abstract class AppBase
	{
		public IContainer Container { get; private set; }

		private ContainerBuilder _builder;
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
			Log("Start init Autofac components and modules");
			InitAutofac();
			Log("Finished setting up Autofac");

			//3 - Finished
			Log("OnInitialized");
			OnInitialized();

			//4 - StartUp
			Log("Startup: show first screen");
			Startup();
		}

		private void InitAutofac()
		{
			_builder = CreateAutofacBuilder();

			//register specific components in project
			RegisterCoreComponents(_builder);

			//register platform module
			if (_platformModule != null)
			{
				_builder.RegisterModule(_platformModule);
			}

			//build
			Container = _builder.Build(Autofac.Builder.ContainerBuildOptions.None);
		}

		protected void Log(string message)
		{
			Mvvm.Log.InfoMessage(message);
		}
	}
}

