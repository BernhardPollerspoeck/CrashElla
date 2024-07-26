namespace CrashElla.Core.Data;


public enum LogLevel
{
	/// <summary>
	/// Represents the finest level of logging.
	/// </summary>
	Trace,

	/// <summary>
	/// Represents debugging information.
	/// </summary>
	Debug,

	/// <summary>
	/// Represents informational messages.
	/// </summary>
	Information,

	/// <summary>
	/// Represents warnings.
	/// </summary>
	Warning,

	/// <summary>
	/// Represents errors.
	/// </summary>
	Error,

	/// <summary>
	/// Represents fatal errors.
	/// </summary>
	Fatal
}
