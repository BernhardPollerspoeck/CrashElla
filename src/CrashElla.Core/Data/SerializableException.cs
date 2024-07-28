namespace CrashElla.Core.Data;

public class SerializableException
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

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public SerializableException()//This constructor is only for serialization
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	{
	}
}
