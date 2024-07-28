using CrashElla.Core;

namespace CrashElla.Demo;

public partial class MainPage : ContentPage
{
	private readonly ICrashElla _crashElla;

	public MainPage(ICrashElla crashElla)
	{
		InitializeComponent();
		_crashElla = crashElla;
	}

	private void OnCatchedClicked(object sender, EventArgs e)
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

	private void OnUncatchedClicked(object sender, EventArgs e)
	{
		throw new InvalidOperationException("Uncaught");
	}
}

