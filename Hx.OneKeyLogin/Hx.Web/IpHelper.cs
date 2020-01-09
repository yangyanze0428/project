using System.Linq;
using System.Threading.Tasks;
using Hx.Extensions;

namespace Hx.Web
{
    public class IpHelper
    {

        /**
           私有IP：
             A类 10.0.0.0-10.255.255.255
             B类 172.16.0.0-172.31.255.255
             C类 192.168.0.0-192.168.255.255
             环回地址:127.0.0.1
         **/
        private static readonly long ABegin = GetIpNum("10.0.0.0");
        private static readonly long AEnd = GetIpNum("10.255.255.255");
        private static readonly long BBegin = GetIpNum("172.16.0.0");
        private static readonly long BEnd = GetIpNum("172.31.255.255");
        private static readonly long CBegin = GetIpNum("192.168.0.0");
        private static readonly long CEnd = GetIpNum("192.168.255.255");

        public static async Task<string> GetRealIpAsync()
        {
            return await Task.Run(() => GetRealIp());
        }
        /// <summary>
        /// 获取客户端真实的ip地址。
        /// </summary>
        /// <returns></returns>
        public static string GetRealIp()
        {
            //有X-Real-Ip头，则取此值
            var realIp = HttpContextAccessor.Current.Request.Headers["X-Real-IP"];
            if (IsIpAddress(realIp)) return realIp;

            //从代理列表中取第一个有效公网IP,有可能只是代理IP或假IP
            var allIp = HttpContextAccessor.Current.Request.Headers["X-FORWARDED-FOR"].Where(IsIpAddress).ToArray();
            if (allIp.Any())
            {
                realIp = allIp.FirstOrDefault(ip => !IsInnerIP(ip));
                if (!realIp.IsNullOrEmpty()) return realIp;
            }

            return HttpContextAccessor.Current.Connection.RemoteIpAddress.ToString();
        }
        /// <summary>
        /// 验证指定的值，是否为有效的ip格式。
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static bool IsIpAddress(string str)
        {
            if (string.IsNullOrWhiteSpace(str) || str.Length < 7 || str.Length > 15)
            { return false; }
            //const string regformat = @"^((?:(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d)))\.){3}(?:25[0-5]|2[0-4]\d|((1\d{2})|([1-9]?\d))))";//(\d(25[0-5]|2[0-4][0-9]|1?[0-9]?[0-9])\d\.){3}\d(25[0-5]|2[0-4][0-9]|1?[0-9]?[0-9])\d
            //var regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return Utilities.RegexHelper.IsIP(str);
        }

        /// <summary>
        /// 判断IP地址是否为内网IP地址
        /// </summary>
        /// <param name="ipAddress">IP地址字符串</param>
        /// <returns></returns>
        public static bool IsInnerIP(string ipAddress)
        {
            var ipNum = GetIpNum(ipAddress);
            return IsInner(ipNum, ABegin, AEnd) || IsInner(ipNum, BBegin, BEnd) || IsInner(ipNum, CBegin, CEnd) || ipAddress.Equals("127.0.0.1");
        }
        /// <summary>
        /// 把IP地址转换为Long型数字
        /// </summary>
        /// <param name="ipAddress">IP地址字符串</param>
        /// <returns></returns>
        private static long GetIpNum(string ipAddress)
        {
            var ip = ipAddress.Split('.');
            var a = long.Parse(ip[0]);
            var b = long.Parse(ip[1]);
            var c = long.Parse(ip[2]);
            var d = long.Parse(ip[3]);

            var ipNum = a * 256 * 256 * 256 + b * 256 * 256 + c * 256 + d;
            return ipNum;
        }
        /// <summary>
        /// 判断用户IP地址转换为Long型后是否在内网IP地址所在范围
        /// </summary>
        /// <param name="userIp"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static bool IsInner(long userIp, long begin, long end)
        {
            return (userIp >= begin) && (userIp <= end);
        }
    }
}