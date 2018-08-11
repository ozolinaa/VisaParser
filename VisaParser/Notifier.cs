using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace VisaParser
{
    class Notifier : IDisposable
    {
        SmtpClient smtpClient = null;
        private const string fromAddress = "noreply@ozolin.ru";

        public Notifier()
        {
            smtpClient = new SmtpClient("smtp.mail.ru");
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(fromAddress, "pass@word1");

        }

        public void Dispose()
        {
            smtpClient.Dispose();
        }

        public void SendEmail(string toAddress, string subject, string body)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(fromAddress);
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(toAddress);
            mailMessage.Body = body;
            mailMessage.Subject = subject;
            smtpClient.Send(mailMessage);
            Task.Delay(1000).Wait();
        }
    }
}
