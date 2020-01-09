using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hx.Extensions;
using Microsoft.AspNetCore.Html;
using System.ComponentModel;
using Domain.Common.Enums;

namespace Web.CusManager
{
    public delegate object SelfApplicable<T>(SelfApplicable<T> self, T arg);
    public static class WebExtensions
    {
        public static string Caption(this IHtmlHelper htmlHelper, PageResource resource)
        {
            return resource.GetDescription();
        }
        public static string Caption(this IHtmlHelper htmlHelper, AuditResult resource)
        {
            return resource.GetDescription();
        }
        public static object Render<T>(this IHtmlHelper helper, T model, SelfApplicable<T> f)
        {
            return f(f, model);
        }

        private static Action<T> Fix<T>(Func<Action<T>, Action<T>> f)
        {
            return x => f(Fix(f))(x);
        }

        public static void Render<T>(this IHtmlHelper helper, T model, Func<Action<T>, Action<T>> f)
        {
            Fix(f)(model);
        }
        public static IHtmlContent EnumDropDownFor(this IHtmlHelper helper, Type enumType, string id, object htmlAttributes = null)
        {
            var items = ToSelectList(enumType);

            return helper.Raw(CreateDrawDownHtml(items, id, htmlAttributes));
        }
        public static IHtmlContent EnumDropDownFor(this IHtmlHelper helper, Enum enumValue, string id, object htmlAttributes = null)
        {
            var items = ToSelectList(enumValue);

            return helper.Raw(CreateDrawDownHtml(items, id, htmlAttributes));
        }

        private static string CreateDrawDownHtml(IEnumerable<SelectListItem> items, string id, object htmlAttributes)
        {
            var sb = new StringBuilder();

            sb.Append($"<select id=\"{id}\" name=\"{id}\" ");
            if (htmlAttributes != null)
            {
                foreach (var attribute in htmlAttributes.GetType().GetProperties())
                {
                    sb.Append($" {FormatHtmlAttributeName(attribute.Name)}=\"{FormatHtmlAttributeValue(attribute.GetValue(htmlAttributes))}\"");
                }
            }

            sb.Append(">");
            foreach (var item in items)
            {
                if (item.Selected)
                {
                    sb.Append($"<option selected='selected' value='{item.Value}'>{item.Text}</option>");
                }
                else
                {
                    sb.Append($"<option value='{item.Value}'>{item.Text}</option>");
                }
            }
            sb.Append("</select>");
            return sb.ToString();
        }

        private static string FormatHtmlAttributeName(string attrName)
        {
            return attrName.Replace("_", "-");
        }
        private static string FormatHtmlAttributeValue(object attrValue)
        {
            return attrValue.ToString().Replace("\"", "&quot;");
        }
        public static IEnumerable<SelectListItem> GetEnumDropDownList(this IHtmlHelper htmlHelper, Type enumType)
        {
            return ToSelectList(enumType);
        }
        public static IEnumerable<SelectListItem> GetEnumDropDownList(this IHtmlHelper htmlHelper, Enum enumValue)
        {
            return ToSelectList(enumValue);
        }
        private static IEnumerable<SelectListItem> ToSelectList(this Type enumType)
        {
            return from Enum e in Enum.GetValues(enumType)
                   select new SelectListItem
                   {
                       Selected = false,
                       Text = e.GetDescription(),
                       Value = (Convert.ToInt32(e)).ToString()
                   };
        }
        private static IEnumerable<SelectListItem> ToSelectList(this Enum enumValue)
        {
            return from Enum e in Enum.GetValues(enumValue.GetType())
                   select new SelectListItem
                   {
                       Selected = e.Equals(enumValue),
                       Text = e.GetDescription(),
                       Value = (Convert.ToInt32(e)).ToString()
                   };
        }
    }
    public enum PageResource
    {
        [Description("新增")]
        ButtonNew,

        [Description("删除")]
        ButtonDelete,

        [Description("编辑")]
        ButtonEdit,

        [Description("查询")]
        ButtonSearch,

        [Description("提交")]
        ButtonSubmit,

        [Description("重置")]
        ButtonReset,

        [Description("确定")]
        ButtonOk,

        [Description("保存")]
        ButtonSave,

        [Description("取消")]
        ButtonCancel,

        [Description("退出")]
        ButtonQuit
    }
}
