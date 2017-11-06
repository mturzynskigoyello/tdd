using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddShop.Cli.Order;
using TddShop.Cli.Order.Models;
using TddShop.Cli.Order.Repositories;

namespace TddShop.Cli.Tests.Order
{
    [TestFixture]
    public class OrderDeliveryDateTests
    {
        private Mock<IStockRepository> _stockRepository;

        [SetUp]
        public void Initialize()
        {
            _stockRepository = new Mock<IStockRepository>();
        }

        [Test]
        public void GetEstimatedDelivery_ShouldReturnDateLaterThanToday()
        {
            // Arrange
            var order = new OrderModel { Items = new ItemModel[0] };
            var target = new OrderDeliveryDate(order);
            target.StockRepository = _stockRepository.Object;
            _stockRepository.Setup(x => x.GetAvailableItemsByName(It.IsAny<string>())).Returns(int.MaxValue);

            var orderDate = DateTime.Parse("2017-11-09");
            
            // Act
            var actual = target.GetEstimatedDelivery(orderDate);

            // Assert
            Assert.That(actual, Is.GreaterThan(orderDate));
        }

        [Test]
        public void GetEstimatedDelivery_OrderPlacedOnMondayMorning_ShouldBeDeliveredOnWednesday()
        {
            // Arrange
            var order = new OrderModel { Items = new ItemModel[0] };
            var target = new OrderDeliveryDate(order);
            target.StockRepository = _stockRepository.Object;
            _stockRepository.Setup(x => x.GetAvailableItemsByName(It.IsAny<string>())).Returns(int.MaxValue);

            var orderDate = DateTime.Parse("2017-11-06 11:00"); // monday

            // Act
            var actual = target.GetEstimatedDelivery(orderDate);

            // Assert
            Assert.That(actual, Is.EqualTo(DateTime.Parse("2017-11-08")));
        }

        [Test]
        public void GetEstimatedDelivery_OrderPlacedOnMondayAfternoon_ShouldBeDeliveredOnThursday()
        {
            // Arrange
            var order = new OrderModel { Items = new ItemModel[0] };
            var target = new OrderDeliveryDate(order);
            target.StockRepository = _stockRepository.Object;
            _stockRepository.Setup(x => x.GetAvailableItemsByName(It.IsAny<string>())).Returns(int.MaxValue);

            var orderDate = DateTime.Parse("2017-11-06 15:00"); // monday

            // Act
            var actual = target.GetEstimatedDelivery(orderDate);

            // Assert
            Assert.That(actual, Is.EqualTo(DateTime.Parse("2017-11-09")));
        }

        [Test]
        public void GetEstimatedDelivery_OrderPlacedOnFridayAfternoon_ShouldBeDeliveredOnThursday()
        {
            // Arrange
            var order = new OrderModel { Items = new ItemModel[0] };
            var target = new OrderDeliveryDate(order);
            target.StockRepository = _stockRepository.Object;
            _stockRepository.Setup(x => x.GetAvailableItemsByName(It.IsAny<string>())).Returns(int.MaxValue);

            var orderDate = DateTime.Parse("2017-11-10 15:00"); // friday

            // Act
            var actual = target.GetEstimatedDelivery(orderDate);

            // Assert
            Assert.That(actual, Is.EqualTo(DateTime.Parse("2017-11-15")));
        }

        [Test]
        public void GetEstimatedDelivery_ThereAreNotEnoughItemsInStock_ShouldBeDeliveredIn9Days()
        {
            // Arrange
            var order = new OrderModel
            {
                Items = new ItemModel[]
                {
                    new ItemModel
                    {
                        Quantity = 1
                    }
                }
            };
            var target = new OrderDeliveryDate(order);
            target.StockRepository = _stockRepository.Object;
            _stockRepository.Setup(x => x.GetAvailableItemsByName(It.IsAny<string>())).Returns(0);

            var orderDate = DateTime.Parse("2017-11-06 15:00"); // monday

            // Act
            var actual = target.GetEstimatedDelivery(orderDate);

            // Assert
            Assert.That(actual, Is.EqualTo(DateTime.Parse("2017-11-16")));
        }

        [Test]
        public void GetEstimatedDelivery_OrderWorthEur600PlacedOnMonday_ShouldBeDeliveredOnNextMonday()
        {
            // Arrange
            var order = new OrderModel
            {
                Items = new ItemModel[]
                {
                    new ItemModel
                    {
                        Quantity = 2,
                        Price = 300
                    }
                }
            };
            var target = new OrderDeliveryDate(order);
            target.StockRepository = _stockRepository.Object;
            _stockRepository.Setup(x => x.GetAvailableItemsByName(It.IsAny<string>())).Returns(int.MaxValue);

            var orderDate = DateTime.Parse("2017-11-06 15:00"); // friday

            // Act
            var actual = target.GetEstimatedDelivery(orderDate);

            // Assert
            Assert.That(actual, Is.EqualTo(DateTime.Parse("2017-11-13")));
        }
    }
}
