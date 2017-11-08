using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TddShop.Cli.Order;
using TddShop.Cli.Order.Models;
using TddShop.Cli.Order.Repositories;

namespace TddShop.Cli.Tests.Order
{
    [TestFixture]
    public class DiscountCalculatorTests
    {
        private DiscountCalculator _target;
        private Mock<IStockRepository> _stockRepositoryStub;

        [SetUp]
        public void SetUp()
        {
            _stockRepositoryStub = new Mock<IStockRepository>();
            _target = new DiscountCalculator(_stockRepositoryStub.Object);
        }

        [Test]
        public void GetDiscount_EmptyOrder_ShouldReturnZero()
        {
            // Arrange
            var orderModel = new OrderModel();

            // Act
            var actual = _target.GetDiscount(orderModel, new DateTime(2017, 11, 8));

            // Assert
            Assert.That(actual, Is.EqualTo(0));
        }

        [Test]
        public void GetDiscount_OrderValueAbove500_ShouldReturn10percentForAll()
        {
            // Arrange
            var orderModel = new OrderModel
            {
                Items = new[]
                {
                    new ItemModel
                    {
                        Price = 200,
                    },
                    new ItemModel
                    {
                        Price = 200,
                    },
                    new ItemModel
                    {
                        Price = 150,
                    }
                }
            };

            var expected = (200 + 200 + 150) * 0.1M;

            // Act
            var actual = _target.GetDiscount(orderModel, new DateTime(2017, 11, 8));

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetDiscount_OrderWithBeerItems_ShouldDiscountBeerItems()
        {
            // Arrange
            var beerCategory = "Beer";
            var orderModel = new OrderModel
            {
                Items = new[]
                {
                    new ItemModel
                    {
                        Category = beerCategory,
                        Price = 5,
                    },
                    new ItemModel
                    {
                        Category = beerCategory,
                        Price = 4,
                    },
                    new ItemModel
                    {
                        Category = "Food",
                        Price = 40
                    }
                }
            };

            var expected = (5-2) + (4-2);

            // Act
            var actual = _target.GetDiscount(orderModel, new DateTime(2017, 11, 8));

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetDiscount_OrderWithBeerItemsPriceBelow2_ShouldNotCountNegativePrice()
        {
            // Arrange
            var beerCategory = "Beer";
            var orderModel = new OrderModel
            {
                Items = new[]
                {
                    new ItemModel
                    {
                        Category = beerCategory,
                        Price = 5,
                    },
                    new ItemModel
                    {
                        Category = beerCategory,
                        Price = 1,
                    }
                }
            };

            var expected = (5 - 2) + 1;

            // Act
            var actual = _target.GetDiscount(orderModel, new DateTime(2017, 11, 8));

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetDiscount_IfOrderedLastItemOnStockCheaperProduct_ShouldReturn50percentDiscount()
        {
            // Arrange
            var lastItemName = "Toy car";
            var cheaperLastItemName = "Cheese";

            var orderModel = new OrderModel
            {
                Items = new[]
                {
                    new ItemModel
                    {
                        Name = lastItemName,
                        Price = 126,
                    },
                    new ItemModel
                    {
                        Name = cheaperLastItemName,
                        Price = 50,
                    }
                }
            };

            _stockRepositoryStub
                .Setup(repository => repository.GetAvailableItemsByName(lastItemName))
                .Returns(1);

            _stockRepositoryStub
                .Setup(repository => repository.GetAvailableItemsByName(cheaperLastItemName))
                .Returns(1);

            var expected = 126 * 0.5M;

            // Act
            var actual = _target.GetDiscount(orderModel, new DateTime(2017, 11, 8));

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetDiscount_IfOrderedMultipleLastItems_ShouldReturnWithDiscounts()
        {
            // Arrange
            var lastItemName = "Toy car";
            var moreExpensiveLastItemName = "Expensive Cheese";

            var orderModel = new OrderModel
            {
                Items = new[]
                {
                    new ItemModel
                    {
                        Name = lastItemName,
                        Price = 126,
                    },
                    new ItemModel
                    {
                        Name = moreExpensiveLastItemName,
                        Price = 200,
                    }
                }
            };

            _stockRepositoryStub
                .Setup(repository => repository.GetAvailableItemsByName(lastItemName))
                .Returns(1);

            _stockRepositoryStub
                .Setup(repository => repository.GetAvailableItemsByName(moreExpensiveLastItemName))
                .Returns(1);

            var expected = 126 * 0.5M + 200*0.75M;

            // Act
            var actual = _target.GetDiscount(orderModel, new DateTime(2017, 11, 8));

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetDiscount_IfOrderPlacedInDecember_ShouldReturn30percentForAll()
        {
            // Arrange
            var orderModel = new OrderModel
            {
                Items = new[]
                {
                    new ItemModel
                    {
                        Price = 126,
                    },
                    new ItemModel
                    {
                        Price = 200,
                    }
                }
            };

            var expected = (126 + 200) * 0.3M;

            // Act
            var actual = _target.GetDiscount(orderModel, new DateTime(2016, 12, 13));

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetDiscount_IfOrderPlacedInDecemberAndAbove500_ShouldReturn30percentForAll()
        {
            // Arrange
            var orderModel = new OrderModel
            {
                Items = new[]
                {
                    new ItemModel
                    {
                        Price = 400,
                    },
                    new ItemModel
                    {
                        Price = 200,
                    }
                }
            };

            var expected = (400 + 200) * 0.3M;

            // Act
            var actual = _target.GetDiscount(orderModel, new DateTime(2016, 12, 13));

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetDiscount_IfOrderPlacedInDecemberBuyOnlyLeftOvers_ShouldReturnLeftoversDiscounts()
        {
            // Arrange
            var lastItemName = "Toy car";
            var moreExpensiveLastItemName = "Expensive Cheese";

            var orderModel = new OrderModel
            {
                Items = new[]
                {
                    new ItemModel
                    {
                        Name = lastItemName,
                        Price = 400,
                    },
                    new ItemModel
                    {
                        Name = moreExpensiveLastItemName,
                        Price = 100,
                    }
                }
            };

            _stockRepositoryStub
                .Setup(repository => repository.GetAvailableItemsByName(lastItemName))
                .Returns(1);

            _stockRepositoryStub
                .Setup(repository => repository.GetAvailableItemsByName(moreExpensiveLastItemName))
                .Returns(1);

            var expected = 400 * 0.75M + 100 * 0.5M;

            // Act
            var actual = _target.GetDiscount(orderModel, new DateTime(2016, 12, 13));

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetDiscount_IfOrderOnlyWithBeersFor5Eur_ShouldChooseBeerDiscount()
        {
            // Arrange
            var beerCategory = "Beer";
            var numberOfBeers = 100;
            var beerPrice = 5;

            var orderItems = Enumerable.Range(1, numberOfBeers).Select(i => new ItemModel
            {
                Category = beerCategory,
                Price = beerPrice,
            }).ToArray();

            var orderModel = new OrderModel
            {
                Items = orderItems
            };

            var expected = numberOfBeers * (beerPrice - 2);

            // Act
            var actual = _target.GetDiscount(orderModel, new DateTime(2017, 11, 8));

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetDiscount_IfOrderedInDecemberOnlyWithBeersFor5Eur_ShouldChooseDecemberDiscount()
        {
            // Arrange
            var beerCategory = "Beer";
            var numberOfBeers = 30;
            var beerPrice = 5;

            var orderItems = Enumerable.Range(1, numberOfBeers).Select(i => new ItemModel
            {
                Category = beerCategory,
                Price = beerPrice,
            }).ToArray();

            var orderModel = new OrderModel
            {
                Items = orderItems
            };

            var expected = numberOfBeers * (5 - 2);

            // Act
            var actual = _target.GetDiscount(orderModel, new DateTime(2016, 12, 13));

            // Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
