using CrashElla.Core;
using CrashElla.Core.Extensions;

namespace CrashElla.Demo;

public partial class MainPage : ContentPage
{
	private readonly ICrashElla _crashElla;

	public MainPage(ICrashElla crashElla)
	{
		InitializeComponent();
		_crashElla = crashElla;
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		try
		{
			throw new InvalidOperationException("Test");
		}
		catch (Exception ex)
		{
			_crashElla.Exception(ex);
		}
	}

	private void OnCounter2Clicked(object sender, EventArgs e)
	{
		_crashElla.LogInformation("Counter 2 {Now} has been clicked", DateTime.UtcNow);

	}
}

