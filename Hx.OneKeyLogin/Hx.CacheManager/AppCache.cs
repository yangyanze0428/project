using Domain.Common.Dtos.App;
using Domain.Common.Repositories;
using Hx.Components;
using Hx.Domain.Services;
using Hx.ICacheManager;
using Hx.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hx.CacheManager
{
    public class AppCache : DictionaryCacheBase<AppDto>, IAppCache
    {
        IUnitOfWorkService _unitOfWork;
        public AppCache(IUnitOfWorkService unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        object o = new object();
        protected override void Create(Dictionary<string, AppDto> dict)
        {
            var repo = ObjectContainer.Resolve<IAppRepository>();
            var list = _unitOfWork.Execute(() => repo.GetList()).ToList();
            foreach (var app in list)
            {
                dict.Add(app.Id, app.MapTo<AppDto>());
            }
        }
        public override void Set(string id, AppDto item)
        {
            lock (o)
            {
                base.Remove(id);
                base.Set(id,item);
            }
        }
        public override void Remove(string id)
        {
            lock (o)
            {
                base.Remove(id);
            }
        }
        public void Init() 
        {
            try
            {
                GetDictionary();
            }
            catch (Exception ex)
            {
                Logger.Error("初始化app错误", ex);
            }
        }
    }
}
