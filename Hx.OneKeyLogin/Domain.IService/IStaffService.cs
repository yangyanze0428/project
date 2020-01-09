using System.Collections.Generic;
using Domain.Common;
using Domain.Common.Dtos.MemberShip;
using Domain.Common.Enums;

namespace Domain.IService
{
    public interface IStaffService
    {
        Result Verify(StaffDto dto);
        StaffDto Get(string userId);
        /// <summary>
        /// 增加员工
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Result<string> Add(StaffDto dto, int orgId, UserType roleId);
        /// <summary>
        /// 修改员工
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Result<string> Update(StaffDto dto);
        /// <summary>
        /// 禁用员工
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result Delete(string id);
        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Result Pass(string id);
        /// <summary>
        /// 获取员工列表
        /// </summary>
        /// <returns></returns>
        List<StaffDto> GetList();

        List<StaffDto> GetList(StaffPara para, out int count);
        List<StaffDto> GetStaff(UserType roleId, int orgId);
    }
}
