using System;
using Mvvm;

namespace SampleAutofac.Droid
{
	class DroidLogger : Mvvm.ILogger
	{
		public void Log(LoggerCategory category, string message)
		{
			switch (category)
			{
				case LoggerCategory.Debug:
					Android.Util.Log.Debug("sample-autofac", message);
					return;
				case LoggerCategory.Info:
					Android.Util.Log.Info("sample-autofac", message);
					return;
				case LoggerCategory.Error:
					Android.Util.Log.Error("sample-autofac", message);
					return;
				default:
					break;
			}
		}
	}
}