using Hx.Domain.Entities;

namespace Domain.Common.Entities.Perms
{
    public class PermsEntity : AggregateRootWithStringKey
    {
        /// <summary>
        /// 权限名
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// 父id
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 模块url
        /// </summary>
        public string Module { get; set; }
        /// <summary>
        /// 顺序
        /// </summary>
        public int OrderBy { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 平台类型
        /// </summary>
        public int PlatFormType { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnable { get; set; }
        public string Icon { get; set; }

    }
}
