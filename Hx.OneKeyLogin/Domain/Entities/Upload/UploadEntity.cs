using Hx.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Entities.Upload
{
   public class UploadEntity : AggregateRoot<long>
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 扩展名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Route { get; set; }
        public int OrderBy { get; set; }
    }
}
