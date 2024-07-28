using CrashElla.Core;
using Microsoft.Extensions.Logging;

namespace CrashElla.Framework.Maui;

public static class ILoggingBuilderExtensions
{
	public static ILoggingBuilder WithMauiCrashStore(this ILoggingBuilder builder)
	{
		builder.Services.AddSingleton<ICrashStore, MauiCrashStore>();
		return builder;
	}
}
