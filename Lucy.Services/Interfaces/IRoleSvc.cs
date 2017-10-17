using System.Collections.Generic;
using Lucy.Services.Dtos;
using Lucy.Models;
using Lucy.Tools;

namespace Lucy.Services.Interfaces
{
    public interface IRoleSvc
    {
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        object GetPager(GridPageModel pager, SearchModel search);
        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SysRole GetModel(int? id);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <param name="menuIDs"></param>
        /// <param name="funcIDs"></param>
        /// <returns></returns>
        ResultEx Save(SysRole model, List<int> menuIDs, List<int> funcIDs);
        /// <summary>
        /// 获取角色菜单权限
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        List<int> GetRoleMenuID(int roleID);
        /// <summary>
        /// 获取角色功能全系西安
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        List<int> GetRoleFuncID(int roleID);
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        ResultEx Delete(List<int> ids);
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        List<SysRole> GetList();
    }
}
