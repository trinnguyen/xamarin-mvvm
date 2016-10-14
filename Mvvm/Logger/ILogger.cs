using System;
namespace Mvvm
{
	public interface ILogger
	{
		void Log(LoggerCategory category, string message);
	}

	public enum LoggerCategory
	{
		Debug,
		Info,
		Error
	}
}
