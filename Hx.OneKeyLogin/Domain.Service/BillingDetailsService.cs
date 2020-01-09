using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Common.Dtos.Balance;
using Domain.Common.Entities.Expenses;
using Domain.Common.Enums;
using Domain.Common.Repositories;
using Domain.IService;
using Hx;
using Hx.Extensions;
using Hx.ObjectMapping;

namespace Domain.Service
{
    public class BillingDetailsService : ServiceBase, IBillingDetailsService
    {
        private readonly IOrgRepository _orgRepository;
        private IBillingDetailsRepository _billingDetailsRepository;

        public BillingDetailsService(IOrgRepository orgRepository, IBillingDetailsRepository billingDetailsRepository)
        {
            _orgRepository = orgRepository;
            _billingDetailsRepository = billingDetailsRepository;
        }

        public List<BillingDetailsDto> GetList(UserType roleId, int orgId)
        {
            var rlt = UnitOfWorkService.Execute(() =>
            {
                var data = _orgRepository.GetDatas<BillingDetailsEntity>((int)roleId, orgId, DataType.BillingDetails);
                return data?.MapTo<List<BillingDetailsDto>>();
            });
            return rlt;
        }
        public List<BillingDetailsDto> GetList(BillingDetailsPara para, out int total)
        {
            try
            {
                var p = PredicateBuilder.Default<BillingDetailsDto>();
                if (para.UserId.IsNotNullOrEmpty())
                {
                    p = p.And(m => m.UserId != null && m.UserId.Contains(para.UserId));
                }
                if (para.AppKey.IsNotNullOrEmpty())
                {
                    p = p.And(m => m.AppKey.Equals(para.AppKey));
                }
                if (para.StartTime > new DateTime())
                {
                    p = p.And(m => m.CreateDate >= para.StartTime);
                }
                if (para.EndTime > new DateTime())
                {
                    p = p.And(m => m.CreateDate < para.EndTime);
                }
                var details = GetList(para.RoleId, para.OrgId);
                details = details.Where(p.Compile()).ToList();
                total = details.Count;
                return details.OrderByDescending(m => m.CreateDate).Skip(para.PageNumber * para.PageSize).Take(para.PageSize).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("select * from BillingDetails error", ex);
                total = 0;
                return new List<BillingDetailsDto>();
            }
        }

        public IEnumerable<BillingDetailsStatis> Statis(string userId,DateTime startTime,DateTime endTime)
        {
            try
            {
                var list = UnitOfWorkService.Execute(() => _billingDetailsRepository.Statis(userId,startTime,endTime));
                return list;
            }
            catch (Exception ex)
            {
                Logger.Error("select * from BillingDetailsStatis error", ex);
                return null;
            }
        }
    }
}
