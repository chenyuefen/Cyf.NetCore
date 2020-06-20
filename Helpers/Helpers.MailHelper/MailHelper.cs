using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class MailHelper
    {
        public static async Task<bool> SentAsync(MailConfig config)
        {
            try
            {
                var message = CreateMimeMessage(config);
                using (SmtpClient client = new SmtpClient())
                {
                    client.CheckCertificateRevocation = false;
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Timeout = 10 * 1000;
                    await client.ConnectAsync(config.Host, config.Port, SecureSocketOptions.Auto);
                    await client.AuthenticateAsync(config.FromAddress, config.Password);
                    await client.SendAsync(message);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static MimeMessage CreateMimeMessage(MailConfig config)
        {
            MimeMessage mimeMessage = new MimeMessage();
            var fromMailAddress = new MailboxAddress(config.FromName ?? config.FromAddress, config.FromAddress);
            mimeMessage.From.Add(fromMailAddress);
            for (int i = 0; i < config.ToAddress.Count; i++)
            {
                var item = config.ToAddress[i];
                var toMailAddress = new MailboxAddress(item);
                mimeMessage.To.Add(toMailAddress);
            }
            BodyBuilder bodyBuilder = new BodyBuilder()
            {
                HtmlBody = config.Body,
            };
            if (config.Attachments != null)
            {
                var attachment = bodyBuilder.Attachments.Add(config.FileName, config.Attachments);
                //解决中文文件名乱码
                var charset = "GB18030";
                attachment.ContentType.Parameters.Clear();
                attachment.ContentDisposition.Parameters.Clear();
                attachment.ContentType.Parameters.Add(charset, "name", config.FileName);
                //解决文件名不能超过41字符
                foreach (var param in attachment.ContentType.Parameters)
                {
                    param.EncodingMethod = ParameterEncodingMethod.Rfc2047;
                }
            }
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = config.Subject;
            return mimeMessage;
        }

        /// <summary>
        /// 验证是否能够接收邮件
        /// </summary>
        public static async Task<bool> CanReceiveEmail(string userName, string password, string host, int port, bool useSsl)
        {
            try
            {
                using (var client = new Pop3Client())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(host, port, useSsl);
                    await client.AuthenticateAsync(userName, password);
                    await client.DisconnectAsync(true);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 接收邮件
        /// </summary>
        public static async Task<IEnumerable<MailEntity>> ReceiveEmail(string userName, string password, string host, int port, bool useSsl, int count = 1)
        {
            try
            {
                using (var client = new Pop3Client())
                {
                    // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(host, port, useSsl);
                    await client.AuthenticateAsync(userName, password);
                    var msg = await client.GetMessagesAsync(Enumerable.Range(client.Count - count, count).ToList());
                    var list = msg.Select(it => new MailEntity
                    {
                        Date = it.Date,
                        MessageId = it.MessageId,
                        Subject = it.Subject,
                        TextBody = it.TextBody,
                        HtmlBody = it.HtmlBody,
                    });
                    await client.DisconnectAsync(true);
                    return list;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

    }
}
