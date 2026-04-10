using CardTrackingVang.DataServices;
using CardTrackingVang.Models;
using Microsoft.Extensions.AI;
using System.Text;
using System.Text.RegularExpressions;

namespace CardTrackingVang.AiServices
{
    public class ChatService
    {
        private readonly IChatClient _chatClient;
        private readonly DataService _service;

        public ChatService(IChatClient chatClient, DataService ds)
        {
            this._chatClient = chatClient;
            this._service = ds;
        }
        public async Task<string> AnswerQuestion(string question)
        {
            try
            {
                StringBuilder sb = new();
                sb.AppendLine($"Please answer {question}");
                sb.AppendLine("The answer should be less than one paragraph");

                var response = await _chatClient.GetResponseAsync(sb.ToString());

                var AiResponse = response.Text;

                if (AiResponse != null)
                {
                    return AiResponse;
                }
                else
                {
                    return "Question seemed to not be answered\nPlease ask a different question";
                }
            }
            catch (InvalidOperationException ex)
            {
                throw (new Exception("Chat client is not initialized. Please add your OpenAI API key in settings.", ex));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> MakeCardBasedOnText(string text) 
        {
            try
            {
                StringBuilder sb = new();
                sb.AppendLine($"Based on the following text {text}");
                sb.AppendLine("Provide the following output to fulfill my Card model.");
                sb.AppendLine("1: A title representing the name of card (like Pikachu)");
                sb.AppendLine("2: A rough value of the card based on any commerce website with description/info");
                sb.AppendLine("3: Find the best card type that matches ONE OF THE FOLLOWING:");
                foreach(CardType ct in _service.GetCardTypes()) 
                {
                    sb.Append(ct.Type);
                }
                sb.AppendLine($"Return your reponse in the following order Title: x, Value: x, CardType: x");
                sb.AppendLine("MUST RETURN LIKE THIS: Title: Greninja, Value: 20.00, CardType: Pokemon");


                var response = await _chatClient.GetResponseAsync(sb.ToString());

                var AiResponse = response.Text;

                if (AiResponse != null)
                {
                    return AiResponse;
                }
                else
                {
                    return "Question seemed to not be answered\nPlease ask a different question";
                }
            }
            catch (InvalidOperationException ex)
            {
                throw (new Exception("Chat client is not initialized. Please add your OpenAI API key in settings.", ex));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Card TranslateAiToCard(string s)
        {
            // It should always follow that specified format.
            var regex = new Regex(@"Title:\s*(?<Title>.*),\s*Value:?\s*(?<Value>[\d.]+),\s*CardType:\s*(?<CardType>.*)");
            var match = regex.Match(s);
            if (!match.Success)
            {
                throw new FormatException();
            }
            var parts = s.Split(",");

            // Since I know it followed that specified format I can use index removing here safely.

            var cardTypeName = parts[2].Remove(0, 10).Trim();
            int CardTypeId = this._service.GetCardTypeId(cardTypeName);

            string valuestring = parts[1].Remove(0, 7).Trim();
            decimal.TryParse(valuestring, out decimal value);
            Card newCard = new()
            {
                Title = parts[0].Remove(0, 6),
                Value = value,
                CardTypeID = CardTypeId,
            };

            return newCard;
        }
    }
}
