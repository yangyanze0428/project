using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Hx.HttpClientFactory
{
    public class HxHttpClient:IHxHttpClient
    {
        private readonly IHttpClientFactory _clientFactory;
        public HxHttpClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public Task<HttpResponseMessage> SendAsync(string url, string data, string contentType = "application/json")
        {
            var request = new HttpRequestMessage(HttpMethod.Post,
                new Uri(url));
            request.Headers.Clear();
            request.Content = new StringContent(data);
            //request.Headers.Remove("Content-Type");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);//("text/json");
            var client = _clientFactory.CreateClient();
            client.Timeout =new TimeSpan( 0,0,30);
            return client.SendAsync(request);
        }
    }
}
