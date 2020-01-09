using System;
using System.Data;
using Hx.Domain.Uow;
using Oracle.ManagedDataAccess.Client;

namespace Hx.Module.Oracle
{
    public class OracleDbConnectionProvider : IDbConnectionProvider
    {
        public string Name { get; } = "oracle";

        public IDbConnection Create(string connectionString, bool ifOpen = false)
        {
            var conn = new OracleConnection { ConnectionString = connectionString };
            if (ifOpen) conn.Open();
            return conn;
        }
    }
}
