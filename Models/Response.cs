using System;
using Newtonsoft.Json;
using System.Collections.Generic;
    
namespace Palota.Assessment.Countries.Models {
    
    public class Response {
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class Country{
        public class Coordinates{
            [JsonProperty("lattitude")]
            public double Lattitude {set; get;}
            [JsonProperty("longitude")]
            public double Longitude {set; get;}
            
            public Coordinates(List<double> coordinates){
                this.Lattitude = coordinates[0];
                this.Longitude = coordinates[1];
            }
            
        }

        [JsonProperty("name")]
        public String Name {get; set;}

        [JsonProperty("iso3Code")]
        public String iso3Code;

        [JsonProperty("alpha3Code")]
        private String Alpha3Code {set { iso3Code = value; }}
        [JsonProperty("capital")]
        public String Capital {get; set;}
        [JsonProperty("subregion")]
        public String Subregion {get; set;}
        [JsonProperty("region")]
        public String Region {get; set;}
        [JsonProperty("population")]
        public int Population {get; set;}
        [JsonProperty("demonym")]
        public String Demonym {get; set;}
        [JsonProperty("nativeName")]
        public String NativeName {get; set;}
        [JsonProperty("numericCode")]
        public String NumericCode {get; set;}
        [JsonProperty("flag")]
        public String Flag {get; set;}

        [JsonProperty("location")]
        public Coordinates Location;
        [JsonProperty("latlng")]
        public List<double> latlng {set {Location = new Coordinates(value);} }

        public List<String> boaders {get; set; }

        [JsonProperty("borders")]
        public List<String> boadersL {set {boaders = value; }}
    }
}