using Domain.Common.Dtos.Balance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hx.Extensions;
using Web.CmsManager.Models;

namespace Web.CmsManager.Profile
{
    public class ExpenseDetailProfile : AutoMapper.Profile
    {
        public ExpenseDetailProfile()
        {
            CreateMap<ExpenseDetailDto, ExpenseDetailModel>()
                .ForMember(d => d.CreateDate, opt => opt.MapFrom(s => s.CreateDate.ToString19()))
                .ForMember(d => d.OperatorType, opt => opt.MapFrom(s => s.OperatorType.GetDescription()))
                .ForMember(d => d.Success, opt => opt.MapFrom(s => s.Success ? "成功" : "失败"))
                .ForMember(d => d.Value, opt => opt.MapFrom(s => Math.Abs(s.Value)));
        }
    }
}
