using Dumblog.Network;

namespace Dumblog
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server(8080);
            while (server.IsActive)
            {

            }
        }
    }
}