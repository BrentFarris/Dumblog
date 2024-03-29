﻿using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Dumblog.View
{
    public class FeedbackSmtpSender : IFeedbackSender
    {
        public struct Config
        {
            public string to;
            public string from;
            public string subject;
            public string userName;
            public string password;
        }

        private Config config;

        public FeedbackSmtpSender(Config config)
        {
            this.config = config;
        }

        public Task Send(FeedbackModel model)
        {
            var smtp = new SmtpClient();

            if (!string.IsNullOrEmpty(config.userName))
            {
                smtp.Credentials = new NetworkCredential(config.userName, config.password);
            }

            var builder = new StringBuilder();

            builder.AppendLine($"name: {model.name}");
            builder.AppendLine($"email: {model.email}");
            builder.AppendLine(string.Empty);
            builder.AppendLine($"{model.comments}");

            var mail = new MailMessage(config.from, config.to, config.subject, builder.ToString());
            smtp.Send(mail);
            return Task.FromResult(true);
        }
    }
}