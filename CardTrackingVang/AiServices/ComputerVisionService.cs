using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Text;

namespace CardTrackingVang.AiServices
{
    public class ComputerVisionService
    {
        private readonly ComputerVisionClient _client;
        private readonly AiKeys _aiKeys;

        public ComputerVisionService(AiKeys a)
        {
            this._aiKeys = a;

            _client = new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(_aiKeys.ComputerVisionKey)
                )
            {
                Endpoint = _aiKeys.ComputerVisionEndpoint,
            };
        }

        public async Task<string> AnalyzeImageAsync(Stream imageStream) 
        {
            try
            {
                var features = new List<VisualFeatureTypes?>
            {
                VisualFeatureTypes.Categories,
                VisualFeatureTypes.Description,
                VisualFeatureTypes.Faces,
                VisualFeatureTypes.Brands,
                VisualFeatureTypes.Objects,
            };

                var result = await _client.AnalyzeImageInStreamAsync(imageStream, features);

                StringBuilder sb = new();

                if(result.Description?.Captions.Count > 0) 
                {
                    sb.AppendLine("Tags: ");
                    foreach(var tag in result.Tags.OrderByDescending(t => t.Confidence)) 
                    {
                        sb.AppendLine($"{tag.Name}\nConfidence{tag.Confidence}\n----------------------------");
                    }
                    sb.AppendLine();
                }

                if(result.Objects.Count > 0) 
                {
                    sb.AppendLine("Objects: ");
                    foreach(var obj in result.Objects.OrderByDescending(o => o.Confidence)) 
                    {
                        sb.AppendLine($"{obj.ObjectProperty}\nConfidence{obj.Confidence}\n----------------------------");
                    }
                    sb.AppendLine();
                }

                if (result.Categories.Count > 0) 
                {
                    sb.AppendLine("Categories: ");
                    foreach (var obj in result.Categories)
                    {
                        sb.AppendLine($"{obj.Name}\n----------------------------");
                    }
                    sb.AppendLine();
                }

                if (result.Faces.Count > 0)
                {
                    sb.AppendLine("Faces: ");
                    foreach (var obj in result.Faces)
                    {
                        sb.AppendLine($"Age: {obj.Age} Gender: {obj.Gender}\n----------------------------");
                    }
                    sb.AppendLine();
                }

                if (result.Brands.Count > 0)
                {
                    sb.AppendLine("Faces: ");
                    foreach (var obj in result.Brands)
                    {
                        sb.AppendLine($"Age: {obj.Name} Confidence: {obj.Confidence}\n----------------------------");
                    }
                    sb.AppendLine();
                }

                return sb.ToString();
            }
            catch
            {
                throw new NotImplementedException();
            }
        }
    }
}
