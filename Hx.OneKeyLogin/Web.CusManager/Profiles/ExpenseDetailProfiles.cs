using AutoMapper;
using Domain.Common.Dtos.Balance;
using Hx.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.CusManager.Models;

namespace Web.CusManager.Profiles
{
    public class ExpenseDetailProfiles : Profile
    {
        public ExpenseDetailProfiles()
        {
            CreateMap<ExpenseDetailDto, ExpenseDetailModel>()
                .ForMember(m => m.SuccessFormat, opt => opt.MapFrom(s => s.Success ? "成功" : "失败"))
                .ForMember(m => m.OperatorTypeDesc, opt => opt.MapFrom(s => s.OperatorType.GetDescription()))
                .ForMember(m => m.CreateDate, opt => opt.MapFrom(s => s.CreateDate.ToString19()));
        }
    }
}
