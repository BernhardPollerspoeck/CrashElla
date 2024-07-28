using Microsoft.Extensions.Logging;
using CrashElla.Ingest.Http.Seq;
using CrashElla.Framework.Maui;

namespace CrashElla.Demo;
public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddTransient<MainPage>();

		builder.Logging
			.AddHttpSeqCrashElla(config =>
			{
				config.ApiKey = "No0IBNPRdeVFJZ35Pdb0";
				config.IngestUri = "http://localhost:5341";
			})
			.WithMauiCrashStore();



		var host = builder.Build();

		host.EnableMauiCrashElla();

		return host;
	}
}
