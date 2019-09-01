using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Dumblog.View
{
    public struct FeedbackModel
    {
        public string name;
        public string email;
        public string captcha;
        public string comments;
    }

    public interface IFeedbackSender
    {
        Task Send(FeedbackModel model);
    }

    /// <summary>
    /// Feedback Page Code
    /// </summary>
    public class FeedbackLoader
    {
        public struct Config
        {
            public string captcha;
        }

        private const string MARKDOWN_REPLACE_TEXT = "MARKDOWN_REPLACE";
        private const string MESSAGE_REPLACE_TEXT = "<!--MESSAGE_REPLACE-->";

        private Config config;
        private IFeedbackSender sender;
        private readonly string _successHtml = string.Empty;
        private readonly string _errorHtml = string.Empty;
        private readonly string _indexHtml = string.Empty;

        public FeedbackLoader(Config config, IFeedbackSender sender)
        {
            this.config = config;
            this.sender = sender;

            var template = File.ReadAllText("Content/template.html");
            _indexHtml = template.Replace(MARKDOWN_REPLACE_TEXT, File.ReadAllText("Content/Feedback/feedback.html"));
            _errorHtml = _indexHtml.Replace(MESSAGE_REPLACE_TEXT, File.ReadAllText("Content/Feedback/feedback_error.html"));
            _successHtml = _indexHtml.Replace(MESSAGE_REPLACE_TEXT, File.ReadAllText("Content/Feedback/feedback_success.html"));
        }

        public async Task<bool> TryProcess(HttpContext context)
        {
            try
            {
                if (context.Request.Method == "POST")
                {
                    await ProcessPost(context);
                    return true;
                }
                else if (context.Request.Path.Value.Equals("/feedback"))
                {
                    await ReturnGet(context);
                    return true;
                }
                else if (context.Request.Path.Value.Equals("/feedback_success"))
                {
                    await ReturnSuccess(context);
                    return true;
                }
                else if (context.Request.Path.Value.Equals("/feedback_error"))
                {
                    await ReturnError(context);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(FeedbackLoader)} Exception {ex.Message}");
            }
            return false;
        }

        // Gets

        async Task ReturnGet(HttpContext context)
        {
            Console.WriteLine($"{nameof(FeedbackLoader)} ProcessGet");
            await context.Response.WriteAsync(_indexHtml);
        }

        async Task ReturnError(HttpContext context)
        {
            Console.WriteLine($"{nameof(FeedbackLoader)} ReturnError");
            await context.Response.WriteAsync(_errorHtml);
        }

        async Task ReturnSuccess(HttpContext context)
        {
            Console.WriteLine($"{nameof(FeedbackLoader)} ReturnSuccess");
            await context.Response.WriteAsync(_successHtml);
        }

        // Redirects

        void RedirectError(HttpContext context)
        {
            Console.WriteLine($"{nameof(FeedbackLoader)} RedirectError");
            context.Response.Redirect("feedback_error");
        }

        void RedirectSuccess(HttpContext context)
        {
            Console.WriteLine($"{nameof(FeedbackLoader)} RedirectSuccess");
            context.Response.Redirect("feedback_success");
        }

        // Post

        async Task ProcessPost(HttpContext context)
        {
            Console.WriteLine($"{nameof(FeedbackLoader)} ProcessPost");

            FeedbackModel model = await DeserializeModel(context);

            try
            {
                if (ValidateModel(model))
                {
                    await sender.Send(model);

                    RedirectSuccess(context);
                }
                else
                {
                    Console.WriteLine($"{nameof(FeedbackLoader)} ERROR - ValidateModel");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(FeedbackLoader)} Exception {ex.Message}");
            }

            RedirectError(context);
        }

        async Task<FeedbackModel> DeserializeModel(HttpContext content)
        {
            var form = await content.Request.ReadFormAsync();

            return new FeedbackModel
            {
                captcha = form[nameof(FeedbackModel.captcha)],
                comments = form[nameof(FeedbackModel.comments)],
                email = form[nameof(FeedbackModel.email)],
                name = form[nameof(FeedbackModel.name)],
            };
        }

        bool ValidateModel(FeedbackModel model)
        {
            if (!config.captcha.Equals(model.captcha))
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