using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TddShop.Cli.Order.Models
{
    public class OrderModel
    {
        public string CustomerUsername { get; set; }
        public ItemModel[] Items { get; set; }
    }
}
