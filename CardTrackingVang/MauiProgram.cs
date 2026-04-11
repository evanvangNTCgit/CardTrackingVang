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
            // builder.Configuration.AddJsonFile("appsettings.local.json");

            // GPT-------
            // Get the assembly where the json is located
            var assembly = typeof(App).Assembly;
            // The name is usually [ProjectName].[FileName]
            using var stream = assembly.GetManifestResourceStream("CardTrackingVang.Resources.Raw.appsettings.local.json");

            if (stream != null)
            {
                var config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();

                var Aiskeys = new AiKeys()
                {
                    OpenAIEndpoint = config["OpenAiEndpoint"] ?? "",
                    OpenAIKey = config["OpenAiKey"] ?? "",
                    ComputerVisionEndpoint = config["ComputerVisionEndpoint"] ?? "",
                    ComputerVisionKey = config["ComputerVisionKey"] ?? "",
                    TextAnalyticsEndpoint = builder.Configuration["TextAnalyticsEndpoint"] ?? "",
                    TextAnalyticsKey = builder.Configuration["TextAnalyticsKey"] ?? "",
                    SpeechServiceEndpoint = builder.Configuration["SpeechServiceEndpoint"] ?? "",
                    SpeechServiceKey = builder.Configuration["SpeechServiceKey"] ?? "",
                };

                // Safely initialize the client
                if (!string.IsNullOrEmpty(Aiskeys.OpenAIEndpoint) && !string.IsNullOrEmpty(Aiskeys.OpenAIKey))
                {
                    var foundryClient = new AzureOpenAIClient(new Uri(Aiskeys.OpenAIEndpoint), new System.ClientModel.ApiKeyCredential(Aiskeys.OpenAIKey));
                    builder.Services.AddSingleton(foundryClient.GetChatClient("gpt-4o").AsIChatClient());

                    var endpoint = Aiskeys.OpenAIEndpoint;
                    var apiKey = Aiskeys.OpenAIKey;
                    // var foundryClient = new AzureOpenAIClient(new Uri(endpoint), new System.ClientModel.ApiKeyCredential(apiKey));
                    var chatClient = foundryClient.GetChatClient("gpt-4o").AsIChatClient();
                    builder.Services.AddSingleton(chatClient);
                    builder.Services.AddSingleton<ChatService>();
                    builder.Services.AddSingleton(new ComputerVisionService(Aiskeys));
                }
                // GPT------
                builder.Services.AddSingleton(Aiskeys);
            }
#if DEBUG
                builder.Logging.AddDebug();
#endif

                return builder.Build();
            }
        }
    }
