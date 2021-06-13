using NUnit.Framework;
using CartProcessingService.Model;
using System.Linq;
using CartProcessingService.API.Offers;

namespace CartProcessingService.Tests
{
    public class AppleOffersTests
    {
        public AppleOffers Target { get; set; }

        [SetUp]
        public void Setup()
        {
            this.Target = new AppleOffers();
        }

        private ShoppingCart GetCart(int quantity)
        {
            var cart = new ShoppingCart();
            cart.AddItemToCart(new Product() { Description = "Test", Id = 1, Name = ProductConstants.Apple, UnitPrice = ProductConstants.AppleCost }, quantity);
            return cart;
        }

        [Test]
        public void Apply_OneApple_OfferNotApplied()
        {
            var cart = this.GetCart(1);
            Target.Apply(cart);

            Assert.AreEqual(0, cart.Discounts.Sum(x => x.Amount), "No discount should be applied.");
        }

        [Test]
        public void Apply_TwoApples_OfferApplied()
        {
            var cart = this.GetCart(2);
            Target.Apply(cart);

            Assert.AreEqual(ProductConstants.AppleCost * 1, cart.Discounts.Sum(x => x.Amount), "Discount for one free apple should be applied.");
        }
        [Test]
        public void Apply_ThreeApples_OfferApplied()
        {
            var cart = this.GetCart(3);
            Target.Apply(cart);

            Assert.AreEqual(ProductConstants.AppleCost * 1, cart.Discounts.Sum(x => x.Amount), "Discount for one free apple should be applied.");
        }

        [Test]
        public void Apply_FourApples_OfferApplied()
        {
            var cart = this.GetCart(4);
            Target.Apply(cart);

            Assert.AreEqual(ProductConstants.AppleCost * 2, cart.Discounts.Sum(x => x.Amount), "Discount for two free apples should be applied.");
        }
    }
}
