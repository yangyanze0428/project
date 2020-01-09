using DapperExtensions;
using Hx.Modules;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Hx.Db.Oracle
{
    [DependsOn(typeof(KernelModule))]
    public class DapperMapperModule: ModuleBase
    {
        public override Assembly[] GetAdditionalAssemblies()
        {
            return new[] { typeof(Predicates).Assembly };
        }
    }
}
