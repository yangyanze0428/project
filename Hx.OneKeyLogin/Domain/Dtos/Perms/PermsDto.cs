using Hx.Domain.Entities;
using System.Collections.Generic;

namespace Domain.Common.Dtos.Perms
{
    public class PermsDto
    {
        public string Id { get; set; }
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
    /// <summary>
    /// 查询条件
    /// </summary>
    public class PermsPara : PageBase
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Caption { get; set; }
    }

    /// <summary>
    /// 页面显示
    /// </summary>
    public class PermsDtoShow
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// 模块url
        /// </summary>
        public string Module { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public int IsEnable { get; set; }
        /// <summary>
        /// 父级Id
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        public List<PermsDtoShow> Children { get; set; } = new List<PermsDtoShow>();
    }
}
