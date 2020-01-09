using Domain.Common.Dtos.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.ICacheManager
{
    public interface IAppCache: IDictionaryCache<AppDto>
    {
        void Init();
    }
}
