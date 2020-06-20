using MailKit.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpers
{
    public class MailConfig
    {
        public MailConfig()
        {
        }

        public MailConfig(string host, int port, string password, string fromName, string fromAddress, string toAddress, string subject, string body)
        {
            Host = host;
            Port = port;
            Password = password;
            FromName = fromName;
            FromAddress = fromAddress;
            ToAddress = new List<string> { toAddress };
            Subject = subject;
            Body = body;
        }

        public string Host { get; set; }
        public int Port { get; set; }
        public SecureSocketOptions Options { get; set; } = SecureSocketOptions.Auto;
        public string FromAddress { get; set; }
        public string FromName { get; set; }
        public string Password { get; set; }
        public List<string> ToAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public byte[] Attachments { get; set; }
        public string FileName { get; set; } = "未命名文件.txt";
    }
}
