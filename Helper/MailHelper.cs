using Microsoft.Extensions.Options;
using ShopMVC.Settings;
using System.Net;
using System.Net.Mail;

namespace ShopMVC.Helper
{
    public class MailHelper
    {
        public MailSetting options { get; set; }
        public MailHelper(IOptions<MailSetting> options)
        {
            this.options = options.Value;    
        }
        public void Send(string to, string subject, string body)
        {
            var client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(options.Mail, options.Password);
            using (var message = new MailMessage(
                from: new MailAddress(options.Mail, options.Name),
                to: new MailAddress(to, to)
                ))
            {

                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = true;
                client.Send(message);
            }
        }
    }
}
