
using System;
using System.IO;
using System.Reflection;
namespace SportsStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailFromAddress = "sportsstpre@example.com";
        public string MailToAddress = "orders@example.com";

        public string Username = "";
        public string Password = "";
        public string ServerName = "example.com";
        public int ServerPort = 8080;
        public bool UseSsl = true;
        public bool UseDefaultCredentials = false;
        public bool WriteAsFile = true;
        public string FileLocation = string.Format(@"{0}\bin\Dump", AppDomain.CurrentDomain.BaseDirectory);
    }
}
