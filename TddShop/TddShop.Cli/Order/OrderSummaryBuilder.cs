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
        /// Please use "item" or "items" in result string accordingly to actual number of bought items
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public string GetSummary(OrderModel order)
        {
            throw new NotImplementedException();
        }
    }
}
