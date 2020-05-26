using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Email
{
    public class EmailSettings
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public string FromEmail { get; set; }
    }
}
