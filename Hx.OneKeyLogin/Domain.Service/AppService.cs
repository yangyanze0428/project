using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common;
using Domain.Common.Dtos.App;
using Domain.Common.Dtos.Perms;
using Domain.Common.Entities.Application;
using Domain.Common.Entities.Perms;
using Domain.Common.Enums;
using Domain.Common.Repositories;
using Domain.IService;
using Domain.Service.Handler;
using Hx;
using Hx.Extensions;
using Hx.HttpClientFactory;
using Hx.ICacheManager;
using Hx.ObjectMapping;
using Microsoft.Extensions.Configuration;

namespace Domain.Service
{
    public class AppService : ServiceBase, IAppService
    {
        private readonly IAppRepository _repo;
        IAppCache _appCache;
        private IHxHttpClientFactory _hxHttpClientFactory;
        private IConfiguration _configuration;
        private IOrgRepository _orgRepository;
        private IDataInOrgRepository _dataInOrgRepository;
        private IStaffRepository _staffRepository;
        public AppService(IAppRepository repo, IAppCache appCache, IHxHttpClientFactory hxHttpClientFactory, IConfiguration configuration, IOrgRepository orgRepository, IDataInOrgRepository dataInOrgRepository, IStaffRepository staffRepository)
        {
            _appCache = appCache;
            _hxHttpClientFactory = hxHttpClientFactory;
            _configuration = configuration;
            _repo = repo;
            _orgRepository = orgRepository;
            _dataInOrgRepository = dataInOrgRepository;
            _staffRepository = staffRepository;
        }

        public AppDto GetAppCache(string appKey)
        {
            return _appCache.Get(appKey);
        }

        public List<AppDto> GetList(UserType roleId, int orgId)
        {
            var rlt = UnitOfWorkService.Execute(() =>
            {
                var datas = _orgRepository.GetDatas<AppEntity>((int)roleId, orgId, DataType.App);
                return datas?.MapTo<List<AppDto>>();
            });
            return rlt;
        }

        public List<AppDto> GetList()
        {
            var list = UnitOfWorkService.Execute(() => _repo.GetList()?.ToList());
            return list.Select(m => m.MapTo<AppDto>()).ToList();
        }

        public List<AppDto> GetList(AppPara para, out int total)
        {
            try
            {
                var apps = GetList(para.RoleId, para.OrgId);
                var p = PredicateBuilder.Default<AppDto>();
                if (para.UserId.IsNotNullOrEmpty())
                {
                    p = p.And(m => m.UserId != null && m.UserId.Contains(para.UserId));
                }
                if (para.Id.IsNotNullOrEmpty())
                {
                    p = p.And(m => m.Id.Equals(para.Id));
                }
                if (para.AppName.IsNotNullOrEmpty())
                {
                    p = p.And(m => m.AppName.Contains(para.AppName));
                }
                if (para.SalesMan.IsNotNullOrEmpty())
                {
                    p = p.And(m => m.SalesMan.Contains(para.SalesMan));
                }
                if (para.AppType != 0)
                {
                    p = p.And(m => m.AppType == para.AppType);
                }
                if (para.StartTime > new DateTime())
                {
                    p = p.And(m => m.CreateDate >= para.StartTime);
                }
                if (para.EndTime > new DateTime())
                {
                    p = p.And(m => m.CreateDate < para.EndTime);
                }
                //var apps = UnitOfWorkService.Execute(() =>
                //{
                //    var list = _repo.GetPaged(p, para.PageNumber, para.PageSize, out count, false, m => m.CreateDate);
                //    return list?.Select(c => c.MapTo<AppDto>()).ToList();
                //});

                //if (apps == null)
                //{
                //    total = 0;
                //    return new List<AppDto>();
                //}
                apps = apps.Where(p.Compile()).ToList();
                total = apps.Count;
                var rlt = apps.OrderByDescending(m => m.CreateDate).Skip(para.PageNumber * para.PageSize).Take(para.PageSize).ToList();
                return rlt;
            }
            catch (Exception e)
            {
                Logger.Error("select * from app error", e);
                total = 0;
                return new List<AppDto>();
            }
        }

        public Result Add(AppDto dto, UserType roleId)//TODO:缓存同步
        {
            try
            {
                var id = "";
                var entity = dto.MapTo<AppEntity>();
                var rlt = UnitOfWorkService.Execute(() =>
                {
                    id = _repo.InsertAndGetId(entity);
                    var orgId = _staffRepository.GetOrgId(dto.SalesMan).OrgId;
                    var dataInOrg = new DataInOrgEntity
                    {
                        DataId = id,
                        DataType = (int)DataType.App,
                        OrgId = orgId,
                        RoleId = ((int)roleId).ToString()
                    };
                    var dataRlt = _dataInOrgRepository.Inserts(dataInOrg);

                    return id.IsNotNullOrEmpty() && dataRlt;
                });

                if (!rlt) return new Result { Status = false, Message = "数据库操作失败" };
                dto.Id = id;
                var json = Serializer.Serialize(dto);
                var url = _configuration.GetValue("WebApi:url", "http://localhost:15002/sync");
                var http = _hxHttpClientFactory.CreateHttpClient();
                http.SendAsync(url, Serializer.Serialize(new CusNotification { Method = 11, Body = json }));
                Logger.Info($"app同步结果add:{json}");
                //_mediator.Publish(syncModel);
                return new Result { Status = true };
            }
            catch (Exception ex)
            {
                Logger.Error("add app error", ex);
                return new Result { Status = false, Message = "内部服务器错误" };
            }
        }

        public Result Delete(string id)
        {
            try
            {
                var rlt = UnitOfWorkService.Execute(() => _repo.Delete(id));
                if (!rlt) return new Result { Status = false, Message = "数据库操作失败" };
                var url = _configuration.GetValue("WebApi:url", "http://localhost:15002/sync");
                var http = _hxHttpClientFactory.CreateHttpClient();
                http.SendAsync(url, Serializer.Serialize(new CusNotification { Method = 13, Body = id }));
                Logger.Info($"app同步结果del:{id}");
                return new Result { Status = true };
            }
            catch (Exception ex)
            {
                Logger.Error("delete app error", ex);
                return new Result { Status = false, Message = "内部服务器错误" };
            }
        }

        public Result Update(AppDto dto)
        {
            try
            {
                var entity = dto.MapTo<AppEntity>();
                var rlt = UnitOfWorkService.Execute(() => _repo.Update(entity));
                if (!rlt) return new Result { Status = false, Message = "数据库操作失败" };
                var json = Serializer.Serialize(dto);
                var url = _configuration.GetValue("WebApi:url", "http://localhost:15002/sync");
                var http = _hxHttpClientFactory.CreateHttpClient();
                http.SendAsync(url, Serializer.Serialize(new CusNotification { Method = 12, Body = json }));
                Logger.Info($"app同步结果update:{json}");
                return new Result { Status = true };
            }
            catch (Exception ex)
            {
                Logger.Error("update app error", ex);
                return new Result { Status = false, Message = "内部服务器错误" };
            }
        }

        public AppDto Get(string id)
        {
            var entity = UnitOfWorkService.Execute(() => _repo.Get(id));
            return entity?.MapTo<AppDto>();
        }

        public void Remove(string id)
        {
            _appCache.Remove(id);
        }

        public void Set(AppDto dto)
        {
            _appCache.Set(dto.Id, dto);
        }
    }
}
