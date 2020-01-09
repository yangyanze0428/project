using Hx;
using Hx.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Web.CusManager.Models;

namespace Web.CusManager.Helper
{
    public static class ModelSchemeHelper
    {
        public static Dictionary<string, List<GridColumn>> Schemes { get; } = new Dictionary<string, List<GridColumn>>();


        public static void Register(Type type)
        {
            var key = GetModelKey(type);
            if (key.IsNullOrEmpty()) throw new HxException($"{type.AssemblyQualifiedName} 没有找到ModelKey定义");
            if (Schemes.ContainsKey(key)) throw new HxException($"ModelKey 存在重复定义：{key}");
            var scheme = GridCreator.Create(type);
            Schemes.TryAdd(key, scheme);
        }

        private static string GetModelKey(Type type)
        {
            var attr = type.GetCustomAttribute<ModelKeyAttribute>(false);
            return attr == null ? "" : attr.Key;
        }
    }
}
