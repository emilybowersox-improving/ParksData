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

        public async Task<string> GetParkName()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://seriouslyfundata.azurewebsites.net/api/parks");
            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();
            var seleucidResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ParkResponse>(responseContent);



     /*       return */


        }
    }
}
