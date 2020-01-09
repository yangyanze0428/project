using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.CusManager.Helper;
using Web.CusManager.Models;

namespace Web.CusManager.Controllers
{

    public class CommonController : ControllerBase
    {
        //static private Dictionary<string, Type> SchemeTypes = new Dictionary<string, Type>();
        //static CommonController()
        //{
        //    SchemeTypes["staff"] = typeof(StaffModel);
        //    SchemeTypes["channel"] = typeof(ChannelModel);
        //    SchemeTypes["cg"] = typeof(ChannelGroup);
        //    SchemeTypes["ft"] = typeof(FreeAuditTextModel);
        //    SchemeTypes["blacklist"] = typeof(BlackListSysModel);
        //    SchemeTypes["swu"] = typeof(SensitiveWordUserModel);
        //    SchemeTypes["swc"] = typeof(SensitiveWordChannelModel);
        //    SchemeTypes["sm"] = typeof(SmsInfoModel);
        //    SchemeTypes["ca"] = typeof(ContentAnalysisModel);
        //    SchemeTypes["role"] = typeof(RoleModel);
        //    SchemeTypes["sa"] = typeof(SignAtureModel);
        //    SchemeTypes["blacklistuser"] = typeof(BlackListUserModel);
        //    SchemeTypes["blacklistchannel"] = typeof(BlackListChannelModel);
        //    SchemeTypes["rt"] = typeof(RoutingModel);
        //    SchemeTypes["gc"] = typeof(GetClientModel);
        //    SchemeTypes["wl"] = typeof(WhiteListModel);
        //    SchemeTypes["sw"] = typeof(SensitiveWordModel);

        //    SchemeTypes["audit"] = typeof(AuditModel);
        //    SchemeTypes["si"] = typeof(SmsDetailInfoModel);
        //    SchemeTypes["sl"] = typeof(SmsReplyModel);
        //}

        //public async Task<JsonResult> GetChannels()
        //{

        //}
        //[Route("/c/msgtype")]
        //public async Task<JsonResult> GetMsgType()
        //{
        //    var msgType = EnumExt.GetSelectList(typeof(MsgType));
        //    var idTexts = msgType.Select(m =>
        //    {
        //        var idtext = new IdText { Id = m.Value, Text = m.Text, selected = m.Value == "1" };
        //        return idtext;
        //    }).ToList();
        //    return await Task.FromResult(Json(idTexts));
        //}

        ////获取所有通道
        //[Route("channels")]
        //public async Task<JsonResult> GetChannels()
        //{
        //    var chService = ObjectContainer.Resolve<IChannelService>();
        //    var chs = UseTenant(() => chService.GetChannels());
        //    var idtext = GetIdTexts(chs);
        //    return await Task.FromResult(Json(idtext));
        //}
        //[Route("/userexist")]
        //public async Task<bool> UserExist(string id)
        //{
        //    var userService = ObjectContainer.Resolve<ICustomerService>();
        //    var user = UseTenant(() => userService.Get(id));//.GetUser(id);
        //    if (user != null) return true;
        //    return await Task.FromResult(false);
        //}

        //private IdText[] GetIdTexts(List<ChannelInfoDto> dtos)
        //{
        //    if (dtos == null || dtos.Count <= 0) return null;
        //    var list = new List<IdText>();
        //    foreach (var item in dtos)
        //    {
        //        var idtext = new IdText { Id = item.Id, Text = item.Name };
        //        list.Add(idtext);
        //    }
        //    return list.ToArray();
        //}
        [Route("scheme")]
        public async Task<List<GridColumn>> Get(string scheme)
        {
            if (ModelSchemeHelper.Schemes.TryGetValue(scheme.ToLower(), out var cols))
            {
                //var cols = GridCreator.Create(t);
                return await Task.FromResult(cols);
            }
            return null;
        }
    }
}