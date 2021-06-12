
using System.Text.Json.Serialization;

namespace CartProcessingService.Model
{
    /// <summary>
    /// A product - a saleable item.
    /// </summary>
    public class Product
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Description")]
        public string Description { get; set; }

        [JsonPropertyName("UnitPrice")]
        public decimal UnitPrice { get; set; }

    }
}
