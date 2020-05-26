using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text;
using Castle.DynamicProxy;
using Core.Utilities.Email;
using Core.Utilities.Interceptors.Autofac;
using Core.Utilities.IOC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private readonly int _interval;
        private readonly Stopwatch _stopwatch;
        private readonly string[] _recipients;
        private readonly SendEmail _sendEmail;

        public PerformanceAspect(int interval, string torecipients)
        {
            _interval = interval;
            
            _stopwatch = ServiceTool.ServiceProviders.GetService<Stopwatch>();
            _sendEmail = ServiceTool.ServiceProviders.GetService<SendEmail>();
            _recipients = torecipients.Split(',');
        }

        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                if (_recipients != null && _recipients.Length > 0)
                {
                    _sendEmail.SendEmailToSpecialAccount(invocation,_recipients,_stopwatch);
                }
            }
            _stopwatch.Reset();
        }
    }
}
