using CardTrackingVang.AiServices;
using CardTrackingVang.ViewModel;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Security.Cryptography;

namespace CardTrackingVang;

public partial class AnalyzeCardImage : ContentPage
{
    private readonly ComputerVisionService _computerVisionService;
    private readonly ImageRecognitionViewModel _imageRecognitionViewModel;
    public bool Loading = false;
    public AnalyzeCardImage(ICameraProvider camProvider, ComputerVisionService cvs)
	{
        InitializeComponent();
        this._computerVisionService = cvs;
        this._imageRecognitionViewModel = new(cvs);
        this.BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Ask for camera permission:
        var cameraPermissionRequest = await Permissions.RequestAsync<Permissions.Camera>();

        if (cameraPermissionRequest != PermissionStatus.Granted)
        {
            await DisplayAlertAsync("ALERT", "Camera permissions are needed to use the AI image view.\nPlease allow camera permissions for this service", "OK");
        } 
        }

    private async void Capture_Clicked(object sender, EventArgs e)
    {
        try
        {
            this.Loading = true;

            var picStream = await this.UserCamera.CaptureImage(CancellationToken.None);

            if (picStream != null)
            {
                //var ai = await this._computerVisionService.AnalyzeImageAsync(picStream);
                // await DisplayAlertAsync("Info", $"{ai}", "OK");

                await Task.Delay(10000);
            }
            else
            {
                await DisplayAlertAsync("ALERT", "Failed to see image taken\nPlease try again!", "OK");
                return;
            }
        }
        catch (Exception ex) 
        {
            await DisplayAlertAsync("ALERT", $"Error occured processing image please try again\n{ex.Message}", "OK");
        }
        finally
        {
            this.Loading = false;
        }
        }

    private async void SelectImageBTN_Clicked(object sender, EventArgs e)
    {
        var result = await this._imageRecognitionViewModel.PickAndAnalyzeImage();
        if (string.IsNullOrWhiteSpace(result))
        {
            return;
        }

        Console.WriteLine(result);
    }
}