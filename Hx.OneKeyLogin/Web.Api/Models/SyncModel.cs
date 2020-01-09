using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Api.Models
{
    /// <summary>
    /// 11:表示新增AppInfo 12:表示修改AppInfo 13:表示删除AppInfo
    /// </summary>
    public class SyncModel
    {
        public int Method { get; set; }
        public string Body { get; set; }
    }
}
