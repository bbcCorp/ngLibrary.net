using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ngLibrary.Core
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string textMessage, string htmlMessage);
        Task SendEmailAsync(List<string> emails, string subject, string textMessage, string htmlMessage);
    }

}
