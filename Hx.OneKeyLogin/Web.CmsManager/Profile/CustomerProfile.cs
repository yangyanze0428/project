using Hx.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.CmsManager.Models;

namespace Web.CmsManager.Profile
{
    public class CustomerProfile: AutoMapper.Profile
    {
        public CustomerProfile()
        {
            CreateMap<Domain.Common.Dtos.MemberShip.CustomerModel, CustomerModel>()
                .ForMember(d => d.CreateDate, opt => opt.MapFrom(s => s.CreateDate.ToString19()))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.GetDescription()))
             //.ForMember(d => d.AuditResultDesc, opt => opt.MapFrom(s => s.AuditResult.GetDescription()))
             .ForMember(d => d.AuthCreateDate, opt => opt.MapFrom(s => s.AuthCreateDate.ToString19()));

            CreateMap<Domain.Common.Dtos.MemberShip.CustomerDto, CustomerModel>()
                .ForMember(d => d.CreateDate, opt => opt.MapFrom(s => s.CreateDate.ToString19()))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.GetDescription()));
             //.ForMember(d => d.AuditResultDesc, opt => opt.MapFrom(s => s.AuditResult.GetDescription()))
        }
    }
}
