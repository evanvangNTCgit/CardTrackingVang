using CardTrackingVang.Models;
using CardTrackingVang.ViewModel;
using Syncfusion.Maui.Maps;

namespace CardTrackingVang;

public partial class GeolocationGPS : ContentPage
{
    private readonly MarkerViewModel markerViewModel = new();

    public GeolocationGPS()
	{
		InitializeComponent();
        this.BindingContext = markerViewModel;
        this.tileLayer.Markers = markerViewModel.MarkerModels;
    }

    private async void LocateBTN_Clicked(object sender, EventArgs e)
    {
        try
        {
            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

            if (status != PermissionStatus.Granted)
            {
                return;
            }

            Location? location = await Geolocation.GetLocationAsync(new GeolocationRequest
            {
                DesiredAccuracy = GeolocationAccuracy.High,
                Timeout = TimeSpan.FromSeconds(30)
            });

            if (location != null && this.tileLayer != null)
            {
                this.tileLayer.Center = new MapLatLng(location.Latitude, location.Longitude);

                var userPastLocation = this.markerViewModel.MarkerModels.FirstOrDefault(m => m.Label == "Current Location");

                if (userPastLocation != null)
                {
                    this.markerViewModel.RemoveMarker(userPastLocation);
                }

                this.markerViewModel.AddMarker(new MarkerModel() { Label = "Current Location", Latitude = location.Latitude, Longitude = location.Longitude });

                this.tileLayer.ZoomPanBehavior = new MapZoomPanBehavior() { ZoomLevel = 10 };
            }
        }
        catch
        {
            await DisplayAlertAsync("ALERT", "Failed to get current location. Please make sure permission are enabled and try again.", "OK");
        }
    }
}