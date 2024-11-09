using Microsoft.Maui.Maps;
using Microsoft.Maui.Controls.Maps;

namespace FindMe_GO
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
        }

        public MapPage(Location location) : this()
        {
            Pin locationPin = new Pin
            {
                Label = "Current Location",
                Address = "Current Location",
                Type = PinType.Place,
                Location = new Location(location.Latitude, location.Longitude)
            };
            bingMap.Pins.Add(locationPin);
            bingMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(location.Latitude, location.Longitude), Distance.FromMiles(1)));
        }
    }
}