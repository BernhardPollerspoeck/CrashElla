namespace CrashElla.Core;

public interface ICrashStore
{
	/// <summary>
	/// Stores a crash report.
	/// </summary>
	/// <param name="entry">The crash report to be stored.</param>
	void Store(IngestEntry entry);

	/// <summary>
	/// Removes a crash report.
	/// </summary>
	/// <param name="id">The ID of the crash report to be removed.</param>
	void Remove(Guid id);

	/// <summary>
	/// Gets all the crash report entries.
	/// </summary>
	/// <returns>An array of crash report entries.</returns>
	HashSet<Guid> GetEntries();

	/// <summary>
	/// Gets a specific crash report entry by its ID.
	/// </summary>
	/// <param name="id">The ID of the crash report entry.</param>
	/// <returns>The crash report entry.</returns>
	IngestEntry? GetEntry(Guid id);
}
