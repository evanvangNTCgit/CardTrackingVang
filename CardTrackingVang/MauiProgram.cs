using Azure.AI.OpenAI;
using CardTrackingVang.AiServices;
using CardTrackingVang.DataAccess;
using CardTrackingVang.DataServices;
using CardTrackingVang.ViewModel;
using CommunityToolkit.Maui;
using Microsoft.Extensions.AI;
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
                .UseMauiCommunityToolkitCamera()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            //builder.Configuration.AddUserSecrets<AiKeys>();
            builder.Services.AddDbContext<DataContext>();
            builder.Services.AddSingleton<DataService>(); // Pages should likely use only on Service
            builder.Services.AddSingleton<CardsListViewModel>(); // Also decided for pages to share one list view model.
            builder.Services.AddTransient<CardDetails>();
            builder.Services.AddTransient<AddCard>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<CardHandling>();

            // Reading from secrets.json
            // CANNOT READ FROM SECRETS.JSON ON MOBILE.
            // must do this instead.
            builder.Configuration.AddJsonFile("appsettings.local.json");
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

            var endpoint = Aiskeys.OpenAIEndpoint;
            var apiKey = Aiskeys.OpenAIKey;
            var foundryClient = new AzureOpenAIClient(new Uri(endpoint), new System.ClientModel.ApiKeyCredential(apiKey));
            var chatClient = foundryClient.GetChatClient("gpt-4o").AsIChatClient();
            builder.Services.AddSingleton(chatClient);
            builder.Services.AddSingleton<ChatService>();
            builder.Services.AddSingleton(Aiskeys);
            builder.Services.AddSingleton(new ComputerVisionService(Aiskeys));
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
