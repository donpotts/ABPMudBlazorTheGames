using TheGames.Maui.Blazor.Services;
using TheGames.Shared.Blazor;
using TheGames.Shared.Blazor.Services;

namespace TheGames.Maui.Blazor;

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

        builder.Services.AddBlazorServices("https://localhost:44354/");
        builder.Services.AddSingleton<IStorageService, MauiStorageService>();
        
        return builder.Build();
    }
}
