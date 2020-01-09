using Hx.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Web.Models;

namespace Web.CmsManager.Models
{
    public class GridCreator
    {
        public static List<GridColumn> Create(Type schemeType)
        {
            var props = schemeType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var cols = new List<GridColumn>();
            foreach (var prop in props)
            {
                var col = new GridColumn();
                var attrs = prop.GetCustomAttributes<ViewAttribute>();
                var customAttributes = attrs as ViewAttribute[] ?? attrs.ToArray();
                if (customAttributes.Any())
                {
                    var attr = customAttributes[0];
                    col.Field = attr.Field ?? prop.Name.ToCamelCase();
                    col.Title = attr.Title ?? prop.Name;
                    col.Align = attr.Align.ToString().ToLower();
                    col.HeadAlign = attr.HeadAlign.ToString().ToLower();
                    col.Width = attr.Width ?? "";
                    col.Hidden = attr.Hidden;
                    col.Order = attr.Order;
                    //cols.Add(col);
                }
                else
                {
                    col.Field = prop.Name.ToCamelCase();
                    col.Title = prop.Name;
                }
                cols.Add(col);
            }
            return cols.OrderBy(item => item.Order).ToList();
        }
        public static List<GridColumn> Create<T>()
        {
            return Create(typeof(T));
        }
    }
    public class QueryAttribute
    {
        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="peroperties"></param>
        /// <returns></returns>
        public List<ViewAttribute> GetAttrContent(PropertyInfo[] peroperties)
        {
            var attrList = new List<ViewAttribute>();
            foreach (var property in peroperties)
            {
                var attrs = property.GetCustomAttributes(true);
                if (attrs.Length > 0)
                {
                    var strField = "";
                    var strTitle = "";
                    var strWidth = "";
                    var strAlign = Align.Left;
                    var strHeadAlign = Align.Center;
                    foreach (object attr in attrs)
                    {
                        if (attr is ViewAttribute)
                        {
                            strField = ((ViewAttribute)attr).Field;
                            strTitle = ((ViewAttribute)attr).Title;
                            strWidth = ((ViewAttribute)attr).Width;
                            strAlign = ((ViewAttribute)attr).Align;
                            strHeadAlign = ((ViewAttribute)attr).HeadAlign;
                        }
                    }
                    attrList.Add(new ViewAttribute
                    {
                        Field = strField,
                        Title = strTitle,
                        Width = strWidth,
                        Align = strAlign,
                        HeadAlign = strHeadAlign,
                    });
                }
            }
            return attrList;
        }
    }
}
