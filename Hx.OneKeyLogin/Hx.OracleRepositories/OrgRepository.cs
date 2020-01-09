using Dapper;
using Domain.Common.Entities.MemberShip;
using Domain.Common.Enums;
using Domain.Common.Repositories;
using Hx.Components;
using Hx.Dapper.Repository;
using Hx.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hx.OracleRepositories
{
    public class OrgRepository : DapperRepositoryBase<OrgEntity, long>, IOrgRepository
    {
        public OrgRepository(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(currentUnitOfWorkProvider)
        {
        }

        public OrgEntity GetOrgByUser(string userId)
        {
            var sql = @"select a.* from login_org a join login_staff b on a.id=b.orgid join login_role c on b.usertype=c.id where b.id=:id";
            var rlt = Connection.Query<OrgEntity>(sql, new { id = userId });
            return rlt.FirstOrDefault();
        }

        public List<TEntity> GetDatas<TEntity>(int roleId, int orgId, DataType type)
        {
            var tableName = GetTableName(type);
            var sql = $@"select distinct a.* from {tableName} a 
                        join login_datainorg d on a.id=d.dataid
                        join login_org g on d.orgid=g.id
                        --join login_role r on d.roleid=r.id
                        where d.datatype=:type";
            if (orgId > 0)
            {
                sql += " and g.id =:orgId";
            }
            var rlt = Connection.Query<TEntity>(sql, new {/* roleId = roleId, */orgId = orgId, type = type });
            return rlt.ToList();
        }
        public List<TEntity> GetData<TEntity>(int roleId, int orgId, DataType type)
        {
            var sql = $@"select distinct a.* from login_balance a 
                        join login_datainorg d on a.userid=d.dataid
                        join login_org g on d.orgid=g.id
                        --join login_role r on d.roleid=r.id
                        where d.datatype=:type";
            if (orgId > 0)
            {
                sql += " and g.id =:orgId";
            }
            var rlt = Connection.Query<TEntity>(sql, new {/* roleId = roleId, */orgId = orgId, type = type });
            return rlt.ToList();
        }


        private string GetTableName(DataType type)
        {
            var tableName = string.Empty;
            switch (type)
            {
                case DataType.App:
                    tableName = "login_app";
                    break;
                case DataType.Authenty:
                    tableName = "login_authenty";
                    break;
                case DataType.Customer:
                    tableName = "login_customer";
                    break;
                case DataType.ExpenseDetail:
                    tableName = "login_expensedetail";
                    break;
                case DataType.Recharge:
                    tableName = "login_recharge";
                    break;
                case DataType.Balance:
                    tableName = "login_balance";
                    break;
                case DataType.Staff:
                    tableName = "login_staff";
                    break;
                case DataType.BillingDetails:
                    tableName = "login_billingdetails";
                    break;
                case DataType.Prod:
                    tableName = "login_prod";
                    break;
                default:
                    throw new Exception("not exists DataType");
            }
            return tableName;
        }
    }
}
