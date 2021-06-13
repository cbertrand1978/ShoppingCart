﻿using CartProcessingService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartProcessingService.API.Offers
{
    public class OffersCheck : IOffer<ShoppingCart>
    {
        public void Apply(ShoppingCart item)
        {
            this.ApplesBuyOneGetOneFree(item);
        }

        private void ApplesBuyOneGetOneFree(ShoppingCart cart)
        {
            var apples = cart.CartContents.SingleOrDefault(x => x.Product.Name == ProductConstants.Apple);

            if (apples != null)
            {
                var validDiscounts = Math.DivRem(apples.Quantity, 2, out int remainder);

                if (validDiscounts > 0)
                {
                    cart.Discounts.Add(new Discount() { Amount = validDiscounts * apples.Product.UnitPrice, Description = "Apples - Buy One Get One Free." });
                }
            }
        }
    }
}
