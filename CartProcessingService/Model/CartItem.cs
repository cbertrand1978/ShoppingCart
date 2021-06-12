using System;
using System.Text.Json.Serialization;

namespace CartProcessingService.Model
{
    /// <summary>
    /// Shopping cart item containing the relevant product details.
    /// </summary>
    public class CartItem
    {
        //public Guid ItemId { get; set; }

        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("TotalPrice")]
        public decimal TotalPrice 
        {
            get
            {
                return this.Product.UnitPrice * this.Quantity;
            }
        }

        [JsonPropertyName("Product")]
        public Product Product { get; set; }
    }
}
