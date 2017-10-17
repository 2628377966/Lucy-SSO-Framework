using EntityFramework.Extensions;
using Mehdime.Entity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using Lucy.Services.Dtos;
using Lucy.Services.Interfaces;
using Lucy.Models;
using Lucy.Tools;

namespace Lucy.Services.Implements
{
    public class AccountSvc : IAccountSvc
    {
        private readonly IDbContextScopeFactory _dbScopeFactory = new DbContextScopeFactory();

        #region  IAccountSvc 成员
        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public SysAccount GetModel(string UserName)
        {
            using (var dbScope = _dbScopeFactory.CreateReadOnly())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                return db.SysAccounts.Where(p => p.UserName.Equals(UserName)).SingleOrDefault();
            }
        }
        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="search"></param>
        /// <param name="studented">只获取学生类型的</param>
        /// <returns></returns>
        public object GetPager(GridPageModel pager, AccountModel.Search search,bool studented=false)
        {
            using (var dbScope = _dbScopeFactory.CreateReadOnly())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                var tempQuery = from p in db.SysAccounts
                                join aa in db.SysAccountRoles on p.Id equals aa.UserId into tempA
                                from a in tempA.DefaultIfEmpty()
                                join bb in db.SysRoles on a.RoleId equals bb.Id into tempB
                                from b in tempB.DefaultIfEmpty()
                                where (string.IsNullOrEmpty(search.SearchKey) ? true : (p.RealName.Contains(search.SearchKey) || p.UserName.Contains(search.SearchKey))) &&
                                (search.usertype.HasValue ? p.UserType == search.usertype.Value : true)
                                &&(studented ? p.UserType == (int)SysEnum.UserType.学生 : true)
                                select new AccountModel.Full
                                {
                                    CreateDate = p.CreateDate,
                                    HeadPoint = p.HeadPoint,
                                    Id = p.Id,
                                    IsEnabled = p.IsEnabled,
                                    Password = p.Password,
                                    RealName = p.RealName,
                                    RoleName = b.RoleName,
                                    UserName = p.UserName,
                                    UserType = p.UserType
                                };
                var tempTable = tempQuery.ToList();
                var query = from p in tempTable
                            group p by new { p.CreateDate, p.HeadPoint, p.Id, p.IsEnabled, p.Password, p.RealName, p.UserName, p.UserType } into g
                            select g.Aggregate((now, next) => new AccountModel.Full
                            {
                                CreateDate = g.Key.CreateDate,
                                HeadPoint = g.Key.HeadPoint,
                                Id = g.Key.Id,
                                IsEnabled = g.Key.IsEnabled,
                                Password = g.Key.Password,
                                RealName = g.Key.RealName,
                                RoleName = now.RoleName + "," + next.RoleName,
                                UserName = g.Key.UserName,
                                UserType = g.Key.UserType
                            });
                return new PagedList<AccountModel.Full>(query.ToList(), pager).ToPager();
            }
        }
        /// <summary>
        /// 批量更新状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public ResultEx Enabled(List<int> ids, bool flag)
        {
            using (var dbScope = _dbScopeFactory.Create())
            {
                try
                {
                    var db = dbScope.DbContexts.Get<WebDbContext>();
                    db.SysAccounts.Where(x => ids.Contains(x.Id)).Update(x => new SysAccount { IsEnabled = !flag });
                    return ResultEx.Init();
                }
                catch (System.Exception ex)
                {
                    return ResultEx.Init(ex);
                }
            }
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ResultEx ResetPassWord(List<int> ids, string password)
        {
            using (var dbScope = _dbScopeFactory.Create())
            {
                try
                {
                    var db = dbScope.DbContexts.Get<WebDbContext>();
                    db.SysAccounts.Where(x => ids.Contains(x.Id)).Update(x => new SysAccount { Password = password });
                    return ResultEx.Init();
                }
                catch (System.Exception ex)
                {
                    return ResultEx.Init(ex);
                }
            }
        }
      
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResultEx UpdatePassword(AccountModel.ModifyPwd model)
        {
            using (var dbScope = _dbScopeFactory.Create())
            {
                try
                {
                    var db = dbScope.DbContexts.Get<WebDbContext>();
                    var accountModel = this.GetModel(CurrentUser.User.UserName);
                    if (accountModel == null)
                        return ResultEx.Init(false, "未能找到用户信息！");
                    if (accountModel.Password != new MD5Encrtpy().encode(model.OldPwd))
                        return ResultEx.Init(false, "原始密码错误！");
                    accountModel.Password = new MD5Encrtpy().encode(model.NewPwd);
                    db.Entry(accountModel).State = EntityState.Modified;
                    db.SaveChanges();
                    return ResultEx.Init();
                }
                catch (System.Exception ex)
                {
                    return ResultEx.Init(ex);
                }
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResultEx UpdatePassword(int userId, AccountModel.ModifyPwd model)
        {
            using (var dbScope = _dbScopeFactory.Create())
            {
                try
                {
                    var db = dbScope.DbContexts.Get<WebDbContext>();
                    var accountModel = db.SysAccounts.Where(x => x.Id == userId).SingleOrDefault();
                    if (accountModel == null)
                        return ResultEx.Init(false, "未能找到用户信息！");
                    if (accountModel.Password != new MD5Encrtpy().encode(model.OldPwd))
                        return ResultEx.Init(false, "原始密码错误！");
                    db.SysAccounts.Where(x => x.Id == userId).Update(x => new SysAccount { Password = new MD5Encrtpy().encode(model.NewPwd) });
                    db.SaveChanges();
                    return ResultEx.Init();
                }
                catch (System.Exception ex)
                {
                    return ResultEx.Init(ex);
                }
            }
        }
        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="id"></param>
        /// <param name="headPoint"></param>
        /// <returns></returns>
        public ResultEx UpdateHeadPoint(int id, string headPoint)
        {
            using (var dbScope = _dbScopeFactory.Create())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                db.SysAccounts.Where(x => x.Id == id).Update(x => new SysAccount { HeadPoint = headPoint });
                return ResultEx.Init();
            }
        }
        public List<AccountModel.SmallModel> GetAccountList(AccountModel.Search search)
        {
            using (var dbScope = _dbScopeFactory.Create())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                var query = from p in db.SysAccounts
                            where (string.IsNullOrEmpty(search.SearchKey) ? true : p.RealName.Contains(search.SearchKey)) && ((search.usertype == null||search.usertype==0 )? true : p.UserType == search.usertype)
                            select new AccountModel.SmallModel {
                                Id=p.Id,
                                RealName=p.RealName,
                                UserType=p.UserType
                            };
                return query.ToList();
            }
        }
        /// <summary>
        /// 获取全部的用户
        /// </summary>
        /// <returns></returns>
        public List<AccountModel.Full> GetFullList( )
        {
            using (var dbScope = _dbScopeFactory.CreateReadOnly())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                var tempQuery = from p in db.SysAccounts
                                join aa in db.SysAccountRoles on p.Id equals aa.UserId into tempA
                                from a in tempA.DefaultIfEmpty()
                                join bb in db.SysRoles on a.RoleId equals bb.Id into tempB
                                from b in tempB.DefaultIfEmpty()
                                where p.IsEnabled
                                select new AccountModel.Full
                                {
                                    CreateDate = p.CreateDate,
                                    HeadPoint = p.HeadPoint,
                                    Id = p.Id,
                                    IsEnabled = p.IsEnabled,
                                    Password = p.Password,
                                    RealName = p.RealName,
                                    RoleName = b.RoleName,
                                    UserName = p.UserName,
                                    UserType = p.UserType
                                };
                var tempTable = tempQuery.ToList();
                var query = from p in tempTable
                            group p by new { p.CreateDate, p.HeadPoint, p.Id, p.IsEnabled, p.Password, p.RealName, p.UserName, p.UserType } into g
                            select g.Aggregate((now, next) => new AccountModel.Full
                            {
                                CreateDate = g.Key.CreateDate,
                                HeadPoint = g.Key.HeadPoint,
                                Id = g.Key.Id,
                                IsEnabled = g.Key.IsEnabled,
                                Password = g.Key.Password,
                                RealName = g.Key.RealName,
                                RoleName = now.RoleName + "," + next.RoleName,
                                UserName = g.Key.UserName,
                                UserType = g.Key.UserType
                            });
                return query.ToList();
            }
        }
        /// <summary>
        /// 获取full model
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>

        public AccountModel.Full GetFullModel(string userName)
        {
            using (var dbScope = _dbScopeFactory.CreateReadOnly())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                var query = from p in db.SysAccounts
                            join aa in db.SysAccountRoles on p.Id equals aa.UserId into tempA
                            from a in tempA.DefaultIfEmpty()
                            join bb in db.SysRoles on a.RoleId equals bb.Id into tempB
                            from b in tempB.DefaultIfEmpty()
                            where p.IsEnabled && p.UserName == userName
                            select new AccountModel.Full
                            {
                                CreateDate = p.CreateDate,
                                HeadPoint = p.HeadPoint,
                                Id = p.Id,
                                IsEnabled = p.IsEnabled,
                                Password = p.Password,
                                RealName = p.RealName,
                                UserName = p.UserName,
                                UserType = p.UserType
                            };

                return query.SingleOrDefault();
            }
        }
        #endregion

        #region IAccountRoleSvc 成员

        /// <summary>
        /// 用户角色保存
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public ResultEx SaveAccoutRole(int userId, List<int> roleIds)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                using (var dbScope = _dbScopeFactory.Create())
                {
                    try
                    {
                        var db = dbScope.DbContexts.Get<WebDbContext>();
                        db.SysAccountRoles.Where(x => x.UserId == userId).Delete();
                        foreach (var roleId in roleIds)
                        {
                            db.SysAccountRoles.Add(new Models.SysAccountRole { RoleId = roleId, UserId = userId });
                        }
                        db.SaveChanges();
                        ts.Complete();
                        return ResultEx.Init();
                    }
                    catch (System.Exception ex)
                    {
                        ts.Dispose();
                        return ResultEx.Init(ex);
                    }
                }
            }
        }
        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<int> GetRoleIdsByUserId(int userId)
        {
            using (var dbScope = _dbScopeFactory.Create())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                return db.SysAccountRoles.Where(p => p.UserId == userId).Select(p => p.RoleId).ToList();
            }
        }
        #endregion

      
    }
}
