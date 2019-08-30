using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Dumblog.View
{
    public class FaviconLoader
    {
        public async Task<bool> TryProcess(HttpContext context)
        {
            try
            {
                return context.Request.Path.Value.Equals("/favicon.ico");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(FeedbackLoader)} Exception {ex.Message}");
            }
            return false;
        }
    }
}