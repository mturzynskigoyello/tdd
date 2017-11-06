using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddShop.Cli.Account;
using TddShop.Cli.Account.Repositories;
using TddShop.Cli.Tests.Account.Stubs;

namespace TddShop.Cli.Tests.Account
{
    [TestFixture]
    public class UsernameValidatorTests
    {
        private UsernameValidator _target;
        private Mock<IUsernameRepository> _moqUsernameRepositoryStub;
        
        [SetUp]
        public void Initialize()
        {
            _moqUsernameRepositoryStub = new Mock<IUsernameRepository>();
            
            _target = new UsernameValidator(_moqUsernameRepositoryStub.Object);
        }

        [Test]
        public void IsValid_UsernameContainsNonAlphanumericCharacters_ShouldReturnInvalid()
        {
            // Arrange            
            var username = "johnsmith#";
            _moqUsernameRepositoryStub.Setup(x => x.IsInUse(username)).Returns(false);

            // Act
            var result = _target.IsValid(username);

            // Assert
            Assert.That(result, Is.EqualTo(UsernameValidationResult.InvalidUsername));
        }

        [Test]
        public void IsValid_UsernameAlreadyInUse_ShouldReturnInUse()
        {
            // Arrange
            var username = "johnsmith";
            _moqUsernameRepositoryStub.Setup(x => x.IsInUse(username)).Returns(true);

            // Act
            var result = _target.IsValid(username);

            // Assert
            Assert.That(result, Is.EqualTo(UsernameValidationResult.UsernameInUse));
        }

        [Test]
        public void IsValid_UsernameIsValidAndAvailable_ShouldReturnOk()
        {
            // Arrange            
            var username = "johnsmith";
            _moqUsernameRepositoryStub.Setup(x => x.IsInUse(username)).Returns(false);

            // Act
            var result = _target.IsValid(username);

            // Assert
            Assert.That(result, Is.EqualTo(UsernameValidationResult.Ok));
        }
    }
}
