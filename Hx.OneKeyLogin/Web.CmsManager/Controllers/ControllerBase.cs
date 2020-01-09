using Domain.IService;
using Hx.Components;
using Hx.Logging;
using Hx.Serializing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Web.CmsManager.Models;
using Domain.Common.Enums;

namespace Web.CmsManager.Controllers
{
    [Authorize]
    public class ControllerBase : Controller
    {
        private ILogger _logger;
        protected ILogger Logger
        {
            get
            {
                if (_logger != null) return _logger;
                _logger = ObjectContainer.Resolve<ILoggerFactory>().Create("Web");
                return _logger;
            }
        }
        protected ISerializer Serializer
        {
            get
            {
                var serializer = ObjectContainer.Resolve<ISerializer>();
                return serializer;
            }
        }
        protected string UserId
        {
            get { return User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid))?.Value; }
        }

        private LoginUser _loginUser;
        protected LoginUser LoginInfo
        {
            get
            {
                if (_loginUser != null) return _loginUser;
                //如果 HttpContext.User.Identity.IsAuthenticated 为 true，或者 HttpContext.User.Claims.Count() > 0 表示用户已经登录
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var userId = User.FindFirst(ClaimTypes.Sid).Value;//读取userId
                    var userInfo = ObjectContainer.Resolve<IStaffService>().Get(userId);
                    var org = ObjectContainer.Resolve<IOrgService>().Get(userInfo.OrgId);
                    if (userInfo == null)
                    {
                        throw new Exception("invalid user");
                    }
                    var loginUser = new LoginUser
                    {
                        Id = userInfo.Id,
                        Name = userInfo.Name,
                        Role = userInfo.UserType,
                        Org = org
                    };
                    return loginUser;
                }
                _loginUser = new LoginUser();
                return _loginUser;
            }
        }

    }
}
