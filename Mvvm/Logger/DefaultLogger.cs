using System;
using System.Globalization;

namespace Mvvm
{
	public class DefaultLogger : ILogger
	{
		public void Log(LoggerCategory category, string message)
		{
			System.Diagnostics.Debug.WriteLine("{0}: {1}: {2}", category, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",CultureInfo.InvariantCulture), message);
		}
	}
}
