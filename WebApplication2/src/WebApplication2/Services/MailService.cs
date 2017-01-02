using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heinbo.Services
{
    public class MailService : IMailService
    {
        private IConfigurationRoot _config;

        public MailService(IConfigurationRoot config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string to,string from, string subject, string message, string sender)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(sender, from));
            emailMessage.To.Add(new MailboxAddress("", to));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_config["MailService:ServerHost"], 465, SecureSocketOptions.Auto).ConfigureAwait(false);
                await client.AuthenticateAsync(_config["MailService:Username"], _config["MailService:Password"]);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}
