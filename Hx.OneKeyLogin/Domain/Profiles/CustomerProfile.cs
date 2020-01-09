using AutoMapper;
using Domain.Common.Dtos.MemberShip;
using Domain.Common.Entities.MemberShip;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Profiles
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDto,CustomerEntity>();
            CreateMap<CustomerEntity, CustomerDto>();

            CreateMap<CustomerEntity, CustomerModel>();
            CreateMap<CustomerModel, CustomerEntity>();
        }
    }
}
