using NUnit.Framework;
using Moq;
using TddShop.Cli.Order;
using TddShop.Cli.Order.Models;
using TddShop.Cli.Order.Repositories;

namespace TddShop.Cli.Tests.Order
{
    [TestFixture]
    public class OrderServiceTests
    {
        private Mock<IOrderRepository> _orderRepository;
        private Mock<IStockRepository> _stockRepository;

        private OrderService _target;

        [SetUp]
        public void Initialize()
        {
            _orderRepository = new Mock<IOrderRepository>();
            _stockRepository = new Mock<IStockRepository>();

            _target = new OrderService(_orderRepository.Object, _stockRepository.Object);
        }

        [Test]
        public void PlaceOrder_OrderHasNoItems_ShouldNeverSaveOrder()
        {
            // Arrange
            var order = new OrderModel
            {
                Items = new ItemModel[0]
            };
            
            // Act
            _target.PlaceOrder(order);

            // Assert
            _orderRepository.Verify(x => x.SaveOrder(It.IsAny<OrderModel>()), Times.Never);
        }

        [Test]
        public void PlaceOrder_OrderHasSomeItems_ShouldSaveOrder()
        {
            // Arrange
            var order = new OrderModel
            {
                Items = new ItemModel[]
                {
                    new ItemModel
                    {
                        Quantity = 2
                    }
                }
            };

            // Act
            _target.PlaceOrder(order);

            // Assert
            _orderRepository.Verify(x => x.SaveOrder(It.IsAny<OrderModel>()), Times.Once);
        }

        [Test]
        public void PlaceOrder_OrderHasSpecificItemSoldTwice_ShouldDecreaseStockByTwo()
        {
            // Arrange
            var order = new OrderModel
            {
                Items = new ItemModel[]
                {
                    new ItemModel
                    {
                        Name = "Komputer",
                        Quantity = 2
                    }
                }
            };

            // Act
            _target.PlaceOrder(order);

            // Assert
            _stockRepository.Verify(x => x.DecreaseItemsInStock("Komputer", 2), Times.Once);
        }

        [Test]
        public void PlaceOrder_OrderHasMultipleItems_ShouldUpdateStockMultipleTimes()
        {
            // Arrange
            var order = new OrderModel
            {
                Items = new ItemModel[]
                {
                    new ItemModel
                    {                        
                        Quantity = 1
                    },
                    new ItemModel
                    {
                        Quantity = 1
                    }
                }
            };

            // Act
            _target.PlaceOrder(order);

            // Assert
            _stockRepository.Verify(x => x.DecreaseItemsInStock(It.IsAny<string>(), It.IsAny<int>()), Times.AtLeastOnce);
        }
    }
}
