using Dumblog.View;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Dumblog.Network
{
    public class DumblogServer
    {
        PageLoader _loader;
        FaviconLoader _favicon;
        FeedbackLoader _feeback;

        public void Configure(IApplicationBuilder app)
        {
            _loader = new PageLoader();
            _favicon = new FaviconLoader();
            _feeback = new FeedbackLoader(new FeedbackLoader.Config
            {
                from = "todo",
                to = "todo",
                subject = "Hello from DumBlog"
            });
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
