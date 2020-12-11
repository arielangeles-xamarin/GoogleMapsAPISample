using APISample.Helpers;
using MapsAPISample;
using MapsAPISample.Services;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace APISample.ViewModels
{
    public class MapsViewModel : INotifyPropertyChanged
    {
        public ICommand CalculateRouteCommand { get; set; }
        public ICommand UpdatePositionCommand { get; set; }

        public ICommand LoadRouteCommand { get; set; }

        IGoogleMapsAPIService googleMapsApi = new GoogleMapsAPIService();

        public MapsViewModel()
        {
            LoadRouteCommand = new Command(async () => await LoadRoute());
            LoadRouteCommand.Execute(null);
        }

        public async Task LoadRoute()
        {
            int positionIndex = 1;

            var googleDirection = await googleMapsApi.GetDirections("18.48849", "-69.971506", "18.5525209", "-69.4376482");

            if (googleDirection.Routes != null && googleDirection.Routes.Count > 0)
            {
                var positions = Enumerable.ToList(PolylineHelper.Decode(googleDirection.Routes.First().OverviewPolyline.Points));
                CalculateRouteCommand.Execute(positions);

                //Location tracking simulation
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    if (positions.Count > positionIndex)
                    {
                        UpdatePositionCommand.Execute(positions[positionIndex]);
                        positionIndex++;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
            }
            else
                await Application.Current.MainPage.DisplayAlert("No route", "No route found", "Ok");
        }
            

            public event PropertyChangedEventHandler PropertyChanged;
    }
    
}
