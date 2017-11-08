using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddShop.Cli.Order.Models;
using TddShop.Cli.Order.Repositories;

namespace TddShop.Cli.Order
{
    public class DiscountCalculator
    {
        private readonly IStockRepository _stockRepository;
        private const string BeerCategory = "Beer";

        public DiscountCalculator(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        /// <summary>
        /// Possible rules:
        ///     * Total order value > $500 : 10%
        ///     * Items from 'Beer' category are discounted: EUR 2 (don't allow negative price)
        ///     * Items which are leftovers (only one item in stock left): 
        ///         50% to items worth more than EUR 100, 75% to items worth more than EUR 200
        ///     * Discounts placed in December: 30%
        /// 
        /// Only one discount can be applied - it should be the one which is the best for customer
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public decimal GetDiscount(OrderModel order, DateTime orderPlacedOn)
        {
            var discounts = new List<decimal>();
            var orderValue = order.Items.Sum(item => item.Price);
            var lastItemsAvailable = order.Items
                .Where(item => _stockRepository.GetAvailableItemsByName(item.Name) == 1)
                .ToList();

            if (orderValue > 500)
            {
                discounts.Add(orderValue * 0.1M);
            }
            if (order.Items.Any(item => item.Category == BeerCategory))
            {
                discounts.Add(
                    order.Items
                        .Where(item => item.Category == BeerCategory)
                        .Sum(item => item.Price - 2 > 0 ? item.Price - 2 : item.Price));
            }
            if (lastItemsAvailable.Any())
            {
                discounts.Add(
                    lastItemsAvailable
                        .Select(item => item.Price)
                        .Where(price => price >= 100)
                        .Sum(price => price >= 200 ? price * 0.75M : price * 0.5M));
            }
            if (orderPlacedOn.Month == 12)
            {
                discounts.Add(orderValue * 0.3M);
            }

            return discounts.DefaultIfEmpty(0).Max();
        }
    }
}
