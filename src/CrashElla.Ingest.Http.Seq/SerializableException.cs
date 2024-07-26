namespace CrashElla.Ingest.Http.Seq;

internal class SerializableException
{
	public string Message { get; set; }
	public string? StackTrace { get; set; }
	public string? Source { get; set; }
	public SerializableException? InnerException { get; set; }

	public SerializableException(Exception ex)
	{
		Message = ex.Message;
		StackTrace = ex.StackTrace;
		Source = ex.Source;
		if (ex.InnerException != null)
		{
			InnerException = new SerializableException(ex.InnerException);
		}
	}
}
