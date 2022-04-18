using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Palota.Assessment.Countries.Models;
using Palota.Assessment.Countries.Controllers;
using System.Threading.Tasks;
using Palota.Assessment.Countries.ClientHttp;

namespace Palota.Assessment.Countries.Functions
{
    public static class Ping
    {
        [FunctionName("Ping")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# Palota HTTP trigger function processed a request.");

            return new OkObjectResult(new Response
            {
                Message = "Pong from Palota"
            });
        }
    }

    public static class Countries{
        [FunctionName("Countries")]
        public async static Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Getting all Countries.");
            return new OkObjectResult(await new CountriesController().GetCountries());
        }
    }

    public static class CountriesIso3Code{
        [FunctionName("CountriesWithIso3code")]
        public async static Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "countries/{iso3Code}")] HttpRequest req,
            String iso3Code, ILogger log)
        {
            log.LogInformation("Getting countries with the iso3code " +iso3Code+"." );
            try
            {
                return new OkObjectResult(await new CountriesController().GetCountryWithIso3code(iso3Code));
            }
            catch (Client.CountryNotFound e)
            {
                log.LogError(e.Message);
                return new NotFoundObjectResult(new Response
                {
                   Message = e.Message
                });
            }
        }
    }

    public static class CountriesIso3CodeBoader{
        [FunctionName("CountriesIso3CodeBoader")]
        public async static Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "countries/{iso3Code}/borders")] HttpRequest req,
            String iso3Code ,ILogger log)
        {
            log.LogInformation("Getting countries with the iso3code " +iso3Code+" around the boader." );
            try
            {
                return new OkObjectResult(await new CountriesController().GetBoaderWithIso3codeCountries(iso3Code));
            }
            catch (Client.CountryNotFound e)
            {
                log.LogError(e.Message);
                return new NotFoundObjectResult(new Response
                {
                   Message = e.Message
                });
            }
            
        }
    }

    public static class Continents{
        [FunctionName("Continents")]
        public async static Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "continents/{continentName}/countries")] HttpRequest req,
            String continentName ,ILogger log)
        {
            log.LogInformation("Getting countries in the coutinent " +continentName+"." );
            
            try
            {
                return new OkObjectResult(await new CountriesController().GetCountriesInContinent(continentName));
            }
            catch (Client.ContinentNotFound e)
            {
                log.LogError(e.Message);
                return new NotFoundObjectResult(new Response
                {
                   Message = e.Message
                });
            }
        }
    }
}
