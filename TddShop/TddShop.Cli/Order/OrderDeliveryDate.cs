using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddShop.Cli.Order.Models;

namespace TddShop.Cli.Order
{
    public class OrderDeliveryDate
    {
        public OrderModel Order { get; private set; }

        public OrderDeliveryDate(OrderModel order)
        {
            Order = order;
        }

        /// <summary>
        /// Possible rules
        ///     * Minimum delivery date is 3 working days
        ///     * If ordered sooner than 13:00 count today as first day (assuming it's a working day)
        ///     * If there are no enough items in stock add 5 working days
        ///     * Orders worth more than $500 needs approval from manager - add 2 more days
        ///     * Manager doesn't work on Fridays
        ///     
        /// We will need to add an overload which takes current date as a parameter 
        /// to make it independent from DateTime.Now
        /// </summary>
        /// <returns></returns>
        public DateTime GetEstimatedDelivery()
        {
            return DateTime.Now.AddDays(7);
        }
    }
}
