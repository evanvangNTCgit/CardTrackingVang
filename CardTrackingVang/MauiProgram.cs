using CardTrackingVang.DataAccess;
using CardTrackingVang.DataServices;
using CardTrackingVang.ViewModel;
using Microsoft.Extensions.Logging;

namespace CardTrackingVang
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddDbContext<DataContext>();
            builder.Services.AddSingleton<DataService>(); // Pages should likely use only on Service
            builder.Services.AddSingleton<CardsListViewModel>(); // Also decided for pages to share one list view model.
            builder.Services.AddTransient<CardDetails>();
            builder.Services.AddTransient<AddCard>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<CardHandling>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
