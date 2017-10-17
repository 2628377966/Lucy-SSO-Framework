using Mehdime.Entity;
using System.Collections.Generic;
using System.Linq;
using Lucy.Services.Dtos;
using Lucy.Services.Interfaces;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Transactions;
using EntityFramework.Extensions;
using Lucy.Models;
using Lucy.Tools;

namespace Lucy.Services.Implements
{
    public class RoleSvc : IRoleSvc
    {
        private readonly IDbContextScopeFactory _dbScopeFactory = new DbContextScopeFactory(); 
        #region method
        /// <summary> 
        /// 获取模型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysRole GetModel(int? id)
        {
            if (!id.HasValue) return null;
            using (var dbScope = _dbScopeFactory.CreateReadOnly())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                return db.SysRoles.Where(p => p.Id == id.Value).SingleOrDefault();
            }
        }
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pager"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public object GetPager(GridPageModel pager, SearchModel search)
        {
            var result = new Dictionary<string, object>();
            using (var dbScope = _dbScopeFactory.Create())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                var query = from p in db.SysRoles
                            where (!string.IsNullOrEmpty(search.SearchKey) ? p.RoleName.Contains(search.SearchKey) : p.RoleName == p.RoleName)
                            select p;
                return new PagedList<SysRole>(query, pager).ToPager();
            }
        }
        /// <summary>
        ///获取角色功能权限
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public List<int> GetRoleFuncID(int roleID)
        {
            using (var dbScope = _dbScopeFactory.CreateReadOnly())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                var query = from p in db.SysRoleFunctions
                            where p.RoleId == roleID
                            select p.FunctionId;
                return query.ToList();
            }
        }
        /// <summary>
        /// 获取角色菜单权限
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public List<int> GetRoleMenuID(int roleID)
        {
            using (var dbScope = _dbScopeFactory.CreateReadOnly())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                var query = from p in db.SysRoleMenus
                            where p.RoleId == roleID
                            select p.MenuId;
                return query.ToList();
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <param name="menuIDs"></param>
        /// <param name="funcIDs"></param>
        /// <returns></returns>
        public ResultEx Save(SysRole model, List<int> menuIDs, List<int> funcIDs)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    using (var dbScope = _dbScopeFactory.Create())
                    {
                        var db = dbScope.DbContexts.Get<WebDbContext>();
                        if (model.Id == 0)
                            db.SysRoles.Add(model);
                        else
                            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        if (menuIDs != null)
                        {
                            db.SysRoleMenus.Where(p => p.RoleId == model.Id).Delete();
                            foreach (var item in menuIDs)
                            {
                                db.SysRoleMenus.Add(new SysRoleMenu { MenuId = item, RoleId = model.Id });
                            }
                        }
                        if (funcIDs != null)
                        {
                            db.SysRoleFunctions.Where(p => p.RoleId == model.Id).Delete();
                            foreach (var item in funcIDs)
                            {
                                db.SysRoleFunctions.Add(new SysRoleFunction { FunctionId = item, RoleId = model.Id });
                            }
                        }
                        db.SaveChanges();
                        ts.Complete();
                        return ResultEx.Init();
                    }
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    return ResultEx.Init(ex);
                }
            }

        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public ResultEx Delete(List<int> ids)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    using (var dbScope = _dbScopeFactory.Create())
                    {
                        var db = dbScope.DbContexts.Get<WebDbContext>();

                        var query = from p in db.SysRoles
                                    where ids.Contains(p.Id)
                                    select p;

                        query.Delete();

                        //删除从表
                        db.SysRoleMenus.Where(p => ids.Contains(p.RoleId)).Delete();
                        db.SysRoleFunctions.Where(p => ids.Contains(p.RoleId)).Delete();

                        db.SaveChanges();
                        ts.Complete();
                        return ResultEx.Init();
                    }
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    return ResultEx.Init(ex);
                }
            }
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public List<SysRole> GetList()
        {
            using (var dbScope = _dbScopeFactory.Create())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                return db.SysRoles.ToList();
            }
        }
        #endregion
    }
}
