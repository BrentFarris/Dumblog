using Dumblog.View;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Dumblog.Network
{
    public class DumblogServer
    {
        PageLoader _loader;

        public void Configure(IApplicationBuilder app)
        {
            _loader = new PageLoader();
            app.Run(HttpRequestDelegate);
        }

        private async Task HttpRequestDelegate(HttpContext context)
        {
            if(IsFavicon(context))
            {
                await context.Response.WriteAsync(string.Empty);
            }
            else
            {
                string responseString = _loader.LoadFile(context.Request.Path.Value);
                await context.Response.WriteAsync(responseString);
            }
        }

        private bool IsFavicon(HttpContext context)
        {
            return (context.Request.Path.Value.Equals("/favicon.ico"));
        }
    }
}