using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;


namespace ParkData
{
    public class ParkAPI
    {


        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly IMemoryCache memoryCache;


        /*    public ParkAPI(IMemoryCache memoryCache)
            {

                _cache = memoryCache;
            }
    */

        public async Task<List<Park>> GetParks()
        {
            const string Key = "park";
            List<Park> cacheValue;

            if (!this.memoryCache.TryGetValue(Key, out cacheValue))
            {

                HttpResponseMessage response = await _httpClient.GetAsync("https://seriouslyfundata.azurewebsites.net/api/parks");
                response.EnsureSuccessStatusCode();

                string responseContent = await response.Content.ReadAsStringAsync();
                cacheValue = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Park>>(responseContent);

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(20));

                this.memoryCache.Set(Key, cacheValue, cacheEntryOptions);

            }
            return cacheValue;
        }



        /*        public async Task<List<Park>> GetParks()
                {
                    HttpResponseMessage response = await _httpClient.GetAsync("https://seriouslyfundata.azurewebsites.net/api/parks");
                    response.EnsureSuccessStatusCode();

                    string responseContent = await response.Content.ReadAsStringAsync();
                    List<Park> parkInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Park>>(responseContent);

                    return parkInfo;
                }
        */


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
