using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace BeerWebshop.APIClientLibrary.ApiClient.DTO
{
    public class CustomerDTO : AccountDTO
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; } 
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        [JsonPropertyName("age")]
        public int? Age { get; set; }
    }
}
