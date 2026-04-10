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
                    VisualFeatureTypes.Objects,
                    VisualFeatureTypes.Tags,
                    VisualFeatureTypes.Color,
                    VisualFeatureTypes.Description,
                    VisualFeatureTypes.Faces,
                };

                var result = await _client.AnalyzeImageInStreamAsync(imageStream, features);

                StringBuilder sb = new();

                if (result.Description?.Captions.Count > 0)
                {
                    sb.AppendLine("Captions:");
                    foreach (var caption in result.Description.Captions)
                    {
                        sb.AppendLine($"{caption.Text} (Confidence: {caption.Confidence:P2})");
                    }
                    sb.AppendLine();
                }

                if (result.Tags?.Count > 0)
                {
                    sb.AppendLine("Tags:");
                    foreach (var tag in result.Tags.OrderByDescending(t => t.Confidence))
                    {
                        sb.AppendLine($"{tag.Name} (Confidence: {tag.Confidence:P2})");
                    }
                    sb.AppendLine();
                }

                if (result.Objects?.Count > 0)
                {
                    sb.AppendLine("Objects:");
                    foreach (var obj in result.Objects.OrderByDescending(o => o.Confidence))
                    {
                        sb.AppendLine($"{obj.ObjectProperty} (Confidence: {obj.Confidence:P2})");
                    }
                    sb.AppendLine();
                }

                if (result.Faces?.Count > 0)
                {
                    sb.AppendLine("Faces:");
                    foreach (var face in result.Faces)
                    {
                        sb.AppendLine($"Age: {face.Age}, Gender: {face.Gender}");
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

        public async Task<string> RecognizeTextAsync(Stream imageStream)
        {
            try
            {
                // Read text from image
                var result = await _client.RecognizePrintedTextInStreamAsync(true, imageStream);

                // Extract text
                var sb = new StringBuilder();
                sb.AppendLine("Extracted Text:");

                foreach (var region in result.Regions)
                {
                    foreach (var line in region.Lines)
                    {
                        var lineText = string.Join(" ", line.Words.Select(w => w.Text));
                        sb.AppendLine(lineText);
                    }
                    sb.AppendLine();
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                return $"Error recognizing text: {ex.Message}";
            }
        }
    }
}
