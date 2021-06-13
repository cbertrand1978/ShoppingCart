using NUnit.Framework;
using CartProcessingService.Model;
using System.Linq;
using CartProcessingService.API.Offers;

namespace CartProcessingService.Tests
{
    public class OrangeOffersTests
    {
        public OrangeOffers Target { get; set; }

        [SetUp]
        public void Setup()
        {
            this.Target = new OrangeOffers();
        }

        private ShoppingCart GetCart(int quantity)
        {
            var cart = new ShoppingCart();
            cart.AddItemToCart(new Product() { Description = "Test", Id = 1, Name = ProductConstants.Orange, UnitPrice = ProductConstants.OrangeCost }, quantity);
            return cart;
        }

        [Test]
        public void Apply_OneOrange_OfferNotApplied()
        {
            var cart = this.GetCart(1);
            Target.Apply(cart);

            Assert.AreEqual(0, cart.Discounts.Sum(x => x.Amount), "No discount should be applied.");
        }

        [Test]
        public void Apply_TwoOranges_OfferNotApplied()
        {
            var cart = this.GetCart(2);
            Target.Apply(cart);

            Assert.AreEqual(0, cart.Discounts.Sum(x => x.Amount), "No discount should be applied.");
        }
        [Test]
        public void Apply_ThreeOranges_OfferApplied()
        {
            var cart = this.GetCart(3);
            Target.Apply(cart);

            Assert.AreEqual(ProductConstants.OrangeCost * 1, cart.Discounts.Sum(x => x.Amount), "Discount for one free apple should be applied.");
        }

        [Test]
        public void Apply_FourOranges_OfferApplied()
        {
            var cart = this.GetCart(4);
            Target.Apply(cart);

            Assert.AreEqual(ProductConstants.OrangeCost * 1, cart.Discounts.Sum(x => x.Amount), "Discount for one free orange should be applied.");
        }

        [Test]
        public void Apply_SixOranges_OfferApplied()
        {
            var cart = this.GetCart(6);
            Target.Apply(cart);

            Assert.AreEqual(ProductConstants.OrangeCost * 2, cart.Discounts.Sum(x => x.Amount), "Discount for two free oranges should be applied.");
        }
    }
}
