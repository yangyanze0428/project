using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hx.HttpClient
{
    public class HxHttpClient : IHxHttpClient
    {
        public Task<HttpResponseMessage> SendAsync(string url, string data, string contentType = "text/json")
        {
            throw new NotImplementedException();
        }
    }
}
