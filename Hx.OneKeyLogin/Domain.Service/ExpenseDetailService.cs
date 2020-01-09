using Domain.Common.Dtos.Balance;
using Domain.Common.Entities.Expenses;
using Domain.Common.Enums;
using Domain.Common.Repositories;
using Domain.IService;
using Hx;
using Hx.Extensions;
using Hx.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service
{
    public class ExpenseDetailService : ServiceBase, IExpenseDetailService
    {
        private readonly IExpenseDetailRepository _expenseDetailRepository;
        private IOrgRepository _orgRepository;

        public ExpenseDetailService(IExpenseDetailRepository expenseDetailRepository, IOrgRepository orgRepository)
        {
            _expenseDetailRepository = expenseDetailRepository;
            _orgRepository = orgRepository;
        }
        public List<ExpenseDetailDto> GetList(UserType roleId, int orgId)
        {
            var rlt = UnitOfWorkService.Execute(() =>
            {
                var datas = _orgRepository.GetDatas<ExpenseDetailEntity>((int)roleId, orgId, DataType.ExpenseDetail);
                return datas?.MapTo<List<ExpenseDetailDto>>();
            });
            return rlt;
        }

        public List<ExpenseDetailDto> GetList(ExpenseDetailPara para, out int total)
        {
            try
            {
                var count = 0;
                var p = PredicateBuilder.Default<ExpenseDetailDto>();
                if (para.UserId.IsNotNullOrEmpty())
                {
                    p = p.And(m => m.UserId != null && m.UserId.Contains(para.UserId));
                }
                if (para.Mobile.IsNotNullOrEmpty())
                {
                    p = p.And(m => m.Mobile != null && m.Mobile.Contains(para.Mobile));
                }
                if (para.OperatorType != 0)//运营商类型
                {
                    p = p.And(m => m.OperatorType == para.OperatorType);
                }
                if (!string.IsNullOrEmpty(para.SalesMan))
                {
                    p = p.And(m => m.SalesMan.Contains(para.SalesMan));
                }
                if (para.StartTime > new DateTime())
                {
                    p = p.And(m => m.CreateDate >= para.StartTime);
                }
                if (para.EndTime > new DateTime())
                {
                    p = p.And(m => m.CreateDate < para.EndTime);
                }
                // var expenseDetails = UnitOfWorkService.Execute(() =>
                //{
                //    var list = _expenseDetailRepository.GetPaged(p, para.PageNumber, para.PageSize, out count, false, m => m.CreateDate);
                //    return list?.Select(c => c.MapTo<ExpenseDetailDto>()).ToList();
                //});
                // if (expenseDetails == null)
                // {
                //     total = 0;
                //     return new List<ExpenseDetailDto>();
                // }
                // total = count;
                // return expenseDetails;
                var expenseDetails = GetList(para.RoleId, para.OrgId);
                expenseDetails = expenseDetails.Where(p.Compile()).ToList();
                total = expenseDetails.Count;
                return expenseDetails.OrderByDescending(m => m.CreateDate).Skip(para.PageNumber * para.PageSize).Take(para.PageSize).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("select * from expenseDetails error", ex);
                total = 0;
                return new List<ExpenseDetailDto>();
            }
        }

        public IEnumerable<ExpenseStatis> Statis(string userId)
        {
            var list = UnitOfWorkService.Execute(() => _expenseDetailRepository.Statis(userId));
            return list;
        }
    }


}
