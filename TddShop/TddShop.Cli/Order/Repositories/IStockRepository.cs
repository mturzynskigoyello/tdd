using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TddShop.Cli.Order.Repositories
{
    public interface IStockRepository
    {
        int GetAvailableItemsByName(string itemName);
        void DecreaseItemsInStock(string itemName, int quantity);
        void IncreaseItemsInStock(string itemName, int quantity);
    }
}
