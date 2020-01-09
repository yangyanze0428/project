using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hx.HttpClient
{
    public interface IHxHttpClient
    {
        Task<HttpResponseMessage> SendAsync(string url, string data, string contentType = "text/json");
    }
}
