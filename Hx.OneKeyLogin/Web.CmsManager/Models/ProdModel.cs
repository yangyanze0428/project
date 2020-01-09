using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.CmsManager.Models
{
    [ModelKey("prod")]
    public class ProdModel : ViewModelBase
    {
        [View(Title = "id",Hidden =true)]
        public string Id { get; set; }

        [View(Title = "客户编号")]
        public string UserId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [View(Title = "名称")]
        public string Name { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [View(Title = "金额")]
        public int Money { get; set; }
        /// <summary>
        /// 数量/条数
        /// </summary>
        [View(Title = "条数")]
        public int Value { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        [View(Title = "单价")]
        public decimal Price { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        [View(Title = "总条数")]
        public int Total { get; set; }
        [View(Title = "业务员")]
        public string SalesMan { get; set; }
        /// <summary>
        /// 创建者id
        /// </summary>
        [View(Title = "创建者")]
        public string CreatorId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [View(Title = "创建时间")]
        public string CreateDate { get; set; }
        [View(Title = "备注")]
        public string Remark { get; set; }
    }
}
