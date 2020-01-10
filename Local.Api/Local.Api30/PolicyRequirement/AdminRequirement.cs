using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Local.Api30.PolicyRequirement
{
    /// <summary>
    /// 自定义操作对象
    /// </summary>
    public class AdminRequirement:IAuthorizationRequirement
    {
        public string Name { get; set; }
    }
}