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
    }
}
