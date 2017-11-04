using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TddShop.Cli.Invoice
{
    public class InvoiceSender
    {
        public void SendInvoice(string invoiceNumber, string emailAddress, string customerFullName, decimal totalValue)
        {
            var body = $"Hello {customerFullName}, please find attached an invoice for your recent order ({totalValue:c}).";
            var subject = "Invoice {invoiceNumber}";

            var smtpClient = new SmtpClient();
            smtpClient.Credentials = new NetworkCredential("username@domain.com", "password");
            smtpClient.Send("username@domain.com", emailAddress, subject, body);
        }

        public virtual void SendEmail(string emailAddress, string subject, string body)
        {

        }
    }
}
