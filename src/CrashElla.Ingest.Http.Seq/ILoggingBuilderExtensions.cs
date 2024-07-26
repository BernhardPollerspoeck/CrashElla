using CrashElla.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace CrashElla.Ingest.Http.Seq;

public static class ILoggingBuilderExtensions
{
	public static ILoggingBuilder AddHttpSeqCrashElla(this ILoggingBuilder builder, Action<HttpSeqIngestProviderConfiguration> configure)
	{
		builder.Services.Configure(configure);
		builder.Services.AddSingleton<IIngestProvider, HttpSeqIngestProvider>();
		builder.AddCrashElla();
		return builder;
	}
}

