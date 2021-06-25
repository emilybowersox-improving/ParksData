using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using ParkData.Models;

namespace ParkData
{
    public class ParkAPI
    {

        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly IMemoryCache _cache;

        public ParkAPI(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        private string GetParkResponse()
        {
            HttpResponseMessage response = _httpClient.GetAsync("https://seriouslyfundata.azurewebsites.net/api/parks").Result;
            response.EnsureSuccessStatusCode();
            string responseContent = response.Content.ReadAsStringAsync().Result;
           
            return responseContent;
        }

        public List<Park> CheckCache()
        { 
            List<Park> parksFromCache;
          /*  "Park" = CacheKey.Entry */
            if (!_cache.TryGetValue("Park", out parksFromCache))
            {
                string parkResposne = GetParkResponse();
                parksFromCache = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Park>>(parkResposne);

                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(20));
                _cache.Set("Park", parksFromCache, cacheEntryOptions);
            }
            return parksFromCache;
        }


        public List<Park> GetParksWhere(string userQuery)
        {
            List<Park> parkOutput = new List<Park>();

            List<Park> parkInfo = CheckCache();

            foreach (Park p in parkInfo)
            {
                if (p.ParkName.ToLower().Contains(userQuery.ToLower()) || p.Description.ToLower().Contains(userQuery.ToLower()))
                {
                    parkOutput.Add(p);
                }
            }
            return parkOutput;
        }

    }
}


