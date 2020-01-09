using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.HttpClientFactory
{
    public interface IHxHttpClientFactory<IT,T> where T : class, IT
                                                                        where IT : class
    {
        IT CreateHttpClient();
    }
    public interface IHxHttpClientFactory : IHxHttpClientFactory<IHxHttpClient, HxHttpClient>
    {

    }
}
