using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common.Dtos.MemberShip;
using Domain.Common.Repositories;
using Hx.Components;
using Hx.Domain.Services;
using Hx.ICacheManager;
using Hx.ObjectMapping;

namespace Hx.CacheManager
{
    public class CustomerCache : DictionaryCacheBase<CustomerDto>, ICustomerCache
    {
        public IUnitOfWorkService _unit { get; set; } = ObjectContainer.Resolve<IUnitOfWorkService>();

        public void Init()
        {

            try
            {
                GetDictionary();
            }
            catch (Exception ex)
            {
                Logger.Error("初始化customer错误", ex);
            }
        }
        object o = new object();
        public override void Set(string id, CustomerDto item)
        {
            lock (o)
            {
                base.Remove(id);
                base.Set(id, item);
            }
        }
        public override void Remove(string id)
        {
            lock (o)
            {
                base.Remove(id);
            }
        }
        protected override void Create(Dictionary<string, CustomerDto> dict)
        {
            var repo = ObjectContainer.Resolve<ICustomerRepository>();
            var list = _unit.Execute(() => repo.GetList()).ToList();
            foreach (var customer in list)
            {
                dict.Add(customer.Id, customer.MapTo<CustomerDto>());
            }
        }
    }
}