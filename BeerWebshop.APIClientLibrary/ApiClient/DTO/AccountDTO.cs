using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BeerWebshop.APIClientLibrary.ApiClient.DTO
{
    public abstract class AccountDTO
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("role")]
        public string Role { get; set; }
        public AccountDTO() { }
    }
}