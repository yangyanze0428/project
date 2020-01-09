using Domain.Common;
using Domain.Common.Dtos.App;
using Domain.Common.Enums;
using System.Collections.Generic;

namespace Domain.IService
{
    public interface IAppService
    {
        List<AppDto> GetList();
        List<AppDto> GetList(AppPara para, out int total);
        AppDto GetAppCache(string appKey);
        void Set(AppDto dto);
        void Remove(string id);
        Result Add(AppDto dto, UserType roleId);
        Result Delete(string id);
        Result Update(AppDto dto);
        AppDto Get(string id);
        List<AppDto> GetList(UserType roleId, int orgId);
    }
}
