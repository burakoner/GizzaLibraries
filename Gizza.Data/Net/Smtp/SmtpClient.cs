using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Gizza.Data.Net.Smtp
{
    public class SmtpClient : System.Net.Mail.SmtpClient
    {
        public MailAddress From { get; set; }

        public SmtpClient(SmtpSender smtpSender) : base()
        {
            this.SetSender(smtpSender);
        }

        public SmtpClient(string host, int port, bool enableSsl, bool useDefaultCredentials, NetworkCredential credentials) : base()
        {
            this.Host = host;
            this.Port = port; ;
            this.EnableSsl = enableSsl;
            this.UseDefaultCredentials = useDefaultCredentials;
            this.Credentials = credentials;
        }

        public void SetSender(SmtpSender smtpSender)
        {
            if (smtpSender != null)
            {
                this.From = smtpSender.From;
                this.Host = smtpSender.Host;
                this.Port = smtpSender.Port;
                this.UseDefaultCredentials = false;
                this.Credentials = smtpSender.Credentials;
                this.EnableSsl = smtpSender.EnableSsl;
            }
        }

        public bool Send(string recipient, string subject, string body)
        {
            try
            {
                var Message = new MailMessage(this.From, new MailAddress(recipient))
                {
                    BodyEncoding = Encoding.UTF8,
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = body
                };

                this.Send(Message);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SendAsync(string recipient, string subject, string body, object userToken = null)
        {
            try
            {
                var Message = new MailMessage(this.From, new MailAddress(recipient))
                {
                    BodyEncoding = Encoding.UTF8,
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = body
                };

                await Task.Run(() => this.SendAsync(Message, userToken));
                return true;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return false;
            }
        }

    }

    public class SmtpSender
    {
        public MailAddress From { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public NetworkCredential Credentials { get; set; }
        public bool EnableSsl { get; set; }
    }
}
