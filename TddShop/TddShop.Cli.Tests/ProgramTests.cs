using NUnit.Framework;
using TddShop.Cli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TddShop.Cli.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test]
        public void AddTwoNumbers_TwoPlusFour_ShouldReturnSix()
        {
            // Arrange
            int a = 2;
            int b = 4;

            // Act
            int result = Program.AddTwoNumbers(a, b);

            // Assert
            Assert.That(result, Is.EqualTo(6));
        }
    }
}
