using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service.Handler
{
    /// <summary>
    /// 11:表示新增AppInfo 12:表示修改AppInfo 13:表示删除AppInfo
    /// 21:表示新增CustomerDto 22:表示修改CustomerDto 23:表示删除CustomerDto
    /// </summary>
    public class CusNotification:INotification
    {
        public int Method { get; set; }
        public string Body { get; set; }
    }
}
