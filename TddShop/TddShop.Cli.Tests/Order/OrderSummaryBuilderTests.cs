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
                Items = new[] {new ItemModel(), new ItemModel()}
            };

            var target = new OrderSummaryBuilder();

            // Act
            var actual = target.GetSummary(order);

            // Assert
            Assert.That(actual.StartsWith("2 items"), Is.True);
        }

        [Test]
        public void GetSummary_OrderHasTwoItems_SummaryShouldStartWith_1_item()
        {
            // Arrange
            var order = new OrderModel()
            {
                Items = new[] { new ItemModel() }
            };

            var target = new OrderSummaryBuilder();

            // Act
            var actual = target.GetSummary(order);

            // Assert
            Assert.That(actual.StartsWith("1 items"), Is.True);
        }
    }
}
