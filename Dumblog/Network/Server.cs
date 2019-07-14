using Dumblog.View;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Dumblog.Network
{
    public class DumblogServer
    {
        static readonly object _locker = new object();

        PageLoader _loader;

        public void Configure(IApplicationBuilder app)
        {
            _loader = new PageLoader();
            app.Run(HttpRequestDelegate);
        }

        private async Task HttpRequestDelegate(HttpContext context)
        {
            try
            {  
                //remove / prefix
                var localPath = context.Request.Path.Value.Substring(1, context.Request.Path.Value.Length - 1);

                string responseString;

                lock (_locker)
                {
                    responseString = _loader.LoadFile(localPath);
                }

                await context.Response.WriteAsync(responseString);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}