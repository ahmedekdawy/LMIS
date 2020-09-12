using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace LMIS.Bll.Helpers
{
    public static class Utils
    {
        public static void SendEmail(string recipient, string subject, string body, List<string> attachments = null)
        {
            SendEmail(new List<string> { recipient }, subject, body, attachments);
        }

        public static void SendEmail(List<string> recipients, string subject, string body, List<string> attachments = null)
        {
            var settings = DalFactory.Singleton.ConfigCenter.List(new List<string>
            {
                "email.host", "email.port", "email.ssl", "email.displayname", "email.username", "email.password"
            });

            if (settings.Values.Count != 6) return;
            if (settings.Values.Any(string.IsNullOrWhiteSpace)) return;
            if (recipients == null || recipients.All(string.IsNullOrWhiteSpace)) return;

            var m = new MailMessage
            {
                IsBodyHtml = true,
                From = new MailAddress(settings["email.username"], settings["email.displayname"]),
                Subject = subject,
                Body = body
            };

            recipients.Where(r => !string.IsNullOrWhiteSpace(r))
                .ToList().ForEach(r => m.To.Add(new MailAddress(r)));

            if (attachments != null)
                attachments.Where(a => !string.IsNullOrWhiteSpace(a))
                    .ToList().ForEach(a => m.Attachments.Add(new Attachment(a)));

            var smtp = new SmtpClient
            {
                UseDefaultCredentials = false,
                Host = settings["email.host"],
                Port = Convert.ToInt16(settings["email.port"]),
                EnableSsl = Convert.ToBoolean(settings["email.ssl"]),
                Credentials = new NetworkCredential(settings["email.username"], settings["email.password"])
            };

            try
            {
                smtp.Send(m);
                smtp.Dispose();
                GC.Collect();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}