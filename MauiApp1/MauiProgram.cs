namespace PingWall;

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

		//Services
		builder.Services.AddScoped<Services.IPingService, Services.PingMockService>();
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
