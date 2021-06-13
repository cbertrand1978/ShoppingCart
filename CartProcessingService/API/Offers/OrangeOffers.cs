using CartProcessingService.Model;
using System;
using System.Linq;

namespace CartProcessingService.API.Offers
{
    public class OrangeOffers : IOffer<ShoppingCart>
    {
        public void Apply(ShoppingCart item)
        {
            this.OrangesBuyTwoGetThirdFree(item);
        }

        private void OrangesBuyTwoGetThirdFree(ShoppingCart cart)
        {
            var oranges = cart.CartContents.SingleOrDefault(x => x.Product.Name == ProductConstants.Orange);

            if (oranges != null)
            {
                var validDiscounts = Math.DivRem(oranges.Quantity, 3, out int remainder);

                if (validDiscounts > 0)
                {
                    cart.Discounts.Add(new Discount() { Amount = validDiscounts * oranges.Product.UnitPrice, Description = "oranges - Buy Two Get Third Free." });
                }
            }
        }
    }
}
