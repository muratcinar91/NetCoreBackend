using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;
using Castle.DynamicProxy;
using Core.Utilities.IOC;
using Core.Utilities.Security.Hashing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.Email
{


    public class SendEmail
    {
        private readonly SmtpClient _emailProvider;

        public SendEmail()
        {
            _emailProvider = ServiceTool.ServiceProviders.GetService<SmtpClient>();
        }
        public void SendEmailToSpecialAccount(IInvocation invocation, string[] torecipients, Stopwatch stopwatch)
        {
            var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var settings = builder.Build().GetSection("SendingEmailSettings").Get<EmailSettings>();

            _emailProvider.Port = settings.Port;
            _emailProvider.Host = settings.Host;
            _emailProvider.EnableSsl = true;
            _emailProvider.UseDefaultCredentials = false;
            _emailProvider.Credentials = new NetworkCredential(settings.EmailAddress, settings.Password);

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(settings.EmailAddress, settings.FromEmail);

            foreach (var recipient in torecipients)
            {
                mail.To.Add(recipient);
            }

            mail.Subject = "Performans Sorunu!!!";
            mail.IsBodyHtml = true;
            mail.Body =
                $"<h3>Performance :  {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name} ==> {stopwatch.Elapsed.TotalSeconds}</h3>";

            _emailProvider.SendMailAsync(mail);
        }
    }
}
