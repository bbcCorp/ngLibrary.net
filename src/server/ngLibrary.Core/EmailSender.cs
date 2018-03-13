using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using ngLibrary.Model;

namespace ngLibrary.Core
{
    public class SmtpConfig
    {
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmptUser { get; set; }
        public string SmtpPwd { get; set; }
        public bool EnableSSL = true;
    }

    public class EmailService : IEmailSender
    {
        private IConfigurationRoot _config;
        private ILogger<EmailService> _logger;
        private SmtpConfig smtpConfig;


        public EmailService(ILogger<EmailService> logger, IConfigurationRoot config)
        {
            if (logger == null)
                throw new ArgumentNullException("Object implementing ILogger needed for object initialization");

            if (config == null)
                throw new ArgumentNullException("Object implementing IConfigurationRoot needed for object initialization");

            _config = config;
            _logger = logger;

            try
            {
                smtpConfig = new SmtpConfig
                {
                    SmtpHost = _config["NotificationConf:SmtpServer:SmtpHost"],
                    SmtpPort = Convert.ToInt32(_config["NotificationConf:SmtpServer:SmtpPort"]),

                    SmptUser = _config["NotificationConf:SmtpServer:UserID"],
                    SmtpPwd = _config["NotificationConf:SmtpServer:Password"],
                    EnableSSL = Convert.ToBoolean(_config["NotificationConf:SmtpServer:EnableSSL"])
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, "Please provide valid configuation for NotificationConf:SmtpServer");
                throw;
            }

        }

        public async Task SendEmailAsync(string email, string subject, string textMessage, string htmlMessage)
        {
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("NewGen Library", "admin@nglibrary.com"));
                emailMessage.To.Add(new MailboxAddress(email));
                emailMessage.Subject = subject;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = htmlMessage;
                bodyBuilder.TextBody = textMessage;
                emailMessage.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect(smtpConfig.SmtpHost, smtpConfig.SmtpPort, smtpConfig.EnableSSL);

                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(smtpConfig.SmptUser, smtpConfig.SmtpPwd);

                    await client.SendAsync(emailMessage);

                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, "Error in sending out Emails");
                throw;
            }

        }

        public async Task SendEmailAsync(List<string> emails, string subject, string textMessage, string htmlMessage)
        {
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("NewGen Library", "admin@nglibrary.com"));

                emailMessage.Subject = subject;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = htmlMessage;
                bodyBuilder.TextBody = textMessage;
                emailMessage.Body = bodyBuilder.ToMessageBody();


                foreach (var email in emails)
                {
                    emailMessage.To.Add(new MailboxAddress(email));
                }

                using (var client = new SmtpClient())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    client.Connect(smtpConfig.SmtpHost, smtpConfig.SmtpPort, smtpConfig.EnableSSL);

                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate(smtpConfig.SmptUser, smtpConfig.SmtpPwd);

                    await client.SendAsync(emailMessage);

                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggingEvents.Error, ex, "Error in sending out Emails");
                throw;
            }

        }
    }
}
