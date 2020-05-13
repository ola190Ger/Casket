using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SendOnEmail
{
    public class Service
    {
        private readonly ILogger<Service> logger;
        
        public Service(ILogger<Service> logger)
        {
            this.logger = logger;
        }
        public void SendEmailDefault()// google stoped support System.Net.Mail and recommend using third-party app
        {
            try
            {
                //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                MailMessage message = new MailMessage();
                message.IsBodyHtml = true;
                message.From = new MailAddress("EventsCo@mycompany.com", "Events Co");
                message.To.Add("ola190.ger@gmail.com");
                message.Subject = "Message from Events Co";
                message.Body = "<div style=\"color:red;\"> Message with your email and pss for sign on web page</div>";
                //message.Attachments.Add("... path to the file ...");

                using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
                {
                    client.Credentials = new NetworkCredential("Forworkchange@gmail.com", "Abrakadabra2015");
                    client.Port = 587;//465;
                    
                    client.EnableSsl = true;

                    client.Send(message);
                    logger.LogInformation("Email sent succesfully");
                }

            }
            catch (Exception e)
            {
                logger.LogError(e.GetBaseException().Message);
            }
        }

        public void SendEmailCustom()//with mailkit
        {
            try
            {
                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress("Events Co", "EventsCo@mycompany.com"));
                message.To.Add(new MailboxAddress("ola190.ger@gmail.com"));
                message.Subject = "Message from Events Co";
                message.Body = new BodyBuilder() {HtmlBody= "<div style=\"color:green;\"> Message with your email and pss for sign on web page</div>" }.ToMessageBody();

                using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 465, true);
                    client.Authenticate("Forworkchange@gmail.com", "Abrakadabra2015");
                    client.Send(message);
                    client.Disconnect(true);
                    logger.LogInformation("Email sent succesfully");
                }
            }
            catch (Exception e)
            {
                logger.LogError(e.GetBaseException().Message);
            }
        }
    }
}
