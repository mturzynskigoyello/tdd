using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddShop.Cli.Shipment;

namespace TddShop.Cli.Tests.Shipment
{
    [TestFixture]
    public class AncientRomeShippingServiceTests
    {
        private Mock<IDeliveryService> _deliveryService;
        private AncientRomeShippingService _target;

        [SetUp]
        public void Initialize()
        {
            _deliveryService = new Mock<IDeliveryService>();
            _target = new AncientRomeShippingService(_deliveryService.Object);
        }
    }
}
