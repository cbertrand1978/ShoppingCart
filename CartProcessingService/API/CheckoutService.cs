using CartProcessingService.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CartProcessingService.API
{
    public class CheckoutService : ICheckoutService
    {
        #region ICheckoutService members

        /// <summary>
        /// Calculates the total based upon the cart contents.
        /// </summary>
        /// <param name="shoppingCart">The <see cref="ShoppingCart"/> to analyse.</param>
        /// <returns>The sum total of the cart contents.</returns>
        public CartServiceResponse GetCartTotal(ShoppingCart shoppingCart)
        {
            // Work on the one fail policy - it's valid until we find an issue.
            var response = new CartServiceResponse();
            var isValid = true;

            isValid = shoppingCart != null;

            if (isValid)
            {
                isValid = isValid && (shoppingCart.CartContents.All(x => x.Quantity > 0));
            }

            if (isValid)
            {
                var runningTotal = shoppingCart.CartContents.Sum(x => x.Product.UnitPrice * x.Quantity);
                shoppingCart.SetCartTotal(runningTotal);
                response.SetResult(shoppingCart);
            }

            if (!isValid)
            {
                response.SetInvalid();
            }

            return response;
        }

        #endregion

        #region IDisposable

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~CheckoutService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
