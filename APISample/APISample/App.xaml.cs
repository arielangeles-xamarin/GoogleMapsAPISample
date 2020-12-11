using APISample.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace APISample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            GoogleMapsAPIService.Initialize(Constants.GoogleMapsApiKey);
            MainPage = new MapsPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
