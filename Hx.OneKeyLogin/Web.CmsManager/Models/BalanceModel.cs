using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.CmsManager.Models
{
    [ModelKey("balance")]
    public class BalanceModel : ViewModelBase
    {
        [View(Hidden = true)]
        public int Id { get; set; }
        [View(Title = "客户账号")]
        public string UserId { get; set; }
        [View(Title = "修改前的条数")]
        public int BeforeValue { get; set; }
        [View(Title = "剩余条数")]
        public int Value { get; set; }
        [View(Title = "操作人",Hidden =true)]
        public string OperatorId { get; set; }
        [View(Title = "修改时间")]
        public string ModifyDate { get; set; }
        [View(Title = "备注")]
        public string Remark { get; set; }
    }
}
