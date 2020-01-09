using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Alipay.AopSdk.AspnetCore;
using Alipay.AopSdk.Core.Domain;
using Alipay.AopSdk.Core.Request;
using Domain.Common;
using Domain.Common.Dtos.Balance;
using Domain.Common.Enums;
using Domain.IService;
using Hx.Extensions;
using Hx.Pay.Ali;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace Web.CusManager.Controllers
{
    public class PayController : ControllerBase
    {
        private readonly IRechargeService _rechargeService;
        private readonly ICustomerService _customer;
        private readonly AlipayService _alipayService;
        private readonly IConfiguration _configuration;


        public PayController(IRechargeService rechargeService, ICustomerService customer, AlipayService alipayService, IConfiguration configuration)
        {
            _rechargeService = rechargeService;
            _customer = customer;
            _alipayService = alipayService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public void PayDo(List<IFormFile> formFiles)
        {
            var pay = Request.Form["pay"].ToString(); // ali/wx

            var outTradeNo = DateTime.Now.ToStringTimeStamp();
            //订单名称，必填
            var subject = Request.Form["Name"];
            ////流水号
            //var Sernumber = DateTime.Now.ToStringTimeStamp(); // + Request.Query["Id"];
            //付款金额，必填
            var totalFee = Convert.ToDecimal(Request.Form["Money"]);
            //总条数
            var total = Convert.ToInt32(Request.Form["Total"]);

            //商品编号
            var prodId = Request.Form["Id"].ToString();

            var rlt = Add(pay, outTradeNo, totalFee, total, subject);
            if (pay == "wx")
            {

            }
            else
            {
                Payments(outTradeNo, subject, totalFee);
            }
        }
        [HttpGet]
        public void PayDo(string payType, string orderId)
        {
            try
            {
                var rlt = _rechargeService.GetList().FirstOrDefault(m => m.OrderNumber == orderId);
                var pay = payType; // ali/wx
                var outTradeNo = rlt?.OrderNumber;
                var subject = rlt?.OrderName;
                var totalFee = rlt?.Money;
                var num = rlt?.Value;
                if (pay == "wx")
                {
                    //string ImageUrl = "/Wx/MakeQrCode?data=" + HttpUtility.UrlEncode(payRlt.Message);
                    //Request.HttpContext.Session.SetString("ImageUrl", ImageUrl);
                    //HttpContext.Session.CommitAsync();
                    //return Redirect("/Prod/WxPay"); //微信二维码支付页面
                }
                else
                {
                    Payments(outTradeNo, subject, Convert.ToDecimal(totalFee));

                }//支付宝支付页面
            }
            catch (Exception ex)
            {
                Logger.Error("PayPayDo error:", ex);
            }
        }

        private void Payments(string outTradeNo, string subject, decimal totalFee)
        {
            // 组装业务参数model
            var model = new AlipayTradePagePayModel
            {
                Body = "",
                Subject = subject,
                TotalAmount = totalFee.ToString(),
                OutTradeNo = outTradeNo,
                ProductCode = "FAST_INSTANT_TRADE_PAY"
            };
            var request = new AlipayTradePagePayRequest();
            // 设置同步回调地址
           // request.SetReturnUrl($"http://{Request.Host}/AliReturn/AliReturn");
            request.SetReturnUrl(_configuration.GetValue("Online:returnUrl", "http://120.133.26.47:15002/AliReturn/AliReturn"));
            // 设置异步通知接收地址
            var get = GetHost();
            //request.SetReturnUrl($"http://{Request.Host}/AliReturn/AliNotify");
            request.SetNotifyUrl(_configuration.GetValue("Online:notifyUrl", "http://120.133.26.47:15002/AliReturn/AliNotify"));
            // 将业务model载入到request
            request.SetBizModel(model);

            var response = _alipayService.SdkExecute(request);
            //跳转支付宝支付
            Response.Redirect(_alipayService.Options.Gatewayurl + "?" + response.Body);
        }
        public static string GetIP()
        {
            using (var webClient = new WebClient())
            {
                try
                {
                    webClient.Credentials = CredentialCache.DefaultCredentials;
                    byte[] pageDate = webClient.DownloadData("http://pv.sohu.com/cityjson?ie=utf-8");
                    String ip = Encoding.UTF8.GetString(pageDate);
                    webClient.Dispose();

                    Match rebool = Regex.Match(ip, @"\d{2,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
                    return rebool.Value;
                }
                catch (Exception e)
                {
                    return "";
                }

            }
        }
        /// <summary>
        ///  支付宝、微信二维码支付
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        //public IActionResult PayDo(List<IFormFile> formFiles)
        //{
        //    try
        //    {
        //        var pay = Request.Form["pay"].ToString(); // ali/wx
        //        var payment = CreatePayment(pay); //创建支付类型，支付宝/微信

        //        //订单编号 10线上 11线下
        //        var outTradeNo = DateTime.Now.ToStringTimeStamp();//_orderService.CreateOrderId("10")); // + Request.Query["Id"];商户订单号，需要保证不重复
        //        //订单名称，必填
        //        var subject = Request.Form["Name"];
        //        ////流水号
        //        //var Sernumber = DateTime.Now.ToStringTimeStamp(); // + Request.Query["Id"];
        //        //付款金额，必填
        //        var totalFee = Convert.ToDecimal(Request.Form["Money"]);
        //        //总条数
        //        var num = Convert.ToInt32(Request.Form["Total"]);

        //        //商品编号
        //        var prodId = Request.Form["Id"].ToString();

        //        //生成订单发送到支付宝/微信
        //        var payRlt = payment.Pay(outTradeNo, subject, totalFee);
        //        //订单写入数据库  todo:充值表.余额表 redis  或者考虑加一张订单表
        //        //  var addOrder = AddOrder(pay, prodId, outTradeNo, totalFee, num);
        //        var rlt = Add(pay, outTradeNo, totalFee, num, subject);

        //        if (pay == "wx")
        //        {
        //            //string ImageUrl = "/Wx/MakeQrCode?data=" + HttpUtility.UrlEncode(payRlt.Message);
        //            //Request.HttpContext.Session.SetString("ImageUrl", ImageUrl);
        //            //HttpContext.Session.CommitAsync();
        //            //return Redirect("/Prod/WxPay"); //微信二维码支付页面
        //        }

        //        return Content(payRlt.Message, "text/html"); //支付宝支付页面
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error("Pay.PayDo:", ex);
        //        return Content(ex.Message);
        //    }
        //}
        private Result Add(string pay, string outTradeNo, decimal totalFee, int total, string subject)
        {
            var salesMan = _customer.Get(LoginInfo.Id).SalesMan;
            var orderDto = new RechargeDto
            {
                UserId = LoginInfo.Id,
                OperatorId = LoginInfo.Id,
                Value = total,
                Money = Convert.ToInt32(totalFee),
                CreateDate = DateTime.Now,
                OrderNumber = outTradeNo,
                OrderName = subject,
                IdenTity = IdenTity.Client,
                OrderDate = DateTime.Now,
                TradeType = pay.Equals("ali") ? TradeType.AliPay : TradeType.WxPay,
                TradeResult = TradeResult.Wait,
                SalesMan = salesMan,
                RechargeMode = RechargeMode.ContinuedCharge,
                TranId=""
            };
            var result = _rechargeService.Add(orderDto, UserType.Admin);
            return result;
        }

        [HttpGet]
        //public IActionResult PayDo(string payType, string orderId)
        //{
        //    try
        //    {
        //        var rlt = _rechargeService.GetList().FirstOrDefault(m => m.OrderNumber == orderId);
        //        var pay = payType; // ali/wx
        //        var payment = CreatePayment(pay);

        //        var outTradeNo = rlt?.OrderNumber;
        //        var subject = rlt?.OrderName;
        //        var totalFee = rlt?.Money;
        //        var num = rlt?.Value;

        //        //生成订单发送到支付宝/微信
        //        var payRlt = payment.Pay(outTradeNo, subject, Convert.ToDecimal(totalFee));

        //        if (pay == "wx")
        //        {
        //            //string ImageUrl = "/Wx/MakeQrCode?data=" + HttpUtility.UrlEncode(payRlt.Message);
        //            //Request.HttpContext.Session.SetString("ImageUrl", ImageUrl);
        //            //HttpContext.Session.CommitAsync();
        //            //return Redirect("/Prod/WxPay"); //微信二维码支付页面
        //        }
        //        return Content(payRlt.Message, "text/html"); //支付宝支付页面
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error("PayPayDo error:", ex);
        //        return Content(ex.Message);
        //    }
        //}
        //private IPayment CreatePayment(string id)
        //{
        //    //if (id.Equals("wx")) return new WxPay();
        //    if (id.Equals("ali")) return new AliPay();
        //    return null;
        //}
        private string GetHost()
        {
            var url = new StringBuilder();
            url.Append(Request.Scheme);
            url.Append("://");
            url.Append(Request.Host);
            return url.ToString();
        }
    }
}