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
            if(await HandleFavicon(context))
            {
                return;
            }
            string responseString = _loader.LoadFile(context.Request.Path.Value);
            await context.Response.WriteAsync(responseString);
        }

        private async Task<bool> HandleFavicon(HttpContext context)
        {
            if (context.Request.Path.Value.Equals("/favicon.ico"))
            {
                await context.Response.WriteAsync(string.Empty);
                return true;
            }
            return false;
        }
    }
}