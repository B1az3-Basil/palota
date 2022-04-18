using System;
using System.Collections.Generic;
using Palota.Assessment.Countries.Models;
using Palota.Assessment.Countries.ClientHttp;
using System.Threading.Tasks;

namespace Palota.Assessment.Countries.Controllers
{
    public class CountriesController
    {
        private List<Country> Countries = new List<Country>();
        public async Task<List<Country>> GetCountries()
        {
            this.Countries = await Client.GetAllCountries();
            return this.Countries;
        }

        public async Task<List<Country>> GetBoaderWithIso3codeCountries(string iso3Code){
            Country[] countries1 = {await Client.GetCountriesWithIso3code(iso3Code)};
            List<Country> countries = FilterByBoaders(new List<Country>(countries1));
            return countries;
        }

        public async Task<Country> GetCountryWithIso3code(String iso3Code){
            return await Client.GetCountriesWithIso3code(iso3Code);
        }

        public async Task<List<Country>> GetCountriesInContinent(String Continent){
            return await Client.GetCountriesInContinent(Continent);
        }


        private List<Country> FilterByBoaders(List<Country> countriesParam){
            List<Country> countries = new List<Country>();
            foreach (Country country in countriesParam)
            {
                if (country.boaders != null && country.boaders.Count > 0) countries.Add(country);
            }
            return countries;
        }
    }
}