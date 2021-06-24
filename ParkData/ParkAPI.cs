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
            List<Park> parkInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Park>>(responseContent);

            return parkInfo;
        }

        public async Task<List<Park>> GetParksWhere(string userQuery)
        {
            HttpResponseMessage response = await _httpClient.GetAsync("https://seriouslyfundata.azurewebsites.net/api/parks");
            response.EnsureSuccessStatusCode();

            List<Park> parkOutput = new List<Park>();

            string responseContent = await response.Content.ReadAsStringAsync();
            List<Park> parkInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Park>>(responseContent);

            foreach (Park p in parkInfo)
            {
                if (p.ParkName.ToLower().Contains(userQuery) || p.Description.ToLower().Contains(userQuery))
                {
                    parkOutput.Add(p);
                }
            }

    
            return parkOutput;
        }
    }
}
