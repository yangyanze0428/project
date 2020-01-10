using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Local.Api30.PolicyRequirement
{
    /// <summary>
    /// 自定义策略处理流程
    /// </summary>
    /// <remarks>AuthorizationHandler:抽象类  继承IAuthorizationHandler接口</remarks>
    public class MustRoleAdminHandler : AuthorizationHandler<AdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminRequirement requirement)
        {
            context.Succeed(requirement);//成功
            //context.Fail();//失败
            return Task.CompletedTask;
        }
    }
}
