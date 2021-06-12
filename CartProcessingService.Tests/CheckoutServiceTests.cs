using NUnit.Framework;
using CartProcessingService.API;
using CartProcessingService.Model;
using System.Linq;

namespace CartProcessingService.Tests
{
    public class CheckoutServiceTests
    {
        public CheckoutService Target { get; set; }


        [SetUp]
        public void Setup()
        {
            this.Target = new CheckoutService();
        }

        private ShoppingCart GetCart()
        {
            var cart =new ShoppingCart();
            cart.AddItemToCart(new Product() { Description = "Test", Id = 1, Name = "Test Product", UnitPrice = 1.50M }, 2);
            return cart;
        }

        [Test]
        public void GetCartTotal_RejectsNullCart()
        {
            var response = Target.GetCartTotal(null);
            Assert.IsFalse(response.IsValid, "Null cart should not return a valid response.");
        }

        [Test]
        public void GetCartTotal_RejectsMinusQuantities()
        {
            var cart = new ShoppingCart();
            var product = new Product()
            {
                Description = "Test",
                Id = 1,
                Name = "Test",
                UnitPrice = 1.50M
            };
            cart.AddItemToCart(product, -1);
            var response = Target.GetCartTotal(cart);
            Assert.IsFalse(response.IsValid, "Minus quantities should not return a valid response.");
        }

        [Test]
        public void GetCartTotal_CalculatesTotalCorrectly()
        {
            var cart = this.GetCart();
            var response = Target.GetCartTotal(cart);
            Assert.IsTrue(response.IsValid, "Cart contents should be valid.");
            var expectedResult = cart.CartContents.Sum(x => x.Product.UnitPrice * x.Quantity);
            Assert.AreEqual(expectedResult, response.Result.CartTotal, $"Total {response.Result.CartTotal} should match expected {expectedResult}.");
        }
    }
}