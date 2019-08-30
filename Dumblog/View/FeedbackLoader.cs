using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Dumblog.View
{

    /// <summary>
    /// Feedback Page Code
    /// </summary>
    public class FeedbackLoader
    {
        string captchaKey = (DateTime.Now.Year + 1).ToString();
        Config config;

        struct Model
        {
            public string name;
            public string email;
            public string captcha;
            public string comments;
        }

        public struct Config
        {
            public string to;
            public string from;
            public string subject;
        }

        public FeedbackLoader(Config config)
        {
            this.config = config;
        }

        public async Task<bool> TryProcess(HttpContext context)
        {
            try
            {
                if (context.Request.Path.Value.Equals("/feedback.html"))
                {
                    await ProcessGet(context);
                    return true;
                }
                else if (context.Request.Path.Value.Equals("/feedback"))
                {
                    await ProcessPost(context);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(FeedbackLoader)} Exception {ex.Message}");
            }
            return false;
        }

        async Task ProcessGet(HttpContext context)
        {
            Console.WriteLine($"{nameof(FeedbackLoader)} ProcessGet");

            var html = File.ReadAllText("Content/feedback.html");
            await context.Response.WriteAsync(html);
        }

        async Task ProcessPost(HttpContext context)
        {
            Console.WriteLine($"{nameof(FeedbackLoader)} ProcessPost");

            Model model = await DeserializeModel(context);

            if (ValidateModel(model))
            {
                var smtp = new SmtpClient();

                var builder = new StringBuilder();

                builder.AppendLine($"name: {model.name}");
                builder.AppendLine($"email: {model.email}");
                builder.AppendLine(string.Empty);
                builder.AppendLine($"{model.comments}");

                var mail = new MailMessage(config.from, config.to, config.subject, builder.ToString());
                smtp.Send(mail);
            }
            else
            {
                Console.WriteLine($"{nameof(FeedbackLoader)} ERROR - ValidateModel");
            }
        }

        async Task<Model> DeserializeModel(HttpContext content)
        {
            var form = await content.Request.ReadFormAsync();

            return new Model
            {
                captcha = form[nameof(Model.captcha)],
                comments = form[nameof(Model.comments)],
                email = form[nameof(Model.email)],
                name = form[nameof(Model.name)],
            };
        }

        bool ValidateModel(Model model)
        {
            if (!captchaKey.Equals(model.captcha))
            {
                Console.WriteLine($"{nameof(FeedbackLoader)} ERROR - captcha");
                return false;
            }
            if (string.IsNullOrEmpty(model.name))
            {
                Console.WriteLine($"{nameof(FeedbackLoader)} ERROR - name");
                return false;
            }
            if (string.IsNullOrEmpty(model.email))
            {
                Console.WriteLine($"{nameof(FeedbackLoader)} ERROR - email");
                return false;
            }
            if (string.IsNullOrEmpty(model.comments))
            {
                Console.WriteLine($"{nameof(FeedbackLoader)} ERROR - comments");
                return false;
            }
            return true;
        }
    }
}