using System;
namespace Mvvm
{
	public static class Log
	{
		/// <summary>
		/// Gets or sets the logger.
		/// </summary>
		/// <value>The logger.</value>
		internal static ILogger Logger { get; set; }

		/// <summary>
		/// Debug the specified format and objects.
		/// </summary>
		/// <param name="format">Format.</param>
		/// <param name="objects">Objects.</param>
		public static void Debug(string format, params object[] objects)
		{
			string message = string.Format(format, objects);
			LogDebug(message);
		}

		/// <summary>
		/// Debug the specified message.
		/// </summary>
		/// <param name="message">Message.</param>
		public static void Debug(object message)
		{
			LogDebug(message != null ? message.ToString() : "NULL");
		}

		/// <summary>
		/// Info the specified format and objects.
		/// </summary>
		/// <param name="format">Format.</param>
		/// <param name="objects">Objects.</param>
		public static void Info(string format, params object[] objects)
		{
			string message = string.Format(format, objects);
			LogInfo(message);
		}

		/// <summary>
		/// Info the specified message.
		/// </summary>
		/// <param name="message">Message.</param>
		public static void Info(object message)
		{
			LogInfo(message != null ? message.ToString() : "NULL");
		}

		/// <summary>
		/// Errors the message.
		/// </summary>
		/// <param name="format">Format.</param>
		/// <param name="objects">Objects.</param>
		public static void Error(string format, params object[] objects)
		{
			string message = string.Format(format, objects);
			LogError(message);
		}

		/// <summary>
		/// Error the specified message.
		/// </summary>
		/// <param name="message">Message.</param>
		public static void Error(object message)
		{
			LogError(message != null ? message.ToString() : "NULL");
		}

		/// <summary>
		/// Error the specified exception.
		/// </summary>
		/// <param name="exception">Exception.</param>
		public static void Error(Exception exception)
		{
			string message = exception.Message;

			var innerException = exception;
			while (innerException.InnerException != null)
			{
				innerException = innerException.InnerException;
			}

			message += ("\n" + innerException.Message);
			LogError(message);
		}

		private static void LogDebug(string message)
		{
			Logger.Log(LoggerCategory.Debug, message);
		}

		private static void LogError(string message)
		{
			Logger.Log(LoggerCategory.Error, message);
		}

		private static void LogInfo(string message)
		{
			Logger.Log(LoggerCategory.Info, message);
		}
	}
}

