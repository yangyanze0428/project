using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alipay.AopSdk.AspnetCore;
using Domain.Common;
using Domain.Common.Dtos.Balance;
using Domain.Common.Entities.Balance;
using Domain.Common.Enums;
using Domain.Common.Repositories;
using Domain.IService;
using Hx.Pay.Ali;
using Hx.Redis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Web.CusManager.Controllers
{
    public class AliReturnController : ControllerBase
    {
        private readonly IRechargeService _rechargeService;
        private readonly IBalanceService _balanceService;
        private readonly RedisHelper _redisHelper;
        private readonly ICustomerService _customerService;
        private readonly AlipayService _alipayService;
        private readonly IConfiguration _configuration;

        public AliReturnController(IRechargeService rechargeService, IBalanceService balanceService, RedisHelper redisHelper, ICustomerService customerService, AlipayService alipayService, IConfiguration configuration)
        {
            _rechargeService = rechargeService;
            _balanceService = balanceService;
            _redisHelper = redisHelper;
            _customerService = customerService;
            _alipayService = alipayService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 支付宝同步返回通知
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[Route("alireturn")]
        public IActionResult AliReturn()
        {
            var tradeResult = string.Empty;
            try
            {
                /* 实际验证过程建议商户添加以下校验。
              1、商户需要验证该通知数据中的out_trade_no是否为商户系统中创建的订单号，
              2、判断total_amount是否确实为该订单的实际金额（即商户订单创建时的金额），
              3、校验通知中的seller_id（或者seller_email) 是否为out_trade_no这笔单据的对应的操作方（有的时候，一个商户可能有多个seller_id/seller_email）
              4、验证app_id是否为该商户本身。
              */
                var sArray = GetRequestGet();
                if (sArray.Count != 0)
                {
                    //var flag = AlipaySignature.RSACheckV1(sArray, Config.alipay_public_key, Config.charset, Config.sign_type, false);
                    bool flag = _alipayService.RSACheckV1(sArray);
                    if (flag)
                    {
                        string trade_status = Request.Query["trade_status"];
                        var out_trade_no = Request.Query["out_trade_no"];//商户订单号
                        var trade_no = Request.Query["trade_no"];//支付宝交易号
                        var seller_id = Request.Query["seller_id"];//支付宝账号//卖家支付宝用户ID 2088102170353417
                        var total_amount = Request.Query["total_amount"]; //金额
                        var timestamp = Request.Query["timestamp"]; //交易时间
                        var appid = Request.Query["app_id"]; //交易时间
                        var value = Request.Query["value"]; //数量
                        var order = _rechargeService.GetNumber(out_trade_no);
                        var price = _customerService.Get(LoginInfo.Id).Price;
                        var salesMan = _customerService.Get(LoginInfo.Id).SalesMan;
                        //todo:充值表，redis 余额
                        if (order != null)//订单对比
                        {
                            //if (trade_status == "TRADE_FINISHED" || trade_status == "TRADE_SUCCESS")
                            // if (Config.app_id.Equals(appid))
                            var app_id = _configuration.GetValue("Alipay:AppId", "2016080700188416");
                            if (app_id.Equals(appid))
                            {
#if DEBUG
                                //var val = Convert.ToDecimal(total_amount) / price;
                                //var s = Math.Ceiling(val);//Math.Ceiling：向上取整，只要有小数都加1
                                //var balance = new BalanceDto
                                //{
                                //    SalesMan = salesMan,
                                //    UserId = LoginInfo.Id,
                                //    Value = decimal.ToInt32(s),
                                //    Money = decimal.ToInt32(Convert.ToDecimal(total_amount)),
                                //    OperatorId = LoginInfo.Id,
                                //    ModifyDate = DateTime.Now,
                                //    Remark = "客户端"
                                //};
                                //await _balanceService.OnlineRechange(balance, UserType.Admin);//更改客户余额//todo:充值表两条,考虑加订单表
                                // await _redisHelper.StringGetAsync(RedisKeyName.CreateUserAmountKey(LoginInfo.Id));
                                //await _redisHelper.StringIncrementAsync(RedisKeyName.CreateUserAmountKey(LoginInfo.Id), balance.Value);
                                order.TradeResult = TradeResult.Success;//修改订单结果
                                order.OrderDate = Convert.ToDateTime(timestamp);//添加交易时间
                                order.TranId = trade_no;//支付宝交易号
                                _rechargeService.Update(order);
                                tradeResult = "支付成功！";
                                Json("success");
#else
                            tradeResult = "支付成功！";
                            Json("success");
#endif
                            }
                            else
                            {
#if DEBUG
                                order.TradeResult = TradeResult.Fail;//修改订单结果
                                order.OrderDate = Convert.ToDateTime(timestamp);//添加交易时间
                                order.TranId = trade_no;//支付宝交易号
                                _rechargeService.Update(order);
                                tradeResult = "支付失败！";
                                Json("fail");
#else
                            tradeResult = "支付失败！";
                            Json("fail");
#endif
                            }
                        }
                        else//无效订单
                        {
                            order.TradeResult = TradeResult.Invalid;//修改订单结果
                            order.OrderDate = Convert.ToDateTime(timestamp);//添加交易时间
                            order.TranId = trade_no;//支付宝交易号
                            _rechargeService.Update(order);
                            Json("fail");
                        }
                        // Json("同步验证通过");
                    }
                    else
                    {
                        tradeResult = "验证失败！";
                        Json("同步验证失败");
                    }
                }

                ViewBag.TradeResult = tradeResult;
                return View();
            }
            catch (Exception ex)
            {
                Logger.Error("zfb 同步:", ex);
                ViewBag.TradeResult = tradeResult;
                return View();
            }
        }
        public Dictionary<string, string> GetRequestGet()
        {
            var i = 0;
            var sArray = new Dictionary<string, string>();
            //NameValueCollection coll;
            //coll = Request.Form;
            var coll = Request.Query;
            // Get names of all forms into a string array.

            var requestItem = coll.Keys.ToArray();

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Query[requestItem[i]]);
            }
            return sArray;

        }
        /// <summary>
        /// 支付宝异步返回通知
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        // [Route("/alinotify")]
        [AllowAnonymous]
        public async void AliNotify()
        {
            /* 实际验证过程建议商户添加以下校验。
         1、商户需要验证该通知数据中的out_trade_no是否为商户系统中创建的订单号，
         2、判断total_amount是否确实为该订单的实际金额（即商户订单创建时的金额），
         3、校验通知中的seller_id（或者seller_email) 是否为out_trade_no这笔单据的对应的操作方（有的时候，一个商户可能有多个seller_id/seller_email）
         4、验证app_id是否为该商户本身。
         */
            var tradeResult = string.Empty;
            try
            {
                var sArray = GetRequestPost();
                if (sArray.Count != 0)
                {
                    //var flag = AlipaySignature.RSACheckV1(sArray, Config.alipay_public_key, Config.charset, Config.sign_type, false);
                    bool flag = _alipayService.RSACheckV1(sArray);
                    if (flag)
                    {
                        //交易状态
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //请务必判断请求时的total_amount与通知时获取的total_fee为一致的
                        //如果有做过处理，不执行商户的业务程序

                        //注意：
                        //退款日期超过可退款期限后（如三个月可退款），支付宝系统发送该交易状态通知
                        string trade_status = Request.Form["trade_status"];
                        var out_trade_no = Request.Form["out_trade_no"];//商户订单号
                        var trade_no = Request.Form["trade_no"];//支付宝交易号
                        var seller_id = Request.Form["seller_id"];//支付宝账号//卖家支付宝用户ID 2088102170353417
                        var total_amount = Request.Form["total_amount"]; //金额
                        var timestamp = Request.Form["timestamp"]; //交易时间
                        var value = Request.Form["value"]; //数量
                        var appid = Request.Form["app_id"];//appid
                                                           //根据订单号取数据
                        var order = _rechargeService.GetNumber(out_trade_no);
                        // var total = _prodService.GetNumber(decimal.ToInt32(Convert.ToDecimal(total_amount))).Total;
                        Logger.Info($"订单号：{out_trade_no},总数：{_rechargeService.GetNumber(out_trade_no).Value}");
                        var total = _rechargeService.GetNumber(out_trade_no).Value;
                        var salesMan = _customerService.Get(order.UserId).SalesMan;
                        #region
                        switch (trade_status)
                        {
                            case "TRADE_FINISHED"://支付完成
                            case "TRADE_SUCCESS"://支付成功
                                                 //注意：
                                                 //付款完成后，支付宝系统发送该交易状态通知
                                var app_id = _configuration.GetValue("Alipay:AppId", "2016080700188416");
                                if (app_id.Equals(appid))//账号对比//todo:seller_id需要改
                                {
                                    if (order != null)//订单对比
                                    {
                                        var balance = new BalanceDto
                                        {
                                            SalesMan = salesMan,
                                            UserId = order.UserId,
                                            Value = total,
                                            Money = decimal.ToInt32(Convert.ToDecimal(total_amount)),
                                            OperatorId = order.UserId,
                                            ModifyDate = DateTime.Now
                                        };
                                        await _balanceService.OnlineRechange(balance, UserType.Admin);//更改客户余额
                                        await _redisHelper.StringGetAsync(RedisKeyName.CreateUserAmountKey(order.UserId));
                                        await _redisHelper.StringIncrementAsync(RedisKeyName.CreateUserAmountKey(order.UserId), total);
                                        order.TradeResult = TradeResult.Success;//修改订单结果
                                        order.OrderDate = Convert.ToDateTime(timestamp);//添加交易时间
                                        order.TranId = trade_no;//支付宝交易号
                                        _rechargeService.Update(order);
                                        Json("success");
                                    }
                                    else//无效订单
                                    {
                                        order.TradeResult = TradeResult.Invalid;//修改订单结果
                                        order.OrderDate = Convert.ToDateTime(timestamp);//添加交易时间
                                        order.TranId = trade_no;//支付宝交易号
                                        _rechargeService.Update(order);
                                        Json("fail");
                                    }
                                }
                                break;
                            case "TRADE_CLOSED"://订单关闭/买家未在规定时间内付款
                                order.TradeResult = TradeResult.Cancel;//修改订单结果
                                order.OrderDate = Convert.ToDateTime(timestamp);//添加交易时间
                                order.TranId = trade_no;//支付宝交易号
                                _rechargeService.Update(order);
                                Json("fail");
                                break;
                            default://支付失败
                                order.TradeResult = TradeResult.Fail;//修改订单结果
                                order.OrderDate = Convert.ToDateTime(timestamp);//添加交易时间
                                order.TranId = trade_no;//支付宝交易号
                                _rechargeService.Update(order);
                                Json("fail");
                                break;
                        }
                        #endregion
                        #region
                        //                        if (order != null)//订单对比
                        //                        {
                        //                            //if (trade_status == "TRADE_FINISHED" || trade_status == "TRADE_SUCCESS")
                        //                            if (Config.app_id.Equals(appid))
                        //                            {
                        //#if DEBUG
                        //                                var val = Convert.ToDecimal(total_amount) / price;
                        //                                var s = Math.Ceiling(val);//Math.Ceiling：向上取整，只要有小数都加1
                        //                                var balance = new BalanceDto
                        //                                {
                        //                                    SalesMan = salesMan,
                        //                                    UserId = LoginInfo.Id,
                        //                                    Value = decimal.ToInt32(s),
                        //                                    Money = decimal.ToInt32(Convert.ToDecimal(total_amount)),
                        //                                    OperatorId = LoginInfo.Id,
                        //                                    ModifyDate = DateTime.Now,
                        //                                    Remark = "客户端"
                        //                                };
                        //                                await _balanceService.OnlineRechange(balance, UserType.Admin);//更改客户余额//todo:充值表两条,考虑加订单表
                        //                                                                                              // await _redisHelper.StringGetAsync(RedisKeyName.CreateUserAmountKey(LoginInfo.Id));
                        //                                                                                              //await _redisHelper.StringIncrementAsync(RedisKeyName.CreateUserAmountKey(LoginInfo.Id), balance.Value);
                        //                                order.TradeResult = TradeResult.Success;//修改订单结果
                        //                                order.OrderDate = Convert.ToDateTime(timestamp);//添加交易时间
                        //                                _rechargeService.Update(order);
                        //                                tradeResult = "支付成功！";
                        //                                Json("success");
                        //#else
                        //                            tradeResult = "支付成功！";
                        //                            Json("success");
                        //#endif
                        //                            }
                        //                            else
                        //                            {
                        //#if DEBUG
                        //                                order.TradeResult = TradeResult.Fail;//修改订单结果
                        //                                order.OrderDate = Convert.ToDateTime(timestamp);//添加交易时间
                        //                                _rechargeService.Update(order);
                        //                                tradeResult = "支付失败！";
                        //                                Json("fail");
                        //#else
                        //                            tradeResult = "支付失败！";
                        //                            Json("fail");
                        //#endif
                        //                            }
                        //                        }
                        //                        else//无效订单
                        //                        {
                        //                            order.TradeResult = TradeResult.Invalid;//修改订单结果
                        //                            order.OrderDate = Convert.ToDateTime(timestamp);//添加交易时间
                        //                            _rechargeService.Update(order);
                        //                            Json("fail");
                        //                        }
                        #endregion
                        //Json("success");
                    }
                    else
                    {

                        Json("fail");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("zfb 异步:", ex);
            }

        }
        public Dictionary<string, string> GetRequestPost()
        {
            var i = 0;
            var sArray = new Dictionary<string, string>();
            // NameValueCollection coll;
            //coll = Request.Form;
            var coll = Request.Form;
            var requestItem = coll.Keys.ToArray();
            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }
            return sArray;
        }
    }
}