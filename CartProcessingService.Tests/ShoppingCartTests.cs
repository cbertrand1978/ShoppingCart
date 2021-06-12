using NUnit.Framework;
using CartProcessingService.API;
using CartProcessingService.Model;
using System.Linq;


namespace CartProcessingService.Tests
{
    public class ShoppingCartTests
    {
        public ShoppingCart Target { get; set; }


        [SetUp]
        public void Setup()
        {
            this.Target = new ShoppingCart();
        }

        private Product GetProduct()
        {
            return new Product() { Description = "Test", Id = 1, Name = "Test Product", UnitPrice = 1.50M };
        }

        [Test]
        public void AddItemToCart_IgnoresNullProduct()
        {
            Target.AddItemToCart(null, 1);
            Assert.AreEqual(0, Target.CartContents.Count, "Cart should have no items.");
        }

        [Test]
        public void RemoveItemFromCart_IgnoresMissingProduct()
        {
            Target.AddItemToCart(this.GetProduct(), 1);
            var testProduct = new Product() { Description = "Test2", Id = 2, Name = "Test Product 2", UnitPrice = 2.50M };

            Target.RemoveItemFromCart(testProduct, 1);
            Assert.AreEqual(1, Target.CartContents.Count, "Cart should have original item.");
        }

        [Test]
        public void RemoveItemFromCart_RemovesCorrectProductQuantity()
        {
            var product = this.GetProduct();
            Target.AddItemToCart(product, 2);

            Target.RemoveItemFromCart(product, 1);
            Assert.AreEqual(1, Target.CartContents.Count, "Cart should have 1 original item.");
        }

        [Test]
        public void RemoveItemFromCart_RemovesProductWhenZero()
        {
            var product = this.GetProduct();
            Target.AddItemToCart(product, 2);

            Target.RemoveItemFromCart(product, 2);
            Assert.AreEqual(0, Target.CartContents.Count, "Cart should have no original items.");
        }
    }
}
