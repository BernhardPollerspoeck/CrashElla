using CrashElla.Core.Data;

namespace CrashElla.Core;

public record IngestEntry(Guid Id, LogEntry Log, IReadOnlyDictionary<string, object> Properties);