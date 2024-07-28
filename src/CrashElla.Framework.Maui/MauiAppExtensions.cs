using CrashElla.Core;

namespace CrashElla.Framework.Maui;
public static class MauiAppExtensions
{
#if WINDOWS
	private static Exception? _lastFirstChanceException;
#endif

	public static MauiApp EnableMauiCrashElla(this MauiApp app, Dictionary<string, object> customProperties)
	{
		var crashElla = app.Services.GetRequiredService<ICrashElla>();
		CrashEllaInitializer.Initialize(crashElla);

		if (customProperties.Count != 0)
		{
			foreach (var property in customProperties)
			{
				crashElla.AddProperty(property.Key, property.Value);
			}
		}

#if IOS || MACCATALYST

		// For iOS and Mac Catalyst
		// Exceptions will flow through AppDomain.CurrentDomain.UnhandledException,
		// but we need to set UnwindNativeCode to get it to work correctly. 
		// 
		// See: https://github.com/xamarin/xamarin-macios/issues/15252
		ObjCRuntime.Runtime.MarshalManagedException += (_, args)
			=> args.ExceptionMode = ObjCRuntime.MarshalManagedExceptionMode.UnwindNativeCode;

#elif ANDROID

		// For Android:
		// All exceptions will flow through Android.Runtime.AndroidEnvironment.UnhandledExceptionRaiser,
		// and NOT through AppDomain.CurrentDomain.UnhandledException
		Android.Runtime.AndroidEnvironment.UnhandledExceptionRaiser += (sender, args)
			=> crashElla.Exception(args.Exception);

#elif WINDOWS

		// For WinUI 3:
		//
		// * Exceptions on background threads are caught by AppDomain.CurrentDomain.UnhandledException,
		//   not by Microsoft.UI.Xaml.Application.Current.UnhandledException
		//   See: https://github.com/microsoft/microsoft-ui-xaml/issues/5221
		//
		// * Exceptions caught by Microsoft.UI.Xaml.Application.Current.UnhandledException have details removed,
		//   but that can be worked around by saved by trapping first chance exceptions
		//   See: https://github.com/microsoft/microsoft-ui-xaml/issues/7160
		//

		AppDomain.CurrentDomain.FirstChanceException += (_, args) 
			=> _lastFirstChanceException = args.Exception;

		Microsoft.UI.Xaml.Application.Current.UnhandledException += (sender, args) =>
		{
			var exception = args.Exception;

			if (exception.StackTrace is null && _lastFirstChanceException is not null)
			{
				exception = _lastFirstChanceException;
			}
			crashElla.Exception(exception);
		};
#endif


		return app;
	}

	public static MauiApp EnableMauiCrashElla(this MauiApp app)
	{
		var properties = new Dictionary<string, object>()
		{
			{ "App", AppInfo.Current.Name },
			{ "Version", AppInfo.Current.VersionString },
			{ "IsEmulator", DeviceInfo.Current.DeviceType is DeviceType.Virtual },
			{ "Device", DeviceInfo.Current.VersionString }
		};
		return app.EnableMauiCrashElla(properties);
	}
}
