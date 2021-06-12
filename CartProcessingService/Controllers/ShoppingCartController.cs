using CartProcessingService.API;
using CartProcessingService.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CartProcessingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private ICheckoutService CheckoutService { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="checkoutService">Implementation of <see cref="ICheckoutService"/> to use for processing carts.</param>
        public ShoppingCartController(ICheckoutService checkoutService)
        {
            this.CheckoutService = checkoutService;
        }

        [HttpGet]
        public string Get()
        {
            return "Shopping Cart Service is available.";
        }

        /// <summary>
        /// Processes a <see cref="ShoppingCart"/> and returns the response.
        /// </summary>
        /// <param name="shoppingCart">The <see cref="ShoppingCart"/> to response.</param>
        /// <returns>A <see cref="CartServiceResponse"/> containing the outcome of the processing.</returns>
        [HttpPost]
        [ActionName("ProcessCart")]
        public CartServiceResponse ProcessCart(ShoppingCart shoppingCart)
        {
            var response = this.CheckoutService.GetCartTotal(shoppingCart);
            response.IsSuccessful = true;
            return response;
        }
    }
}
