using CrashElla.Core.Data;

namespace CrashElla.Core.Extensions;
/// <summary>
/// Extension methods for the <see cref="ICrashElla"/> interface.
/// </summary>
public static class ICrashEllaExtensions
{
	/// <summary>
	/// Logs an exception with the specified <paramref name="exception"/>.
	/// </summary>
	/// <param name="crashElla">The <see cref="ICrashElla"/> instance.</param>
	/// <param name="exception">The exception to log.</param>
	public static void Exception(this ICrashElla crashElla, Exception exception)
	{
		crashElla.Log(new LogEntry(
			exception.Message,
			LogLevel.Error,
			exception,
			null));
	}

	/// <summary>
	/// Logs an information message with the specified <paramref name="messageTemplate"/> and <paramref name="parameters"/>.
	/// </summary>
	/// <param name="crashElla">The <see cref="ICrashElla"/> instance.</param>
	/// <param name="messageTemplate">The message template.</param>
	/// <param name="parameters">The parameters to format the message template.</param>
	public static void LogInformation(this ICrashElla crashElla, string messageTemplate, params object[] parameters)
	{
		crashElla.Log(new LogEntry(
			messageTemplate,
			LogLevel.Information,
			null,
			parameters?.ToArray()));
	}
}
