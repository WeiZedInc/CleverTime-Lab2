using CommunityToolkit.Maui;

namespace CleverTime;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        Routing.RegisterRoute("CreateTimerPage", typeof(CreateTimerPage));

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainVM>();

        builder.Services.AddSingleton<CreateTimerPage>();

        return builder.Build();
	}
}
