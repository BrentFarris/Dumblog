using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dumblog.View
{
    public class FeedbackDiscordSender : IFeedbackSender
    {
        public struct Config
        {
            public string clientId;
            public string secret;
            public string channelId;
            public string code;
            public string redirectUrl;
        }

        private HttpClient client;
        private string _postUrl;
        private Config config;
        private string tokenInfo;
        private string accesstoken;

        public FeedbackDiscordSender(Config config)
        {
            this.client = new HttpClient();
            this.config = config;
            _postUrl = "https://discordapp.com/api/channels/" + config.channelId + "/messages";
            InitAccessToken();
        }

        public async Task Send(FeedbackModel model)
        {
            var builder = new StringBuilder();
            builder.Append("{");
            builder.Append($" 'content': '{model.comments}',");
            builder.Append($" 'tts': 'fakse',");
            builder.Append("}");
            await client.PostAsync(_postUrl, new StringContent(builder.ToString()));
        }

        void InitAccessToken()
        {
            try
            {
                string client_id = config.clientId;
                string client_sceret = config.secret;
                string code = config.code;
                string redirect_url = config.redirectUrl;

                /*Get Access Token from authorization code by making http post request*/

                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://discordapp.com/api/oauth2/token");
                webRequest.Method = "POST";
                string parameters = "client_id=" + client_id + "&client_secret=" + client_sceret + "&grant_type=authorization_code&code=" + code + "&redirect_uri=" + redirect_url + "";
                byte[] byteArray = Encoding.UTF8.GetBytes(parameters);
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = byteArray.Length;
                Stream postStream = webRequest.GetRequestStream();

                postStream.Write(byteArray, 0, byteArray.Length);
                postStream.Close();
                WebResponse response = webRequest.GetResponse();
                postStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(postStream);
                string responseFromServer = reader.ReadToEnd();

                tokenInfo = responseFromServer.Split(',')[0].Split(':')[1];
                accesstoken = tokenInfo.Trim().Substring(1, tokenInfo.Length - 3);

                //

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accesstoken);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"{nameof(FeedbackDiscordSender)} Exception {ex.Message}");
            }
        }
    }
}