using CrashElla.Core.Data;

namespace CrashElla.Core;

/// <summary>
/// Represents an interface for ingesting log entries.
/// </summary>
public interface IIngestProvider
{
	/// <summary>
	/// Ingests a log entry with custom properties.
	/// </summary>
	/// <param name="entry">The log entry to ingest.</param>
	/// <param name="customProperties">The custom properties associated with the log entry.</param>
	void Ingest(LogEntry entry, IReadOnlyDictionary<string, object> customProperties);
}
