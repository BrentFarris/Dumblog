using Dumblog.View;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dumblog.Network
{
    public class Server
    {
        public bool IsActive { get; private set; }
        private readonly HttpListener _listener;
        private Thread _listenThread;
        PageLoader _loader;

        public Server(ushort port)
        {
            _loader = new PageLoader();
            _listener = new HttpListener();
            _listener.Prefixes.Add($"http://*:{port}/");
            _listener.Start();
            _listenThread = new Thread(ReadNetwork);
            _listenThread.Start();
            IsActive = true;
        }

        private async void ReadNetwork()
        {
            while (IsActive)
            {
                await ContextRequestAsync();
            }
        }

        private async Task ContextRequestAsync()
        {
            HttpListenerContext context = await _listener.GetContextAsync();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;
            string responseString = _loader.LoadFile(request.Url.LocalPath.Substring(1));
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

        public void Stop()
        {
            _listenThread.Abort();
            _listener.Stop();
            IsActive = false;
        }
    }
}