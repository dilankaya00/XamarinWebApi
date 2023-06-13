using Microsoft.AspNetCore.Components.WebView.Maui;
using MudBlazor.Services;
using PetFoodShopping.Data;

namespace PetFoodShopping
{
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
                });

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
            builder.Services.AddMudServices();
            builder.Services.AddSingleton<WeatherForecastService>();

            return builder.Build();
        }
    }
}