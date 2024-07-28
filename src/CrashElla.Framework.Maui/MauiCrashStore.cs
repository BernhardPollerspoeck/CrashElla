using CrashElla.Core;
using System.Text.Json;

namespace CrashElla.Framework.Maui;

internal class MauiCrashStore : ICrashStore
{
	private const string INDEX_KEY = "CrashIndex";
	private static readonly object _lock = new();

	public void Remove(Guid id)
	{
		lock (_lock)
		{
			var index = GetEntries();
			index.Remove(id);
			Preferences.Remove(id.ToString());
			SaveIndex(index);
		}
	}

	public void Store(IngestEntry entry)
	{
		lock (_lock)
		{
			var index = GetEntries();
			var id = entry.Id;
			index.Add(id);
			var entryJson = JsonSerializer.Serialize(entry);
			Preferences.Set(id.ToString(), entryJson);
			SaveIndex(index);
		}
	}
	public HashSet<Guid> GetEntries()
	{
		lock (_lock)
		{
			var indexJson = Preferences.Get(INDEX_KEY, string.Empty);
			return string.IsNullOrEmpty(indexJson)
				? []
				: JsonSerializer.Deserialize<HashSet<Guid>>(indexJson) ?? [];
		}
	}

	public IngestEntry? GetEntry(Guid id)
	{
		lock (_lock)
		{
			var entryJson = Preferences.Get(id.ToString(), string.Empty);
			return string.IsNullOrEmpty(entryJson)
				? null
				: JsonSerializer.Deserialize<IngestEntry>(entryJson) ?? null;
		}
	}


	private static void SaveIndex(HashSet<Guid> index)
	{
		lock (_lock)
		{
			var indexJson = JsonSerializer.Serialize(index);
			Preferences.Set(INDEX_KEY, indexJson);
		}
	}


}
