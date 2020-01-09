using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Castle.Core.Internal;
using Domain.Common;
using Domain.Common.Dtos.MemberShip;
using Domain.Common.Entities.MemberShip;
using Domain.Common.Enums;
using Domain.Common.Repositories;
using Domain.IService;
using Domain.Service.Handler;
using Hx;
using Hx.Extensions;
using Hx.Components;
using Hx.ICacheManager;
using Hx.ObjectMapping;
using MediatR;
using Hx.HttpClientFactory;
using Microsoft.Extensions.Configuration;
using Hx.Serializing;
using Domain.Common.Entities.Balance;
using Domain.Common.Entities.Expenses;
using Hx.Redis;
using Domain.Common.Entities.Perms;

namespace Domain.Service
{
    public class CustomerService : ServiceBase, ICustomerService
    {
        private IBalanceRepository _balanceRepository;
        private ICustomerRepository _customerRepository;
        private IHxHttpClientFactory _hxHttpClientFactory;
        private IConfiguration _configuration;
        private ICustomerCache _customerCache;
        private IAuthentyRepository _authentyRepository;
        private IRechangeRepository _rechangeRepository;
        private RedisHelper _redisHelper;
        private IDataInOrgRepository _dataInOrgRepository;
        private IStaffRepository _staffRepository;
        private IOrgRepository _orgRepository;
        public CustomerService(ICustomerRepository customerRepository, ICustomerCache customerCache, IAuthentyRepository authentyRepository, IHxHttpClientFactory hxHttpClientFactory, IConfiguration configuration, IBalanceRepository balanceRepository, IRechangeRepository rechangeRepository, RedisHelper redisHelper, IDataInOrgRepository dataInOrgRepository, IStaffRepository staffRepository,IOrgRepository orgRepository)
        {
            _customerRepository = customerRepository;
            _customerCache = customerCache;
            _authentyRepository = authentyRepository;
            _hxHttpClientFactory = hxHttpClientFactory;
            _configuration = configuration;
            _balanceRepository = balanceRepository;
            _rechangeRepository = rechangeRepository;
            _redisHelper = redisHelper;
            _dataInOrgRepository = dataInOrgRepository;
            _staffRepository = staffRepository;
            _orgRepository = orgRepository;
        }

        public CustomerDto Get(string userId)
        {
            var entity = UnitOfWorkService.Execute(() => _customerRepository.Get(userId));
            if (entity == null) return new CustomerDto();
            return entity?.MapTo<CustomerDto>();
        }

        public CustomerDto GetCache(string userId)
        {
           return _customerCache.Get(userId);
        }

        public List<CustomerDto> GetModels(CustomerPara para, out int total)
        {
            var p = PredicateBuilder.Default<CustomerDto>();
            if (!string.IsNullOrWhiteSpace(para.Id))
            {
                p = p.And(m => m.Id.Contains(para.Id));
            }
            if (para.SalesMan.IsNotNullOrEmpty())
            {
                p = p.And(m => m.SalesMan.Equals(para.SalesMan));
            }
            //p = p.And(m => m.Status < AccountStatus.Obsolete);
            var lst = GetList(para.RoleId, para.OrgId).Where(p.Compile()).ToList();
            total = lst.Count;
            var rlt = lst.OrderByDescending(m => m.CreateDate).Skip(para.PageNumber * para.PageSize).Take(para.PageSize).ToList();
            return rlt;
        }
        public CustomerModel GetModel(string userId)
        {
            var list = GetModels();
            var model = list.FirstOrDefault(m => m.Id.Equals(userId));
            return model ?? new CustomerModel();
        }
        public List<CustomerModel> GetModels()
        {
            try
            {
                var customers = UnitOfWorkService.Execute(() => _customerRepository.GetList().ToList());
                var authenties = UnitOfWorkService.Execute(() => _authentyRepository.GetList().ToList());
                if (!authenties.Any())
                    return customers.Select(m => m.MapTo<CustomerModel>()).ToList();
                var rlt = from c in customers
                          join a in authenties
                              on c.Id equals a.Id into cusauth
                          from a in cusauth.DefaultIfEmpty()
                          select Create(a, c);
                return rlt.ToList();
            }
            catch (Exception e)
            {
                Logger.Error("select * from authenty and customer error:", e);
                return new List<CustomerModel>();
            }
        }

        public List<CustomerDto> GetList(UserType roleId, int orgId)
        {
            var rlt = UnitOfWorkService.Execute(() =>
            {
                var datas = _orgRepository.GetDatas<CustomerEntity>((int)roleId, orgId, DataType.Customer);
                return datas?.MapTo<List<CustomerDto>>();
            });
            return rlt;
        }

        private CustomerModel Create(AuthentyEntity a, CustomerEntity c)
        {
            var customer = new CustomerModel
            {
                Id = c.Id,
                PassWord = c.PassWord,
                BusinessContact = c.BusinessContact,
                BusinessEmail = c.BusinessEmail,
                BusinessPhone = c.BusinessPhone,
                BusinessQq = c.BusinessQq,
                Creator = c.Creator,
                Operator = c.Operator,
                CreateDate = c.CreateDate,
                PermsId = c.PermsId,
                SalesMan = c.SalesMan,
                PayMode = (PayMode)c.PayMode,
                Status = (AccountStatus)c.Status,
                AuditResult = (AuditResult)c.AuditResult,
                Remark = c.Remark,

                Address = a?.Address ?? string.Empty,
                AuthCreateDate = a != null ? a.CreateDate : DateTime.Now,
                Email = a?.Email ?? string.Empty,
                Licence = a?.Licence ?? string.Empty,
                Name = a?.Name ?? string.Empty,
                Phone = a?.Phone ?? string.Empty,
                Telephone = a?.Telephone ?? string.Empty
            };
            return customer;
        }

        public List<CustomerDto> CustomerPara(CustomerPara para, out int total)
        {
            var count = 0;
            var customers = UnitOfWorkService.Execute(() =>
            {
                var p = PredicateBuilder.Default<CustomerEntity>();
                if (!string.IsNullOrEmpty(para.Id))//账号
                {
                    p = p.And(m => m.Id.Equals(para.Id));
                }
                p = p.And(m => m.Status < 8);
                var customer = _customerRepository.GetPaged(p, para.PageNumber, para.PageSize, out count, false, item => item.CreateDate);
                return customer?.Select(c => c.MapTo<CustomerDto>()).ToList();
            });
            if (customers == null)
            {
                total = 0;
                return new List<CustomerDto>();
            }
            total = count;
            return customers;
        }
        public Result Add(CustomerDto dto, UserType roleId)
        {
            try
            {
                var id = "";
                var rlt = UnitOfWorkService.Execute(() =>
                {
                    var u = _customerRepository.Get(dto.Id);
                    if (u != null)
                    {
                        return new Result(false, "账号已被占用");
                    }
                    id = _customerRepository.InsertAndGetId(dto.MapTo<CustomerEntity>());
                    var orgId = _staffRepository.GetOrgId(dto.SalesMan).OrgId;
                    var dataInOrg = new DataInOrgEntity
                    {
                        DataId = id,
                        DataType = (int)DataType.Customer,
                        OrgId = orgId,
                        RoleId = ((int)roleId).ToString()
                    };
                    var dataRlt = _dataInOrgRepository.Inserts(dataInOrg);
                    Balance(dto,roleId);
                    Recharge(dto,roleId);
                    _redisHelper.StringGet(RedisKeyName.CreateUserAmountKey(dto.Id));
                    _redisHelper.StringIncrement(RedisKeyName.CreateUserAmountKey(id), _configuration.GetValue("giftNumber", 20));
                    if (dataRlt && id.IsNotNullOrEmpty())
                    {
                        return new Result { Status = true };
                    }
                    return new Result { Status = false, Message = "数据库写入失败" };
                });
                if (!rlt.Status) return rlt;
                dto.Id = id;
                var json = Serializer.Serialize(dto);
                var url = _configuration.GetValue("WebApi:url", "http://localhost:15002/sync");
                var http = _hxHttpClientFactory.CreateHttpClient();
                var res = http.SendAsync(url, Serializer.Serialize(new CusNotification { Method = 21, Body = json }));
                Logger.Info($"customer同步结果add:{json}");
                return rlt;
            }
            catch (Exception ex)
            {
                Logger.Error("添加用户时发生错误", ex);
                return new Result(false, "用户添加报错");
            }
        }

        /// <summary>
        /// 余额
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="roleId"></param>
        private void Balance(CustomerDto dto, UserType roleId)
        {
            var b = new BalanceEntity
            {
                UserId = dto.Id,
                OperatorId = dto.Operator,
                BeforeValue = 0,
                Value = _configuration.GetValue("giftNumber", 20),
                ModifyDate = DateTime.Now,
                SalesMan = dto.SalesMan,
                Remark = "赠送"
            };
            _balanceRepository.ChangeBalance(b);
            var orgId = _staffRepository.GetOrgId(dto.SalesMan).OrgId;
            var dataInOrg = new DataInOrgEntity
            {
                DataId = dto.Id,//客户id
                DataType = (int)DataType.Balance,
                OrgId = orgId,
                RoleId = ((int)roleId).ToString()
            };
            _dataInOrgRepository.Inserts(dataInOrg);
        }
        private void Recharge(CustomerDto dto, UserType roleId)
        {
            var r = new RechargeEntity
            {
                UserId = dto.Id,
                Value = _configuration.GetValue("giftNumber", 20),
                Money = 0,
                Bank = 0,
                RechargeMode = 0,
                SalesMan = dto.SalesMan,
                OperatorId = dto.Operator,
                CreateDate = DateTime.Now,
                Remark = "赠送"
            };
            var id= _rechangeRepository.InsertAndGetId(r);
            var orgId = _staffRepository.GetOrgId(dto.SalesMan).OrgId;
            var dataInOrg = new DataInOrgEntity
            {
                DataId = id.ToString(),
                DataType = (int)DataType.Recharge,
                OrgId = orgId,
                RoleId = ((int)roleId).ToString()
            };
           _dataInOrgRepository.Inserts(dataInOrg);
        }
        /// <summary>
        /// 禁用客户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result Delete(string id)
        {
            try
            {
                return UnitOfWorkService.Execute(() =>
                {
                    var r = _customerRepository.Get(id);
                    r.Status = (int)AccountStatus.Obsolete;
                    var user = _customerRepository.Update(r);
                    if (!user)
                    {
                        return new Result { Status = false };
                    }
                    var url = _configuration.GetValue("WebApi:url", "http://localhost:15002/sync");
                    var http = _hxHttpClientFactory.CreateHttpClient();
                    http.SendAsync(url, Serializer.Serialize(new CusNotification { Method = 23, Body = id }));
                    Logger.Info($"customer同步结果del:{id}");
                    return new Result { Status = true };
                });
            }
            catch (Exception ex)
            {
                Logger.Error("禁用用户发生异常:", ex);
                return new Result(false, "禁用失败");
            }
        }
        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result Pass(string id)
        {
            return UnitOfWorkService.Execute(() =>
            {
                var entity = _customerRepository.Get(id);
                if (entity == null) return new Result { Status = false };
                entity.Status = 1;
                _customerRepository.Update(entity);
                return new Result { Status = true };
            });
        }
        public Result Update(CustomerDto dto)
        {
            try
            {
                var rlt = UnitOfWorkService.Execute(() => _customerRepository.Update(dto.MapTo<CustomerEntity>()));
                if (!rlt) return new Result(false, "修改失败");
                var json = Serializer.Serialize(dto);
                var url = _configuration.GetValue("WebApi:url", "http://localhost:15002/sync");
                var http = _hxHttpClientFactory.CreateHttpClient();
                http.SendAsync(url, Serializer.Serialize(new CusNotification { Method = 22, Body = json }));
                Logger.Info($"customer同步结果update:{json}");
                return new Result { Status = true };
            }
            catch (Exception ex)
            {
                Logger.Error("客户修改失败", ex);
                return new Result(false, "修改失败");
            }
        }
        public Result Verify(string userName, string password)
        {
            var customerDto = Get(userName);//TODO:登录是否使用缓存
            if (customerDto == null) return new Result { Status = false, Message = "无效账号" };
            if (customerDto.Status == AccountStatus.Obsolete) return new Result { Status = false, Message = "该账号已禁用" };
            if (customerDto.Status != AccountStatus.Normal) return new Result { Status = false, Message = "该账号无法登录" };
            var pwd = Hx.Security.Md5Getter.Get(password);//密码加密验证

            if (customerDto.Id.Equals(userName, StringComparison.OrdinalIgnoreCase) && customerDto.PassWord.Equals(pwd, StringComparison.OrdinalIgnoreCase))
            {
                return new Result() { Status = true, Body = customerDto, Message = "登录成功" };
            }

            return new Result() { Status = false, Message = "账号或密码错误" };
        }

        public void Set(CustomerDto dto)
        {
            _customerCache.Set(dto.Id, dto);
        }

        public void Remove(string id)
        {
            _customerCache.Remove(id);
        }

        public List<CustomerDto> GetList()
        {
            var entities = UnitOfWorkService.Execute(() => _customerRepository.GetList().ToList());
            return entities?.MapTo<List<CustomerDto>>();
        }

        public List<string> Audit(string[] msgs, AuditResult auditResult)
        {
            var ids = new List<string>();
            for (int i = 0; i < msgs.Length; i++)
            {
                var customer = Get(msgs[i]);
                customer.AuditResult = auditResult;
                var rlt = Update(customer);
                if (rlt.Status)
                {
                    ids.Add(msgs[i]);
                }
            }
            return ids;
        }

        public List<string> GetCustomersBySale(string salerId)
        {
            var list = GetList();
            var sales = list.Where(m => m.SalesMan.Equals(salerId));
            var strList = new List<string>();
            list.ForEach(m => strList.Add(m.Id));
            return strList;
        }
    }
}
