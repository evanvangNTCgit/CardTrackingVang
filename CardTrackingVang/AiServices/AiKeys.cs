using System;
using System.Collections.Generic;
using System.Text;

namespace CardTrackingVang.AiServices
{
    public class AiKeys
    {
        // This is what I used for secrets:
        // https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-10.0&tabs=windows#secret-manager
        // Open AI
        public required string OpenAIEndpoint;
        public required string OpenAIKey;
        // Open Ai Deployment name = ...

        // Azure Computer Vision
        public required string ComputerVisionEndpoint;
        public required string ComputerVisionKey;

        // Azure Text Analytics
        public required string TextAnalyticsEndpoint;
        public required string TextAnalyticsKey;

        // Speech Services
        public string SpeechServiceRegion = "eastus";
        public required string SpeechServiceEndpoint;
        public required string SpeechServiceKey;
    }
}
