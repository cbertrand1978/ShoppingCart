using CartProcessingService.Model;
using System;

namespace CartProcessingService.API
{
    /// <summary>
    /// API for a checkout service - used to process shopping carts.
    /// </summary>
    public interface ICheckoutService : IDisposable
    {
        /// <summary>
        /// Calculates the total based upon the cart contents.
        /// </summary>
        /// <param name="shoppingCart">The <see cref="ShoppingCart"/> to analyse.</param>
        /// <returns>The sum total of the cart contents.</returns>
        CartServiceResponse GetCartTotal(ShoppingCart shoppingCart);
    }
}
