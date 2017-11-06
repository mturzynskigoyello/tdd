using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddShop.Cli.Order.Models;

namespace TddShop.Cli.Order
{
    public class OrderSummaryBuilder
    {
        /// <summary>
        /// Returns order summary in format: {number of items} item(s) with total value of {total value}
        /// Please use "item" or "items" in result string accordingly to actual number of bought items.
        /// In case order has no items the result string should be "There are no items in the order"
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public string GetSummary(OrderModel order)
        {
            var itemsCount = order.Items.Sum(x => x.Quantity);

            if (itemsCount == 0)
            {
                return "There are no items in the order";
            }

            var summary = $"{itemsCount} item";
            if (itemsCount > 1)
            {
                summary += "s";
            }

            var totalValue = order.Items.Sum(x => x.Price * x.Quantity);
            summary += $" with total value of EUR {totalValue}";

            return summary;
        }
    }
}
