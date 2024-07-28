namespace CrashElla.Core;

public interface ICrashElla
{
	/// <summary>
	/// Logs the specified exception.
	/// </summary>
	/// <param name="exception">The exception to be logged.</param>
	void Exception(Exception exception);

	/// <summary>
	/// Logs the specified exception with a formatted message.
	/// </summary>
	/// <param name="exception">The exception to be logged.</param>
	/// <param name="messageTemplate">The message template.</param>
	/// <param name="parameters">The parameters to be formatted into the message.</param>
	void Exception(Exception exception, string messageTemplate, params object[] parameters);

	/// <summary>
	/// Adds a property to the crash report.
	/// </summary>
	/// <param name="key">The key of the property.</param>
	/// <param name="value">The value of the property.</param>
	void AddProperty(string key, object value);

	/// <summary>
	/// Removes a property from the crash report.
	/// </summary>
	/// <param name="key">The key of the property to be removed.</param>
	void RemoveProperty(string key);
}
