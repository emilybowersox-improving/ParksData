using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace ParkData
{
    public class ParkAPI
    {

        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<List<Park>> GetParks()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://seriouslyfundata.azurewebsites.net/api/parks");
            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();
            var parkInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Park>>(responseContent);

            return parkInfo;
        }

        public async Task<List<Park>> GetParksWhere(string userQuery)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://seriouslyfundata.azurewebsites.net/api/parks");
            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();
            var parkInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Park>>(responseContent);

            var parkOutput = new List<Park>();

            foreach (var park in parkInfo)
            {
                if (park.ParkName.Contains(userQuery))
                {
                    parkOutput.Add(park);
                }
            }

            return parkOutput;
        }
    }
}


/*var output = new List<Airport>();

foreach (var airport in airports)
{
    if (airport.Name.Contains("Heliport"))
    {
        output.Add(airport);
    }
}

return output;*/