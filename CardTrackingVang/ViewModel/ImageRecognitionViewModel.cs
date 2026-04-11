using CardTrackingVang.AiServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardTrackingVang.ViewModel
{
    public partial class ImageRecognitionViewModel : ObservableObject
    {
        private readonly ComputerVisionService visionService;

        [ObservableProperty]
        private string imageSource;

        [ObservableProperty]
        private string analysisResult;

        [ObservableProperty]
        private bool isAnalyzing;

        [ObservableProperty]
        private bool isImageSelected;

        public ImageRecognitionViewModel(ComputerVisionService vs)
        {
            visionService = vs;
            isImageSelected = false;
            analysisResult = "Select an image to analyze";
            imageSource = "";
        }

        public async Task<string> PickAndAnalyzeImage(FileResult photo)
        {
            try
            {
                if (photo == null)
                    return "";

                // Display the selected image
                ImageSource = photo.FullPath;
                IsImageSelected = true;
                AnalysisResult = "Analyzing image...";
                IsAnalyzing = true;

                // Analyze the image
                using var stream = await photo.OpenReadAsync();
                var result = await visionService.AnalyzeImageAsync(stream);

                // Recognize text in image.
                using var stream2 = await photo.OpenReadAsync();
                var result2 = await visionService.RecognizeTextAsync(stream2);

                StringBuilder sb = new();
                sb.AppendLine(result);
                sb.AppendLine(result2);

                // Display the result
                AnalysisResult = sb.ToString();
            }
            catch (Exception ex)
            {
                AnalysisResult = $"Error: {ex.Message}";
            }
            finally
            {
                IsAnalyzing = false;
            }

            return AnalysisResult;
        }

        [RelayCommand]
        private async Task PickAndRecognizeText()
        {
            try
            {
                // Pick photo from gallery
                var photo = await MediaPicker.PickPhotoAsync();

                if (photo == null)
                    return;

                // Display the selected image
                ImageSource isc = photo.FullPath;
                IsImageSelected = true;
                AnalysisResult = "Recognizing Text";
                IsAnalyzing = true;

                // Recognize text in image.
                using var stream = await photo.OpenReadAsync();
                var result = await visionService.RecognizeTextAsync(stream);

                // Analyze the image
                using var stream2 = await photo.OpenReadAsync();
                var result2 = await visionService.AnalyzeImageAsync(stream);

                StringBuilder sb = new();
                sb.AppendLine(result);
                sb.AppendLine(result2);

                // Display the result.
                AnalysisResult = sb.ToString();
            }
            catch (Exception ex)
            {
                AnalysisResult = $"Error {ex.Message}";
            }
            finally
            {
                IsAnalyzing = false;
            }
        }

        [RelayCommand]
        private async Task CaptureAndAnalyzeImage()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                if (photo == null)
                    return;

                // Display the captured image.
                ImageSource isc = photo.FullPath;
                isImageSelected = true;
                analysisResult = "Analyzing image...";
                isAnalyzing = true;

                // analyze the image;
                using var stream = await photo.OpenReadAsync();
                var result = await visionService.AnalyzeImageAsync(stream);

                analysisResult = result;
            }
            catch (Exception ex)
            {
                analysisResult = $"Error: {ex.Message}";
            }
            finally
            {
                isAnalyzing = false;
            }
        }
    }
}
