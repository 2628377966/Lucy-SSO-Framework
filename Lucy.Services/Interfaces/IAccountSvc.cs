using System.Collections.Generic;
using Lucy.Services.Dtos;
using Lucy.Tools;
using Lucy.Models;

namespace Lucy.Services.Interfaces
{
    public interface IAccountSvc
    {
        #region IAccountSvc
        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        SysAccount GetModel(string UserName);
        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        object GetPager(GridPageModel pager, AccountModel.Search search, bool studented = false);
        /// <summary>
        /// 批量更新状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        ResultEx Enabled(List<int> ids, bool flag);
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        ResultEx ResetPassWord(List<int> ids, string password);


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultEx UpdatePassword(AccountModel.ModifyPwd model);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ResultEx UpdatePassword(int userId, AccountModel.ModifyPwd model);
        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="id"></param>
        /// <param name="headPoint"></param>
        /// <returns></returns>
        ResultEx UpdateHeadPoint(int id, string headPoint);
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<AccountModel.SmallModel> GetAccountList(AccountModel.Search search);
        /// <summary>
        /// 获取全部的用户
        /// </summary>
        /// <returns></returns>
        List<AccountModel.Full> GetFullList();
        /// <summary>
        /// 获取full model
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>

        AccountModel.Full GetFullModel(string userName);
        #endregion

        #region IAccountRoleSvc
        /// <summary>
        /// 用户角色保存
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        ResultEx SaveAccoutRole(int userId, List<int> roleIds);
        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<int> GetRoleIdsByUserId(int userId);
        #endregion


    }
}
