using System;
namespace Mvvm
{
	public static class Log
	{
		/// <summary>
		/// Info the specified format and objects.
		/// </summary>
		/// <param name="format">Format.</param>
		/// <param name="objects">Objects.</param>
		public static void InfoMessage(string format, params object[] objects)
		{
			string message = string.Format(format, objects);
			LogInfo(message);
		}

		/// <summary>
		/// Info the specified stackItems.
		/// </summary>
		/// <param name="stackItems">Stack items.</param>
		public static void Info(params object[] stackItems)
		{
			if (stackItems != null)
			{
				string message = string.Join("->", stackItems);
				LogInfo(message);
			}
		}

		/// <summary>
		/// Info the specified message.
		/// </summary>
		/// <param name="message">Message.</param>
		public static void InfoMessage(object message)
		{
			LogInfo(message != null ? message.ToString() : "NULL");
		}

		/// <summary>
		/// Error the specified stackItems.
		/// </summary>
		/// <param name="stackItems">Stack items.</param>
		public static void Error(params object[] stackItems)
		{
			if (stackItems != null)
			{
				string message = string.Join("->", stackItems);
				LogError(message);
			}
		}

		/// <summary>
		/// Error the specified format and objects.
		/// </summary>
		/// <param name="format">Format.</param>
		/// <param name="objects">Objects.</param>
		public static void ErrorMessage(string format, params object[] objects)
		{
			string message = string.Format(format, objects);
			LogError(message);
		}

		/// <summary>
		/// Error the specified message.
		/// </summary>
		/// <param name="message">Message.</param>
		public static void ErrorMessage(object message)
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

		private static void LogInfo(string message)
		{
			System.Diagnostics.Debug.WriteLine("{0}: {1}: {2}", "Info", DateTime.Now.ToString(), message);
		}

		private static void LogError(string message)
		{
			System.Diagnostics.Debug.WriteLine("{0}: {1}: {2}", "Error", DateTime.Now.ToString(), message);
		}
	}
}

