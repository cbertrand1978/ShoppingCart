

using System.Text.Json.Serialization;

namespace CartProcessingService.Model
{
    public class CartServiceResponse
    {
        [JsonPropertyName("IsValid")]
        public bool IsValid { get; set; }

        [JsonPropertyName("IsSuccessful")]
        public bool IsSuccessful { get; set; }

        [JsonPropertyName("Result")]
        public ShoppingCart Result { get; set; }

        public CartServiceResponse()
        {
            this.IsValid = true;
        }

        public void SetInvalid()
        {
            this.IsValid = false;
        }

        public void SetResult(ShoppingCart cart)
        {
            this.Result = cart;
        }
    }
}
