using Domain.Common;
using Domain.Common.Dtos.MemberShip;
using Domain.Common.Enums;
using System;
using System.Collections.Generic;

namespace Domain.IService
{
    public interface ICustomerService
    {
        Result Verify(string userName, string password);
        List<CustomerDto> CustomerPara(CustomerPara para, out int total);
        Result Add(CustomerDto dto, UserType roleId);
        Result Delete(string id);
        Result Pass(string id);
        Result Update(CustomerDto dto);
        CustomerDto Get(string userId);
        CustomerDto GetCache(string userId);
        List<CustomerModel> GetModels();

        List<CustomerDto> GetList();
        CustomerModel GetModel(string userId);
        List<CustomerDto> GetModels(CustomerPara para, out int total);
        void Set(CustomerDto dto);
        void Remove(string id);
        List<string> Audit(string[] msgs, AuditResult auditResult);
    }
}
