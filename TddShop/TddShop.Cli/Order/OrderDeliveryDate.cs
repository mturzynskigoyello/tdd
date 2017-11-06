using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddShop.Cli.Order.Models;
using TddShop.Cli.Order.Repositories;

namespace TddShop.Cli.Order
{
    public class OrderDeliveryDate
    {
        public OrderModel Order { get; private set; }

        public IStockRepository StockRepository { get; set; }

        public OrderDeliveryDate(OrderModel order)
        {
            Order = order;
        }

        /// <summary>
        /// Possible rules
        ///     * Minimum delivery date is 3 working days
        ///     * If ordered sooner than 13:00 count today as first day (assuming it's a working day)
        ///     * If there are no enough items in stock add 5 working days 
        ///         (IStockRepository - that is what you need, you can find it under Repositories. PS - we don't need implementation of it, right?)
        ///     * Orders worth more than $500 needs approval from manager - add 2 more days
        ///     * Manager doesn't work on Fridays
        ///     
        /// We will need to add an overload which takes current date as a parameter 
        /// to make it independent from DateTime.Now
        /// </summary>
        /// <returns></returns>
        public DateTime GetEstimatedDelivery()
        {
            return GetEstimatedDelivery(DateTime.Now);
        }

        public DateTime GetEstimatedDelivery(DateTime orderDate)
        {
            var deliveryDate = orderDate.AddDays(3).Date;
            if (orderDate.TimeOfDay < TimeSpan.FromHours(13))
            {
                deliveryDate = deliveryDate.AddDays(-1);
            }

            var allItemsAvailable = Order.Items.All(x => StockRepository.GetAvailableItemsByName(x.Name) > x.Quantity);
            if (!allItemsAvailable)
            {
                deliveryDate = deliveryDate.AddDays(5);
            }

            var orderValue = Order.Items.Sum(x => x.Price * x.Quantity);
            if (orderValue > 500)
            {
                deliveryDate = deliveryDate.AddDays(2);
            }

            if (deliveryDate.DayOfWeek == DayOfWeek.Sunday)
            {
                deliveryDate = deliveryDate.AddDays(1);
            }

            var date = deliveryDate.Date;
            while (date >= orderDate)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                {
                    deliveryDate = deliveryDate.AddDays(1);
                }
                date = date.AddDays(-1);
            }

            if (deliveryDate.DayOfWeek == DayOfWeek.Sunday)
            {
                deliveryDate = deliveryDate.AddDays(1);
            }

            return deliveryDate;
        }
    }
}
