using CardTrackingVang.AiServices;
using CardTrackingVang.DataAccess;
using CardTrackingVang.DataServices;
using CardTrackingVang.ViewModel;
using Microsoft.Extensions.Configuration;
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
            builder.Configuration.AddUserSecrets<AiKeys>();
            builder.Services.AddDbContext<DataContext>();
            builder.Services.AddSingleton<DataService>(); // Pages should likely use only on Service
            builder.Services.AddSingleton<CardsListViewModel>(); // Also decided for pages to share one list view model.
            builder.Services.AddTransient<CardDetails>();
            builder.Services.AddTransient<AddCard>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<CardHandling>();

            // Reading from secrets.json
            //AiKeys.OpenAIEndpoint = builder.Configuration["OpenAiEndpoint"]!;
            //AiKeys.OpenAIKey = builder.Configuration["OpenAiKey"]!;
            //AiKeys.ComputerVisionEndpoint = builder.Configuration["ComputerVisionEndpoint"]!;
            //AiKeys.ComputerVisionEndpoint = builder.Configuration["ComputerVisionEndpoint"]!;
            //AiKeys.TextAnalyticsEndpoint = builder.Configuration["TextAnalyticsEndpoint"]!;
            //AiKeys.TextAnalyticsKey = builder.Configuration["TextAnalyticsKey"]!;
            //AiKeys.SpeechServiceEndpoint = builder.Configuration["SpeechServiceEndpoint"]!;
            //AiKeys.SpeechServiceKey = builder.Configuration["SpeechServiceKey"]!;
#if DEBUG
            var Aiskeys = new AiKeys()
            {
                OpenAIEndpoint = builder.Configuration["OpenAiEndpoint"]!,
                OpenAIKey = builder.Configuration["OpenAiKey"]!,
                ComputerVisionEndpoint = builder.Configuration["ComputerVisionEndpoint"]!,
                ComputerVisionKey = builder.Configuration["ComputerVisionKey"]!,
                TextAnalyticsEndpoint = builder.Configuration["TextAnalyticsEndpoint"]!,
                TextAnalyticsKey = builder.Configuration["TextAnalyticsKey"]!,
                SpeechServiceEndpoint = builder.Configuration["SpeechServiceEndpoint"]!,
                SpeechServiceKey = builder.Configuration["SpeechServiceKey"]!
            };
            builder.Services.AddSingleton(Aiskeys);

            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
