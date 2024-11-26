using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BeerWebshop.APIClientLibrary.ApiClient.DTO
{
    public class AdminDTO : AccountDTO
    {
        [JsonPropertyName("permissionLevel")]
        public int PermissionLevel { get; set; }
    }
}