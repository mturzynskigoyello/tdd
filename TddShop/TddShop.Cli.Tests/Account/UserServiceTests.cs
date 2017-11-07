using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TddShop.Cli.Account;
using TddShop.Cli.Account.Repositories;

namespace TddShop.Cli.Tests.Account
{
    [TestFixture]
    public class UserServiceTests
    {
        // Stubs
        private Mock<IUsernameValidator> _usernameValidator;
        private Mock<IPasswordValidator> _passwordValidator;

        // Mocks
        private Mock<IUsernameRepository> _usernameRepository;

        private UserService _target;

        [SetUp]
        public void Initialize()
        {
            _usernameRepository = new Mock<IUsernameRepository>();
            _passwordValidator = new Mock<IPasswordValidator>();
            _usernameValidator = new Mock<IUsernameValidator>();

            _target = new UserService(_usernameValidator.Object, _passwordValidator.Object, _usernameRepository.Object);
        }

        [Test]
        public void Create_UsernameIsNotValid_ShouldReturnFalse()
        {
            // Arrange
            var username = "johnrambo";
            var password = "password";

            _usernameValidator.Setup(x => x.IsValid(username)).Returns(UsernameValidationResult.InvalidUsername);
            _passwordValidator.Setup(x => x.IsValid(password)).Returns(true);

            // Act
            var actual = _target.CreateUser(username, password);

            // Assert
            Assert.That(actual, Is.False);
        }
        
        [Test]
        public void Create_UsernameIsInUse_ShouldReturnFalse()
        {
            // Arrange
            var username = "johnrambo";
            var password = "password";

            _usernameValidator.Setup(x => x.IsValid(username)).Returns(UsernameValidationResult.UsernameInUse);
            _passwordValidator.Setup(x => x.IsValid(password)).Returns(true);

            // Act
            var actual = _target.CreateUser(username, password);

            // Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void Create_PasswordIsInvalid_ShouldReturnFalse()
        {
            // Arrange
            var username = "johnrambo";
            var password = "password";

            _usernameValidator.Setup(x => x.IsValid(username)).Returns(UsernameValidationResult.Ok);
            _passwordValidator.Setup(x => x.IsValid(password)).Returns(false);

            // Act
            var actual = _target.CreateUser(username, password);

            // Assert
            Assert.That(actual, Is.False);
        }

        [Test]
        public void Create_PasswordIsInvalid_ShouldNeverCreateUser()
        {
            // Arrange
            var username = "johnrambo";
            var password = "password";

            _usernameValidator.Setup(x => x.IsValid(username)).Returns(UsernameValidationResult.Ok);
            _passwordValidator.Setup(x => x.IsValid(password)).Returns(false);

            // Act
            var actual = _target.CreateUser(username, password);

            // Assert
            _usernameRepository.Verify(x => x.Create(username, password), Times.Never);
        }

        [Test]
        public void Create_UsernameIsInvalid_ShouldNeverCreateUser()
        {
            // Arrange
            var username = "johnrambo";
            var password = "password";

            _usernameValidator.Setup(x => x.IsValid(username)).Returns(UsernameValidationResult.InvalidUsername);
            _passwordValidator.Setup(x => x.IsValid(password)).Returns(true);

            // Act
            var actual = _target.CreateUser(username, password);

            // Assert
            _usernameRepository.Verify(x => x.Create(username, password), Times.Never);
        }

        [Test]
        public void Create_UsernameIsInUse_ShouldNeverCreateUser()
        {
            // Arrange
            var username = "johnrambo";
            var password = "password";

            _usernameValidator.Setup(x => x.IsValid(username)).Returns(UsernameValidationResult.UsernameInUse);
            _passwordValidator.Setup(x => x.IsValid(password)).Returns(true);

            // Act
            var actual = _target.CreateUser(username, password);

            // Assert
            _usernameRepository.Verify(x => x.Create(username, password), Times.Never);
        }

        [Test]
        public void Create_UsernameAndPasswordAreValid_ShouldReturnTrue()
        {
            // Arrange
            var username = "johnrambo";
            var password = "password";

            _usernameValidator.Setup(x => x.IsValid(username)).Returns(UsernameValidationResult.Ok);
            _passwordValidator.Setup(x => x.IsValid(password)).Returns(true);

            // Act
            var actual = _target.CreateUser(username, password);

            // Assert
            Assert.That(actual, Is.True);
        }

        [Test]
        public void Create_UsernameAndPasswordAreValid_ShouldCreateUser()
        {
            // Arrange
            var username = "johnrambo";
            var password = "password";

            _usernameValidator.Setup(x => x.IsValid(username)).Returns(UsernameValidationResult.Ok);
            _passwordValidator.Setup(x => x.IsValid(password)).Returns(true);

            // Act
            var actual = _target.CreateUser(username, password);

            // Assert
            _usernameRepository.Verify(x => x.Create(username, password), Times.Once);
        }
    }
}