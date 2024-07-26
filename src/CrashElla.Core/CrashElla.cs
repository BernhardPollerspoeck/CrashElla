using CrashElla.Core.Data;

namespace CrashElla.Core;

/// <inheritdoc/>
internal class CrashElla(IIngestProvider ingestProvider) : ICrashElla
{
	private readonly Dictionary<string, object> _customProperties = [];

	/// <inheritdoc/>
	public void Log(LogEntry entry)
	{
		ingestProvider.Ingest(entry, _customProperties);
	}

	/// <inheritdoc/>
	public void AddProperty(string key, object value)
	{
		_customProperties[key] = value;
	}

	/// <inheritdoc/>
	public void RemoveProperty(string key)
	{
		_customProperties.Remove(key);
	}
}
