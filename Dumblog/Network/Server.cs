using Dumblog.View;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Dumblog.Network
{
    public class DumblogServer
    {
        PageLoader _loader;
        FaviconLoader _favicon;
        FeedbackLoader _feeback;
        IFeedbackSender _feebackSender;

        public void Configure(IApplicationBuilder app)
        {
            _loader = new PageLoader();
            _favicon = new FaviconLoader();
            _feebackSender = new FeedbackSmtpSender(new FeedbackSmtpSender.Config
            {
                from = "",
                to = "",
                subject = "Hello from DumBlog"
            });
            _feeback = new FeedbackLoader(new FeedbackLoader.Config
            {
                captcha = (DateTime.Now.Year + 1).ToString(),
            }, _feebackSender);
            app.Run(HttpRequestDelegate);
        }

        private async Task HttpRequestDelegate(HttpContext context)
        {
            if (await _favicon.TryProcess(context))
            {
                return;
            }

            if (await _feeback.TryProcess(context))
            {
                return;
            }

            string responseString = _loader.LoadFile(context.Request.Path.Value);
            System.Console.WriteLine($"Page is being requested: {context.Request.Path.Value}");
            await context.Response.WriteAsync(responseString);
        }
    }
}
