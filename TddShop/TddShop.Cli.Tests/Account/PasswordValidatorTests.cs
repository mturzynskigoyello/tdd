using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddShop.Cli.Account;

namespace TddShop.Cli.Tests.Account
{
    [TestFixture]
    public class PasswordValidatorTests
    {
        [Test]
        public void IsValid_PasswordIsValid_ShourtReturnTrue()
        {
            // Arrange
            var target = new PasswordValidator();
            var password = "Some$up3rSECretP4w00rd";

            // Act
            var isValid = target.IsValid(password);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        public void IsValid_PasswordIsEmpty_ShourtReturnFalse()
        {
            // Arrange
            var target = new PasswordValidator();
            var password = "";

            // Act
            var isValid = target.IsValid(password);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        public void IsValid_PasswordIsTooShort_ShourtReturnFalse()
        {
            // Arrange
            var target = new PasswordValidator();
            var password = "pass";

            // Act
            var isValid = target.IsValid(password);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        public void IsValid_PasswordContainsNoDigit_ShourtReturnFalse()
        {
            // Arrange
            var target = new PasswordValidator();
            var password = "Some$uperSecretPassword";

            // Act
            var isValid = target.IsValid(password);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        public void IsValid_PasswordContainsNoUppercase_ShourtReturnFalse()
        {
            // Arrange
            var target = new PasswordValidator();
            var password = "some$upersecretpassword32";

            // Act
            var isValid = target.IsValid(password);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        public void IsValid_PasswordContainsNoLowercase_ShourtReturnFalse()
        {
            // Arrange
            var target = new PasswordValidator();
            var password = "SOME$SUPERSECRETPASSWORD4";

            // Act
            var isValid = target.IsValid(password);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        public void IsValid_PasswordContainsNoPunctuation_ShourtReturnFalse()
        {
            // Arrange
            var target = new PasswordValidator();
            var password = "SomeSuperSecretPassword2";

            // Act
            var isValid = target.IsValid(password);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        public void IsValid_PasswordContainsSpaceAsPunctation_ShourtReturnTrue()
        {
            // Arrange
            var target = new PasswordValidator();
            var password = "Some sup3rSECretP4w00rd";

            // Act
            var isValid = target.IsValid(password);

            // Assert
            Assert.That(isValid, Is.True);
        }
    }
}
