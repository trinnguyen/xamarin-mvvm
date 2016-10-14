using System;
using Autofac.Core;
using Autofac;

namespace SampleAutofac.iOS
{
	public class IosAutofacModule : Autofac.Module
	{
		protected override void Load(Autofac.ContainerBuilder builder)
		{
			builder.Register(c => new IosPresenter()).As<Mvvm.IPresenter>();
		}
	}
}
