using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TddShop.Cli.Invoice;

namespace TddShop.Cli.Tests.Invoice.Mocks
{
    public class InvoiceSenderMock : InvoiceSender
    {
        public bool EmailSent { get; private set; }
        public string EmailAddress { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }

        protected override void SendEmail(string emailAddress, string subject, string body)
        {
            EmailSent = true;
            EmailAddress = emailAddress;
            Subject = subject;
            Body = body;
        }
    }
}
