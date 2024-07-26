using CrashElla.Core.Extensions;
using System.ComponentModel;

namespace CrashElla.Core;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class CrashEllaInitializer
{
	public static void Initialize(ICrashElla crashElla)
	{
		AppDomain.CurrentDomain.UnhandledException += (sender, args)
			=> crashElla.Exception((Exception)args.ExceptionObject);

	}
}
