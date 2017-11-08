using System;
using System.Linq;
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
        ///     * If ordered sooner than 13:00 count today as a first day (assuming it's a working day)
        ///     * If there are no enough items in stock add 5 working days 
        ///         (IStockRepository - that is what you need, you can find it under Repositories. PS - we don't need an implementation of it, do we?)
        ///     * Orders worth more than $500 needs approval from manager - add 2 more days
        ///     
        /// We will need to add an overload which takes current date as a parameter 
        /// to make it independent from DateTime.Now
        /// 
        /// </summary>
        /// <returns></returns>
        public DateTime GetEstimatedDelivery()
        {
            throw new NotImplementedException();
        }        
    }
}
