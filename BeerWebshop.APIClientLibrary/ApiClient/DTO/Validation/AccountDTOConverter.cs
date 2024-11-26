using BeerWebshop.APIClientLibrary.ApiClient.DTO;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BeerWebshop.APIClientLibrary.ApiClient.DTO.Validation
{
    public class AccountDTOConverter : JsonConverter<AccountDTO>
    {
        public override AccountDTO Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var rootElement = doc.RootElement;

                if (rootElement.TryGetProperty("role", out var accountType))
                {
                    if (accountType.GetString() == "User")
                    {
                        return JsonSerializer.Deserialize<CustomerDTO>(rootElement.GetRawText(), options);
                    }
                    else if (accountType.GetString() == "Admin")
                    {
                        return JsonSerializer.Deserialize<AdminDTO>(rootElement.GetRawText(), options);
                    }
                }
                throw new JsonException("Unknown account type or missing 'role' property.");
            }
        }

        public override void Write(Utf8JsonWriter writer, AccountDTO value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }

}
