using Domain.Common;
using Domain.Common.Entities.Balance;
using Domain.Common.Repositories;
using Hx.Components;
using Hx.Domain.Services;
using Hx.Logging;
using Hx.Redis;
using Hx.Serializing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Common.Entities.Expenses;

namespace Domain.Service.MediatHandler
{
    public class SendCompleteChargingHandler : INotificationHandler<SendCompleteCommand>
    {
        IBalanceRepository _balanceRepo;
        ILogger Logger { get; set; }
        public SendCompleteChargingHandler(IBalanceRepository balanceRepo,ILoggerFactory factory) 
        {
            Logger = factory.Create();
            _balanceRepo = balanceRepo;
        }
        public async Task Handle(SendCompleteCommand notification, CancellationToken cancellationToken)
        {
            try
            {
                await _balanceRepo.ChangeBalance(new BalanceEntity { UserId = notification.UserId, Value = 1, Remark = "计费程序" });
            }
            catch (Exception ex)
            {
                Logger.Error($"class:{nameof(SendCompleteChargingHandler)}orcl,写入数据库失败",ex);
            }
            
        }
    }
    public class SendCompleteRedisChargingHandler : INotificationHandler<SendCompleteCommand>
    {
        private RedisHelper _redisHelper;
        ILogger Logger { get; set; }
        public SendCompleteRedisChargingHandler(RedisHelper redisHelper, ILoggerFactory factory)
        {
            Logger = factory.Create();
            _redisHelper = redisHelper;
        }
        public async Task Handle(SendCompleteCommand notification, CancellationToken cancellationToken)
        {
            try
            {
              await _redisHelper.StringIncrementAsync(RedisKeyName.CreateUserAmountKey(notification.UserId), -1);
            }
            catch (Exception ex)
            {
                Logger.Error($"class:{nameof(SendCompleteRedisChargingHandler)}redis,写入数据库失败", ex);
            }

        }
    }
    public class SendCompleteExpenseDetailHandler : INotificationHandler<SendCompleteCommand>
    {
        private IExpenseDetailRepository _expenseDetailRepo;
        ILogger Logger { get; set; }
        public SendCompleteExpenseDetailHandler(IExpenseDetailRepository expenseDetailRepo, ILoggerFactory factory)
        {
            Logger = factory.Create();
            _expenseDetailRepo = expenseDetailRepo;
        }
        public async Task Handle(SendCompleteCommand notification, CancellationToken cancellationToken)
        {
            var entity = new ExpenseDetailEntity();
            CreateEntity(notification, entity);
            try
            {
                
                var unitOfWork = ObjectContainer.Resolve<IUnitOfWorkService>();
                unitOfWork.ExecuteNonQuery(async ()=> {
                   await  _expenseDetailRepo.InsertAsync(entity);
                });
                 await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                var json= ObjectContainer.Resolve<ISerializer>().Serialize(entity);
                Logger.Error($"class:{nameof(SendCompleteExpenseDetailHandler)},json:{json},orcl,写入数据库失败:", ex);
            }
        }

        private static void CreateEntity(SendCompleteCommand notification, ExpenseDetailEntity entity)
        {
            entity.Appkey = notification.AppKey;
            entity.Description = notification.Description;
            entity.Mobile = notification.Mobile;
            entity.OperatorType =Convert.ToInt32(notification.OperatorType);
            entity.Remark = "";
            entity.Success = notification.Result;
            entity.UserId = notification.UserId;
            entity.Value = -1;
        }
    }
}
