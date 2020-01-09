using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hx.HttpClientFactory
{
    public interface IHxHttpClient
    {
        Task<HttpResponseMessage> SendAsync(string url, string data, string contentType = "application/json");
    }
}
