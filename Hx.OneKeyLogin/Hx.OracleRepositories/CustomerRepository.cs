using Dapper;
using Domain.Common.Entities.MemberShip;
using Domain.Common.Enums;
using Domain.Common.Repositories;
using Hx.Dapper.Repository;
using Hx.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hx.OracleRepositories
{
    public class CustomerRepository : DapperRepositoryBase<CustomerEntity, string>, ICustomerRepository
    {
        public CustomerRepository(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider) : base(currentUnitOfWorkProvider)
        {
        }
        //public IEnumerable<CustomerEntity> BatchUpdate(string[] msgs,AuditResult auditResult)
        //{
        //    var sql = @"update LOGIN_CUSTOMER set LOGIN_CUSTOMER.AUDITRESULT=@AuditResult where LOGIN_CUSTOMER.ID=@UserId";

        //    var users = new List<>
        //    var rlt = Connection.Execute<CustomerEntity>(sql,);
        //    Connection.Execute(sql,)
        //    return rlt;
        //}

        public IEnumerable<CustomerEntity> GetTest()
        {
            throw new NotImplementedException();
        }
    }
}
