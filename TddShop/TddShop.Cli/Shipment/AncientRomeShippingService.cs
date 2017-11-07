using TddShop.Cli.Order.Models;

namespace TddShop.Cli.Shipment
{
    public class AncientRomeShippingService
    {
        private readonly IDeliveryService _deliveryService;

        public AncientRomeShippingService(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        /// <summary>
        /// To ship an order you need to generate a shipment reference number (see IDeliveryService).
        /// Ancient Rome works with romanian numbers so you will need to convert shipment reference number to a valid romanian number (string).
        /// Use IDeliveryService to ship an order.
        ///                
        /// </summary>
        /// <param name="order"></param>
        public void ShipOrder(OrderModel order)
        {
            
        }
    }    
}
