using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Infra
{
    /// <summary>
    /// 格林威治时间与时间戳
    /// </summary>
   public  class TimeStamp
    {
        //TODO:此类以后移植到nuget基础类库中
        private static DateTimeOffset _utcStartTime = new DateTimeOffset(1970, 1, 1, 0, 0, 0,0, TimeSpan.Zero);
        /// <summary>
        /// 获取格林威治时间戳精确到秒
        /// </summary>
        /// <param name="utcTime">utc时间</param>
        /// <returns>时间戳</returns>
        public static string TimeStampSecondsString(DateTimeOffset utcTime) 
        {
            var ts= utcTime - _utcStartTime;
            string timestamp = Convert.ToInt64(ts.TotalSeconds).ToString();
            return timestamp;
        }

        /// <summary>
        /// 获取格林威治时间戳精确到秒
        /// </summary>
        /// <param name="utcTime"></param>
        /// <returns>long类型</returns>
        public static long TimeStampSecondsLong(DateTimeOffset utcTime)
        {
            var ts = utcTime - _utcStartTime;
            //TimeSpan ts = utcTime - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var timestamp = Convert.ToInt64(ts.TotalSeconds);
            return timestamp;
        }
        /// <summary>
        /// 获取当前时间时间戳，long
        /// </summary>
        /// <returns></returns>
        public static long CurrentTimeStampSecondsLong()
        {
            return TimeStampSecondsLong(DateTimeOffset.UtcNow);
        }
        /// <summary>
        /// 获取当前时间时间戳
        /// </summary>
        /// <returns></returns>
        public static string CurrentTimeStampSecondsString() 
        {
            return TimeStampSecondsString(DateTimeOffset.UtcNow);
        }
        /// <summary>
        /// 获取格林威治时间戳以毫秒为单位
        /// </summary>
        /// <param name="utc"></param>
        /// <returns></returns>
        public static string TimeStampMillisecondString(DateTimeOffset utc)
        {
            TimeSpan ts = utc - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string timestamp = Convert.ToInt64(ts.TotalMilliseconds).ToString();
            return timestamp;
        }
        public static long TimeStampMillisecondLong(DateTimeOffset utc) 
        {
            TimeSpan ts = utc - _utcStartTime;
            return Convert.ToInt64(ts.TotalMilliseconds);
        }

        /// <summary>
        /// 格林威治时间戳转化为时间以秒为单位
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime GetLocalDateTimeSeconds(string timeStampSeconds)
        {
            DateTime dtStart = _utcStartTime.LocalDateTime;
            long lTime = long.Parse(timeStampSeconds + "0000000");
            TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);
        }
        /// <summary>
        /// 格林威治时间戳转换为时间，以毫秒为单位
        /// </summary>
        /// <param name="timeStampMilliseconds"></param>
        /// <returns></returns>
        public static DateTime GetLocalDateTimeMilliseconds(string timeStampMilliseconds)
        {
            DateTime dtStart = _utcStartTime.LocalDateTime;
            long lTime = long.Parse(timeStampMilliseconds + "0000");
            TimeSpan toNow = new TimeSpan(lTime); return dtStart.Add(toNow);
        }
    }
}
