using NUnit.Framework;
using TddShop.Cli.Tests.Invoice.Mocks;

namespace TddShop.Cli.Tests.Invoice
{
    [TestFixture]
    public class InvoiceSenderTests
    {
        [Test]
        public void SendInvoice_SubjectContainsInvoiceNumber()
        {
            // Arrange
            var target = new InvoiceSenderMock();
            var invoiceNumber = "123456";

            // Act
            target.SendInvoice(invoiceNumber, "test@gmail.com", "Super test", 12);

            // Assert
            Assert.That(target.Subject.Contains(invoiceNumber));
        }

        [Test]
        public void SendInvoice_EmailIsSent()
        {
            // Arrange
            var target = new InvoiceSenderMock();
            var invoiceNumber = "123456";

            // Act
            target.SendInvoice(invoiceNumber, "test@gmail.com", "Super test", 12);

            // Assert
            Assert.That(target.EmailSent, Is.True);
        }        
    }
}
