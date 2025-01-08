using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace AnnuaireEntreprise.Maui;

// Entry point for the application.
public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		// Create a new MauiApp.
		var builder = MauiApp.CreateBuilder();
		builder.UseMauiCommunityToolkit();
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

		return builder.Build();
	}
}
