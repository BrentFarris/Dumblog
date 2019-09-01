using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Dumblog.View
{
    public class FaviconLoader
    {
        public Task<bool> TryProcess(HttpContext context)
        {
            bool r = false;
            try
            {
                r = context.Request.Path.Value.Equals("/favicon.ico");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(FeedbackLoader)} Exception {ex.Message}");
            }
            return Task.FromResult(r);
        }
    }
}