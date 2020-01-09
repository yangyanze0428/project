using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Domain.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.CmsManager
{
    public class EnumExt
    {
        /// <summary>

        /// 根据枚举成员获取自定义属性EnumDisplayNameAttribute的属性DisplayName

        /// </summary>

        /// <param name="e"></param>

        /// <returns></returns>

        public static string GetEnumCustomDescription(object e)

        {

            //获取枚举的Type类型对象

            Type t = e.GetType();



            //获取枚举的所有字段

            FieldInfo[] ms = t.GetFields();



            //遍历所有枚举的所有字段

            foreach (FieldInfo f in ms)

            {

                if (f.Name != e.ToString())

                {

                    continue;

                }



                //第二个参数true表示查找EnumDisplayNameAttribute的继承链

                if (f.IsDefined(typeof(EnumDisplayNameAttribute), true))

                {

                    return

                        (f.GetCustomAttributes(typeof(EnumDisplayNameAttribute), true)[0] as EnumDisplayNameAttribute)

                            .DisplayName;

                }

            }



            //如果没有找到自定义属性，直接返回属性项的名称

            return e.ToString();

        }



        /// <summary>

        /// 根据枚举，把枚举自定义特性EnumDisplayNameAttribut的Display属性值撞到SelectListItem中

        /// </summary>

        /// <param name="enumType">枚举</param>

        /// <returns></returns>

        public static List<SelectListItem> GetSelectList(Type enumType)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (object e in Enum.GetValues(enumType))
            {
                selectList.Add(new SelectListItem() { Text = EnumHelper.GetEnumDescription((Enum)e), Value = ((int)e).ToString() });
            }
            return selectList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idTexts"></param>
        /// <param name="selectid">被选着的id</param>
        /// <returns></returns>
        public static SelectList GetSelectLsit(List<IdText> idTexts, string selectid)
        {
            return new SelectList(idTexts, "Id", "Text", selectid);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumType">枚举类型type对象</param>
        /// <param name="selectedId">被选择的optionsid</param>
        /// <returns></returns>
        public static SelectList GetSelectList(Type enumType, string selectedId)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(enumType))
            {
                var text = EnumHelper.GetEnumDescription((Enum)item);
                var value = ((int)item).ToString();
                selectList.Add(new SelectListItem() { Text = text, Value = value, Selected = value.Equals(selectedId) });
            }
            return new SelectList(selectList, "Value", "Text", selectedId);
        }
        public static SelectList GetTextSelectList(Type enumType, string selectedId)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(enumType))
            {
                var text = EnumHelper.GetEnumDescription((Enum)item);
                var value = item.ToString();
                selectList.Add(new SelectListItem() { Text = text, Value = value, Selected = value.Equals(selectedId) });
            }
            return new SelectList(selectList, "Value", "Text", selectedId);
        }
    }
}
