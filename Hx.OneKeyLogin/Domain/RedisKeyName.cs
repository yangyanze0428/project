using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public class RedisKeyName
    {
        /// <summary>
        /// 一键登录额度信息key
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static string CreateUserAmountKey(string userid) 
        {
            return $"set_onekey_sum_{userid}";
        }
    }
}
