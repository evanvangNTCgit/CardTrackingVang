using CardTrackingVang.Models;
using Syncfusion.Maui.Maps;
using System.Collections.ObjectModel;

namespace CardTrackingVang.ViewModel
{
    public class MarkerViewModel
    {
        public ObservableCollection<MarkerModel> MarkerModels { get; set; }

        public MarkerViewModel()
        {
            this.MarkerModels = new();
            this.MarkerModels.Add(new MarkerModel() {Label = "USA", Latitude = 44.933750, Longitude = -89.615891 });
        }

        public void AddMarker (MarkerModel m) 
        {
            this.MarkerModels.Add(m);
        }

        public void RemoveMarker(MarkerModel m) 
        {
            this.MarkerModels.Remove(m);
        }
    }
}
