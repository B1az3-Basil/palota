using System;
using System.Net.Http;
using Palota.Assessment.Countries.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Palota.Assessment.Countries.ClientHttp{

    public class Client{

        public class CouldNotGetCountries: Exception {
            public CouldNotGetCountries(String message): base(message) {
            }
        }

        public class CountryNotFound : Exception{
            public CountryNotFound(String message): base(message){}
        }

        public class ContinentNotFound : Exception{
            public ContinentNotFound(String message): base(message){}
        }

        private static HttpClient client = new HttpClient();
        private static String BaseUrl = Environment.GetEnvironmentVariable("COUNTRIES_API_URL");
        public static async Task<List<Country>> GetAllCountries()
        {
            HttpResponseMessage response = await client.GetAsync(BaseUrl + "all");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<Country>>();
            }
            
            throw new CouldNotGetCountries("Status code: " + response.StatusCode + " Response Message: " + response.RequestMessage);
        }

        public static async Task<Country> GetCountriesWithIso3code(String iso3Code){
            HttpResponseMessage response = await client.GetAsync(BaseUrl + "/alpha/" + iso3Code);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Country>();
            }
            
            throw new CountryNotFound("The country with ISO 3166 Alpha 3 code '" +iso3Code+"' could not be found.");
        }

        public static async Task<List<Country>> GetCountriesInContinent(String Continent)
        {
            HttpResponseMessage response = await client.GetAsync(BaseUrl + "continent/" + Continent);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<Country>>();
            }
            
            throw new ContinentNotFound("The continent with name '"+Continent+"' could not be found.");
        }
    }
}