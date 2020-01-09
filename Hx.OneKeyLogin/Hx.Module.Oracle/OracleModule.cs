using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Loader;
using System.Text;
using DapperExtensions.Sql;
using Hx.Components;
using Hx.Dapper;
using Hx.Modules;

namespace Hx.Module.Oracle
{
    public class OracleModule:ModuleBase
    {
        public override void PreInitialize()
        {
#if NETSTANDARD

            try
            {
                //Assembly.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Oracle.ManagedDataAccess.dll"));

                AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Oracle.ManagedDataAccess.dll"));
                Console.WriteLine("OracleModule load Oracle.ManagedDataAccess.dll");
                var config = ObjectContainer.Resolve<IDapperConfiguration>();
                config.AddDbConnectionProvider<OracleDbConnectionProvider>("oracle");
                DapperExtensions.DapperExtensions.SqlDialect = new OracleDialect();
                Logger.Debug($"OracleModule loaded at {DateTime.Now}");
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
#endif
        }

        public override void Initialize()
        {
            
        }
    }
}
