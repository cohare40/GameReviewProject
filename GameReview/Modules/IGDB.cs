using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace GameReview.Modules
{
    public class IGDB
    {

        private readonly HttpClient _client;

        public IGDB()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("Client-ID", "9kztw06pcdhoib5z0w6ysmjlcgn5q9");
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer 8sxqlcpx1zyar8itwomvrkbejx7zpa");
        }


        public async Task<JArray> PostBasicAsync(string content, CancellationToken cancellationToken, string url)
        {
            Console.WriteLine(content);
            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                
                using (var stringContent = new StringContent(content, Encoding.UTF8, "application/json"))
                {
                    request.Content = stringContent;

                    using (var response = await _client
                        .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                        .ConfigureAwait(false))
                    {

                        //response.EnsureSuccessStatusCode();

                        var res = await response.Content.ReadAsStringAsync();
                        var gameJson = JArray.Parse(res);

                        return gameJson;
                    }
                }
            }
        }
    }
}
