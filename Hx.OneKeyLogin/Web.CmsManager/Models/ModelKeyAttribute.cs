using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ModelKeyAttribute : Attribute
    {
        public ModelKeyAttribute(string key)
        {
            Key = key;
        }

        public string Key { get; }
    }
    [Serializable]
    public class ViewAttribute : Attribute
    {
        /// <summary>
        /// 获取自定义的属性值
        /// </summary>
        public ViewAttribute(string field = null, string title = null, string width = null, Align align = Align.Left, Align halign = Align.Center, int order = 9999, bool hidden = false)//, bool display = false)
        {
            Field = field;
            Title = title;
            Width = width;
            Align = align;
            HeadAlign = halign;
            Order = order;
            Hidden = hidden;

        }


        /// <summary>
        /// 字段【显示内容时用到的字段】
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>

        public string Width { get; set; }

        /// <summary>
        /// 表内容对齐
        /// </summary>
        public Align Align { get; set; }

        /// <summary>
        ///表头对齐
        /// </summary>
        public Align HeadAlign { get; set; }

        public int Order { get; set; }

        public bool Hidden { get; set; }

    }

    public class IgnoreAttribute : Attribute
    {

    }
}
/// <summary>
/// 对齐
/// </summary>
public enum Align
{
    Left,

    Center,

    Right
}