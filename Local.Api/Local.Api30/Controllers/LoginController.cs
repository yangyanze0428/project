using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Local.Api30.Helper;
using Local.Api30.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Local.Api30.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public async Task<object> GetJwtStr(string name, string pass)
        {
            return await Task.Run(() =>
            { // 将用户id和角色名，作为单独的自定义变量封装进 token 字符串中。
                TokenModelJwt tokenModel = new TokenModelJwt { Uid = 1, Role = "Admin" };
                var jwtStr = JwtHelper.IssueJwt(tokenModel);//登录，获取到一定规则的 Token 令牌
                var succ = true;
                return Ok(new { success = succ, token = jwtStr });
            });
        }
    }
}