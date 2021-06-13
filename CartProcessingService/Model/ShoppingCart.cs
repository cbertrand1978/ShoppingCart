using CartProcessingService.API.Offers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace CartProcessingService.Model
{
    /// <summary>
    /// A shopping cart - allows a customer to add items to their basket for purchase.
    /// </summary>
    public class ShoppingCart : IVisitor<ShoppingCart>
    {
        [JsonPropertyName("CartContents")]
        public List<CartItem> CartContents { get; set; }


        [JsonPropertyName("Discounts")]
        public List<Discount> Discounts { get; set; }

        [JsonPropertyName("CartTotal")]
        public decimal CartTotal { get; set; }

        [JsonPropertyName("SubTotal")]
        public decimal SubTotal { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ShoppingCart()
        {
            this.CartContents = new List<CartItem>();
            this.Discounts = new List<Discount>();
        }

        /// <summary>
        /// Add a <paramref name="product"/> to the cart.  If it is already present, then increase the quantity as specified by
        /// <paramref name="quantity"/>.
        /// </summary>
        /// <param name="product">The product to add to the cart.</param>
        /// <param name="quantity">The total quantity to add.</param>
        public void AddItemToCart(Product product, int quantity)
        {
            if (product != null)
            {
                var cartItem = this.CartContents.SingleOrDefault(c => c.Product.Id.Equals(product.Id));

                if (cartItem == null)
                {
                    cartItem = new CartItem()
                    {
                        Product = product,
                        Quantity = quantity
                    };

                    this.CartContents.Add(cartItem);
                }
                else
                {
                    cartItem.Quantity += quantity;
                }
            }
        }

        /// <summary>
        /// Removes an item from the cart.  If the quantity present minus <paramref name="quantity"/> is zero or less, the item is removed completely.
        /// Otherwise the quantity is decreased.
        /// </summary>
        /// <param name="product">The product to remove from the cart.</param>
        /// <param name="quantity">The total quantity to remove.</param>
        public void RemoveItemFromCart(Product product, int quantity)
        {
            var cartItem = this.CartContents.SingleOrDefault(c => c.Product.Id.Equals(product.Id));

            if (cartItem != null)
            {
                if (cartItem.Quantity - quantity <= 0)
                {
                    this.CartContents.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity -= quantity;
                }
            }
        }

        public void SetCartSubTotal(decimal cartSubTotal)
        {
            this.SubTotal = cartSubTotal;
        }

        public void SetCartTotal(decimal cartTotal)
        {
            this.CartTotal = cartTotal;
        }

        /// <summary>
        /// Apply any offers to the cart.
        /// </summary>
        /// <param name="offer">The offer processor to use.</param>
        public void Visit(IOffer<ShoppingCart> offer)
        {
            offer.Apply(this);
        }
    }
}
