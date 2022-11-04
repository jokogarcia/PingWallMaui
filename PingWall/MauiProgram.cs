using CommunityToolkit.Maui;

namespace PingWall;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().UseMauiCommunityToolkit();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Brands-Regular-400.otf", "FAB");
                fonts.AddFont("Free-Regular-400.otf", "FAR");
                fonts.AddFont("Free-Solid-900.otf", "FAS");
            });

        //Services
        builder.Services.AddTransient<Services.IPingService, Services.PingService>();
        builder.Services.AddSingleton<Services.IHostDTORepository, Services.HostDTORepository>();
        builder.Services.AddSingleton<Services.IPingHistoryRepository, Services.PingHistoryRepository>();
        //Pages
        builder.Services.AddSingleton<MainPage>();
        //builder.Services.AddTransient<SinglePing>();

        //ViewModels
        builder.Services.AddSingleton<ViewModel.MainPageViewModel>();
        builder.Services.AddTransient<ViewModel.SinglePingViewModel>();
        return builder.Build();
    }
}
