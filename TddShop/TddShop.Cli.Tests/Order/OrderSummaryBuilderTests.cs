using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddShop.Cli.Order;
using TddShop.Cli.Order.Models;

namespace TddShop.Cli.Tests.Order
{
    [TestFixture]
    public class OrderSummaryBuilderTests
    {
        [Test]
        public void GetSummary_OrderHasTwoItems_SummaryShouldStartWith_2_items()
        {
            // Arrange
            var order = new OrderModel()
            {
                Items = new[] 
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

            var target = new OrderSummaryBuilder();

            // Act
            var actual = target.GetSummary(order);

            // Assert
            Assert.That(actual.StartsWith("2 items"), Is.True);
        }

        [Test]
        public void GetSummary_OrderHasOneItem_SummaryShouldStartWith_1_item()
        {
            // Arrange
            var order = new OrderModel()
            {
                Items = new[] 
                {
                    new ItemModel
                    {
                        Quantity = 1
                    }
                }
            };

            var target = new OrderSummaryBuilder();

            // Act
            var actual = target.GetSummary(order);

            // Assert
            Assert.That(actual.StartsWith("1 item"), Is.True);
        }

        [Test]
        public void GetSummary_OrderHasOneItemSoldTwice_SummaryShouldStartWith_2_items()
        {
            // Arrange
            var order = new OrderModel()
            {
                Items = new[] 
                {
                    new ItemModel
                    {
                        Quantity = 2
                    }
                }
            };

            var target = new OrderSummaryBuilder();

            // Act
            var actual = target.GetSummary(order);

            // Assert
            Assert.That(actual.StartsWith("2 items"), Is.True);
        }

        [Test]
        public void GetSummary_OrderHasOneItemWorthEur20_SummaryShouldEndWith_Eur20()
        {
            // Arrange
            var order = new OrderModel()
            {
                Items = new[] 
                {
                    new ItemModel
                    {
                        Quantity = 1,
                        Price = 20
                    }
                }
            };

            var target = new OrderSummaryBuilder();

            // Act
            var actual = target.GetSummary(order);

            // Assert
            Assert.That(actual.EndsWith("EUR 20"), Is.True);
        }

        [Test]
        public void GetSummary_OrderHasOneItemWorthEur20SoldTwice_SummaryShouldEndWith_Eur40()
        {
            // Arrange
            var order = new OrderModel()
            {
                Items = new[] 
                {
                    new ItemModel
                    {
                        Price = 20,
                        Quantity = 2
                    }
                }
            };

            var target = new OrderSummaryBuilder();

            // Act
            var actual = target.GetSummary(order);

            // Assert
            Assert.That(actual.EndsWith("EUR 40"), Is.True);
        }

        [Test]
        public void GetSummary_OrderHasSomeItems_SummaryShouldContain_WithTotalValueOf()
        {
            // Arrange
            var order = new OrderModel()
            {
                Items = new[] 
                {
                    new ItemModel
                    {
                        Quantity = 1
                    }
                }
            };

            var target = new OrderSummaryBuilder();

            // Act
            var actual = target.GetSummary(order);

            // Assert
            Assert.That(actual.Contains("with total value of"), Is.True);
        }

        [Test]
        public void GetSummary_OrderHasNoItems_SummaryShouldBe_ThereAreNoItemsInTheOrder()
        {
            // Arrange
            var order = new OrderModel()
            {
                Items = new ItemModel[0]
            };

            var target = new OrderSummaryBuilder();

            // Act
            var actual = target.GetSummary(order);

            // Assert
            Assert.That(actual.Contains("There are no items in the order"), Is.True);
        }
    }
}
