using Hx.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.HttpClientFactory
{
   public  class HttpClientModule:ModuleBase
    {
        public override void PreInitialize()
        {
            Register<IHxHttpClientFactory, HxHttpClientFactory>();
        }
    }
}
