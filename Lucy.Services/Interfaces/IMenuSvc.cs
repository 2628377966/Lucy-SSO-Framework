using System.Collections.Generic;
using Lucy.Services.Dtos;
using Lucy.Models;
using Lucy.Tools;

namespace Lucy.Services.Interfaces
{
    public interface IMenuSvc
    {
        /// <summary>
        /// 获取用户权限菜单
        /// </summary>
        /// <param name="memberID"></param>
        /// <returns></returns>
        IEnumerable<SysMenu> GetMemberMenuByMemberID(int memberID);
        /// <summary>
        /// 获取树形列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<MenuModel.TreeList> GetTreeList();
        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="menuID"></param>
        /// <returns></returns>
        SysMenu GetModel(int? menuID);
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        ResultEx Add(SysMenu menu);

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        ResultEx Edit(SysMenu menu);

        /// <summary>
        /// 批量删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ResultEx Deletes(List<int> ids);
        /// <summary>
        /// 获取菜单下拉树
        /// </summary>
        /// <param name="showButton"></param>
        /// <returns></returns>
        List<TreeModel> GetTree(bool showButton = false);
        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        List<MenuModel.Function> GetFunctionList(int menuId);
        /// <summary>
        /// 保存功能列表
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="List"></param>
        /// <returns></returns>
        ResultEx Save(int menuId, List<SysMenuFunction> List);
        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<MenuModel.Function> GetFunctionList(int menuId,int userId);
    }
}