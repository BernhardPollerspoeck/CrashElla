namespace CrashElla.Core.Data;

/// <summary>
/// Represents a log entry.
/// </summary>
public record LogEntry(
	/// <summary>
	/// Gets or sets the message template of the log entry.
	/// </summary>
	string MessageTemplate,

	/// <summary>
	/// Gets or sets the log level of the log entry.
	/// </summary>
	LogLevel Level,

	/// <summary>
	/// Gets or sets the exception associated with the log entry.
	/// </summary>
	Exception? Exception,

	/// <summary>
	/// Gets or sets the parameters of the log entry.
	/// </summary>
	object[]? Parameters);
