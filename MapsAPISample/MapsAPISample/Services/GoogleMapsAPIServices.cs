using MapsAPISample.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MapsAPISample.Services
{
    public class GoogleMapsAPIService : IGoogleMapsAPIService
    {
        private static string googleMapsAPIKey;
        private const string APIBaseAddress = "https://maps.googleapis.com/maps/";
        private const string mediaType = "application/json";

        public static void Initialize(string googleMapsKey) => googleMapsAPIKey = googleMapsKey;

        public async Task<GoogleDirection> GetDirections(string originLatitude, string originLongitude, string destinationLatitude, string destinationLongitude)
        {
            GoogleDirection googleDirection = new GoogleDirection();

            var httpClient = new HttpClient() { BaseAddress = new Uri(APIBaseAddress) };

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));

            var response = await httpClient.GetAsync($"api/directions/json?mode=driving&transit_routing_preference=less_driving&origin={originLatitude},{originLongitude}&destination={destinationLatitude},{destinationLongitude}&key={googleMapsAPIKey}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    googleDirection = JsonConvert.DeserializeObject<GoogleDirection>(json);
                } 
            }

            return googleDirection;
        }


        
    }
}
