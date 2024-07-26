using CrashElla.Core.Data;

namespace CrashElla.Core;

public interface ICrashElla
{
	/// <summary>
	/// Logs a log entry.
	/// </summary>
	/// <param name="entry">The log entry to be logged.</param>
	void Log(LogEntry entry);

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
