using TddShop.Cli.Order.Models;

namespace TddShop.Cli.Shipment
{
    public interface IDeliveryService
    {
        int GenerateShipmentReferenceNumber(int numberOfItemsToDeliver);
        void RequestDelivery(string shipmentReferenceNumber, OrderModel order);
    }
}