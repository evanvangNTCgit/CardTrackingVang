namespace CardTrackingVang;

public partial class AnalyzeCardImage : ContentPage
{
	public AnalyzeCardImage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Ask for camera permission:
        var cameraPermissionRequest = await Permissions.RequestAsync<Permissions.Camera>();

        if(cameraPermissionRequest != PermissionStatus.Granted) 
        {
            await DisplayAlertAsync("ALERT", "Camera permissions are needed to use the AI image view.\nPlease allow camera permissions for this service", "OK");
        }
    }
}