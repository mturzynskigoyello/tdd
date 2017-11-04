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
        private UsernameRepositoryStub _usernameReposutoryStub;
        private Mock<IUsernameRepository> _moqUsernameRepositoryStub;
        
        [SetUp]
        public void Initialize()
        {
            _usernameReposutoryStub = new UsernameRepositoryStub();
            _moqUsernameRepositoryStub = new Mock<IUsernameRepository>();
            
            // start with custom stub, then show how to use Moq as a stub
            _target = new UsernameValidator(_usernameReposutoryStub);
        }

        [Test]
        public void IsValid_UsernameContainsNonAlphanumericCharacters_ShourtReturnInvalid()
        {
            // Arrange            
            var username = "johnsmith#";

            // Act
            var result = _target.IsValid(username);

            // Assert
            Assert.That(result, Is.EqualTo(UsernameValidationResult.InvalidUsername));
        }

        [Test]
        public void IsValid_UsernameAlreadyInUse_ShourtReturnInUse()
        {
            // Arrange            
            var username = "johnsmith";

            // Act
            var result = _target.IsValid(username);

            // Assert
            Assert.That(result, Is.EqualTo(UsernameValidationResult.UsernameInUse));
        }

        [Test]
        public void IsValid_UsernameIsValidAndAvailable_ShourtReturnOk()
        {
            // Arrange            
            var username = "johnsmith";

            // Act
            var result = _target.IsValid(username);

            // Assert
            Assert.That(result, Is.EqualTo(UsernameValidationResult.Ok));
        }
    }
}
