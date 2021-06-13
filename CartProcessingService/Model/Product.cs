
using System.Text.Json.Serialization;

namespace CartProcessingService.Model
{
    /// <summary>
    /// A product - a saleable item.
    /// </summary>
    public class Product
    {
        [JsonPropertyName("Id")]
        public virtual int Id { get; set; }

        [JsonPropertyName("Name")]
        public virtual string Name { get; set; }

        [JsonPropertyName("Description")]
        public virtual string Description { get; set; }

        [JsonPropertyName("UnitPrice")]
        public virtual decimal UnitPrice { get; set; }

    }
}
