using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddShop.Cli.Order.Models;

namespace TddShop.Cli.Order
{
    public class DiscountCalculator
    {
        /// <summary>
        /// Possible rules:
        ///     * Total order value > $500
        ///     * Items from defined categories are discounted
        ///     * Customer who has loyalty card
        ///     * Discounts based on dates
        ///     * RANDOM discounts in some cases
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public decimal GetDiscount(OrderModel order)
        {
            throw new NotImplementedException();
        }
    }
}
