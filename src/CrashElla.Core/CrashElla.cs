using CrashElla.Core.Data;
using System;

namespace CrashElla.Core;

/// <inheritdoc/>
internal class CrashElla(
	IIngestProvider ingestProvider,
	ICrashStore? crashStore) : ICrashElla
{
	private readonly Dictionary<string, object> _customProperties = [];

	/// <inheritdoc/>
	public void Exception(Exception exception)
	{
		Exception(exception, exception.Message);
	}

	/// <inheritdoc/>
	public void Exception(Exception exception, string messageTemplate, params object[] parameters)
	{
		Exception(new IngestEntry(
			Guid.NewGuid(),
			new LogEntry(
				exception.Message,
				LogLevel.Error,
				new(exception),
				parameters),
			_customProperties),
			true);
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


	internal void Exception(IngestEntry ingestEntry, bool store)
	{
		Task.Run(async () =>
		{
			if (store)
			{
				crashStore?.Store(ingestEntry);
			}

			var success = await ingestProvider.Ingest(
				ingestEntry.Log,
				ingestEntry.Properties);

			if (success)
			{
				crashStore?.Remove(ingestEntry.Id);
			}
		});
	}

	internal void RetryStoredEntries()
	{
		var entries = crashStore?.GetEntries();
		if (entries is not { Count: > 0 } || crashStore is null)
		{
			return;
		}

		foreach (var item in entries)
		{
			var entry = crashStore.GetEntry(item);
			if (entry is null)
			{
				continue;
			}

			Exception(entry, false);
		}
	}
}
