using Domain.Common.Dtos.Perms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IService
{
    public interface IPermsService
    {
        /// <summary>
        /// 根据id获取信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PermsDto GetPerms(string id);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool InsertPerms(PermsDto dto);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool UpdatePerms(PermsDto dto);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeletePerms(string id);

        /// <summary>
        /// 获取所有权限，根据平台类型
        /// </summary>
        /// <returns></returns>
        List<PermsDto> GetPermsList(int platFormType);
        /// <summary>
        /// 根据平台类型，是否启用获取权限
        /// </summary>
        /// <param name="platFormType">平台类型</param>
        /// <param name="isenable">是否启用</param>
        /// <returns></returns>
        string GetList(int platFormType, int isenable);

        /// <summary>
        /// 获取当前用户的权限
        /// </summary>
        /// <param name="permIds">权限id</param>
        /// <returns></returns>
        List<PermsDto> GetPermsListById(string permIds);

        /// <summary>
        /// 根据条件查询权限
        /// </summary>
        /// <param name="para">条件</param>
        /// <param name="total">总条数</param>
        /// <returns></returns>
		List<PermsDtoShow> GetPermsPara(PermsPara para, out int total);

        /// <summary>
        /// 根据用户id获取权限列表
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        List<PermsDto> GetPermsListByUserId(string userId);
    }
}
