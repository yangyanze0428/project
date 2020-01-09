using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.CmsManager.Models
{
    /// <summary>
    /// 角色显示
    /// </summary>
    [Serializable]
    [ModelKey("role")]
    public class RoleModel: ViewModelBase
    {
        /// <summary>
        /// Id
        /// </summary>
        [View(Title = "编号", Hidden = true)]
        public string Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [View(Title = "名称", Width = "20%")]
        public string RoleName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [View(Title = "描述", Width = "40%")]
        public string Description { get; set; }

    }
}
