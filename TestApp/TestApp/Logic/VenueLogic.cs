using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestApp.Model;

namespace TestApp.Logic
{
    public class VenueLogic
    {
        public async static Task<List<Venue>> GetVenues(double latitude, double longitude)
        {
            List<Venue> venues = new List<Venue>();

            //var url = VenueRoot.GenerateURL(latitude, longitude);
            var url = "https://wpf-example-venues.s3.us-east-1.amazonaws.com/new-york.json";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(json);

                venues = venueRoot.response.venues as List<Venue>;
            }


            return venues;
        }

    }
}
