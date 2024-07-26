using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.ComponentModel;

namespace CrashElla.Core;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class ILoggingBuilderExtensions
{
	public static ILoggingBuilder AddCrashElla(this ILoggingBuilder builder)
	{
		builder.Services.AddSingleton<ICrashElla, CrashElla>();
		return builder;
	}
}
