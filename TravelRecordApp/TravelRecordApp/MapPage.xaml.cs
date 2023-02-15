﻿using Plugin.Geolocator;
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

namespace TravelRecordApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
        IGeolocator locator = CrossGeolocator.Current;
        public MapPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

			GetLocation();
        }

        protected override void OnDisappearing()
        {
			base.OnDisappearing();

        }

        private async void GetLocation()
		{
			var status = await CheckAndRequestLocationPermission();

			if(status == PermissionStatus.Granted)
			{
				var location = await Geolocation.GetLocationAsync();
				
                locator.PositionChanged += Locator_PositionChanged;
				await locator.StartListeningAsync(new TimeSpan(0, 1, 0), 100);

				locationsMap.IsShowingUser = true;

				CenterMap(location.Latitude, location.Longitude);
			}
		}

        private void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            CenterMap(e.Position.Longitude, e.Position.Latitude);
        }

        private void CenterMap(double latitude, double longitude)
        {
            Xamarin.Forms.Maps.Position center = new Xamarin.Forms.Maps.Position(latitude, longitude);
			MapSpan span = new MapSpan(center, 1, 1);
			locationsMap.MoveToRegion(span);
        }

        private async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
			var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
			if (status == PermissionStatus.Granted)
			{
				return status;
			}

			if(status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
			{
				return status;
			}

			status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
			return status;
        }
    }
}