using Hx.Dapper;
using Hx.Jwt;
using Hx.Modules;
using Hx.ObjectMapping;
using Hx.Runtime.Caching.Memory;
using Hx.Runtime.Caching.Redis;
using Hx.Utilities.SimilarityCalculator;
using Hx.Utilities.SimilarityCalculator.String;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    [DependsOn(
        typeof(BasicModule),
        typeof(DirectMemoryCacheModule),
        typeof(RedisCacheModule),
        typeof(DapperModule),
        typeof(AutoMapperModule),
        typeof(JwtModule)
    )]
    public class DomainModule:ModuleBase
    {
        
    }
}
