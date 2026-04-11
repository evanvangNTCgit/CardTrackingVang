using Syncfusion.Maui.Maps;
using System.Collections.ObjectModel;

namespace CardTrackingVang.ViewModel
{
    public class MarkerViewModel
    {
        public ObservableCollection<MapMarker> MarkerModels { get; set; }

        public MarkerViewModel()
        {
            this.MarkerModels = new();
            this.MarkerModels.Add(new MapMarker() { Latitude = 44.933750, Longitude = -89.615891 });
        }
    }
}
