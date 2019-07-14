using Dumblog.Network;
using Microsoft.AspNetCore.Hosting;

namespace Dumblog
{
    class Program
    {
        static void Main(string[] args)
        {
            new WebHostBuilder()
                 .UseKestrel()
                 .UseStartup<DumblogServer>()
                 .UseUrls("http://*:999")
                 .Build()
                 .Run();
        }
    }
}