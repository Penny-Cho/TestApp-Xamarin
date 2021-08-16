using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TestApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {

        IGeolocator locator = CrossGeolocator.Current;

        public MapPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            GetLocation();
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            locator.StopListeningAsync();

        }


        private async void GetLocation()
        {
            var status = await CheckAndRequestLocationPermission();
            if (status == PermissionStatus.Granted)
            {
                var location = await Geolocation.GetLocationAsync();


                locator.PositionChanged += Locator_PositionChanged;
                await locator.StartListeningAsync(new TimeSpan(0, 1, 0), 100);


                locationsMap.IsShowingUser = true;

                //set the default map location to the user's actual location
                CenterMap(location.Latitude, location.Longitude);
            }
        }

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            CenterMap(e.Position.Latitude, e.Position.Longitude);
        }

        private void CenterMap(double latitude, double longitude)
        {
            Xamarin.Forms.Maps.Position center = new Xamarin.Forms.Maps.Position(latitude, longitude);

            //여기서 1, 1은 center의 위치로부터 몇 도나 더 움직인 상태로 보여줄거냐 이걸 의미함.
            MapSpan span = new MapSpan(center, 1, 1);
            locationsMap.MoveToRegion(span);
        }


        //Getting the permission
        private async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                //promt the user to permit using the location
                return status;
            }

            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            return status;
        }
    }
}



