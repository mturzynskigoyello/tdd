using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddShop.Cli.Order.Models;

namespace TddShop.Cli.Order.Repositories
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Returns order reference number
        /// </summary>
        /// <param name="orderModel"></param>
        /// <returns></returns>
        string SaveOrder(OrderModel orderModel);
    }
}
