using System;
using System.Text.Json.Serialization;

namespace CartProcessingService.Model
{
    public class Discount
    {
        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("Amount")]
        public decimal Amount { get; set; }

    }
}
