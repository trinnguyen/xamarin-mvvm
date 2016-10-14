using Autofac;
using Autofac.Core;

namespace SampleAutofac.Droid
{
	class DroidAutofacModule : Autofac.Module
	{
		protected override void Load(Autofac.ContainerBuilder builder)
		{
			builder.Register(c => new DroidPresenter()).As<Mvvm.IPresenter>();
			builder.Register(c => new DroidLogger()).As<Mvvm.ILogger>();
		}
	}
}