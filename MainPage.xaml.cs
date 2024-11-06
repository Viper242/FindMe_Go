namespace FindMe_GO
{
    public partial class MainPage : ContentPage
    {
        string _baseUrl = "https://bing.com/maps/default.aspx?cp=";
        public string Username { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnFindMeClicked(object sender, EventArgs e)
        {
            var permission = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (permission == PermissionStatus.Granted)
            {
                ShareLocation();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Location Permission", "Location permission is required to share your location", "OK");
            }

            var request = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

            if(request == PermissionStatus.Granted)
            {
                ShareLocation();
            }
            else
            {
                if(DeviceInfo.Platform == DevicePlatform.iOS || 
                    DeviceInfo.Platform == DevicePlatform.MacCatalyst)
                {
                    await App.Current.MainPage.DisplayAlert("Location required", "Location permission is required to share your location", "OK");
                } else
                {
                    await App.Current.MainPage.DisplayAlert("Location required", "Location permission is required to share your location. We'll ask again next time.", "OK");
                }
                
            }

        }
        
        private async Task ShareLocation() 
        { 
            Username = UsernameEntry.Text;
            var locationRequest = new GeolocationRequest(GeolocationAccuracy.Best);
            var location = await Geolocation.GetLocationAsync(locationRequest);

            await Share.RequestAsync(new ShareTextRequest
            {
                Subject = "Find me!",
                Title = "Find me!",
                Text = $"{Username} is sharing their location with you",
                Uri = $"{_baseUrl}{location.Latitude}~{location.Longitude}",
                
            });
        }
    }

}
