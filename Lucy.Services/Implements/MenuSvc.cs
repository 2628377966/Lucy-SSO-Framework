using System.Collections.Generic;
using System.Linq;
using Mehdime.Entity;
using Lucy.Services.Interfaces;
using Lucy.Models;
using EntityFramework.Extensions;
using Lucy.Tools;
using Lucy.Services.Dtos;
using System;
using System.Transactions;

namespace Lucy.Services.Implements
{
    public class MenuSvc : IMenuSvc
    {
        private readonly IDbContextScopeFactory _dbScopeFactory = new DbContextScopeFactory();

        #region method
        public IEnumerable<SysMenu> GetMemberMenuByMemberID(int memberID)
        {
            using (var dbScope = _dbScopeFactory.CreateReadOnly())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                if (!SysConfig.Current.SC_SYS_OPENPOWER) //是否开启权限 
                    return db.SysMenus.Where(p => p.IsEnabled == false).ToList();
                else
                {
                    var query = from t in db.SysAccountRoles
                                join pp in db.SysRoles on t.RoleId equals pp.Id into tempP
                                from p in tempP.DefaultIfEmpty()
                                join aa in db.SysRoleMenus on p.Id equals aa.RoleId into tempA
                                from a in tempA.DefaultIfEmpty()
                                join bb in db.SysMenus on a.MenuId equals bb.Id into tempB
                                from b in tempB.DefaultIfEmpty()
                                where !b.IsEnabled && t.UserId == CurrentUser.User.UserId
                                orderby b.Sort ascending
                                select b;
                    return query.Distinct().ToList();
                }
            }
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public ResultEx Add(SysMenu menu)
        {
            var result = new ResultEx();
            using (var dbScope = _dbScopeFactory.Create())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                var model = db.SysMenus.Add(menu);
                db.SaveChanges();
                result.data = model;
                result.msg = "添加成功";
                return result;
            }
        }
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public ResultEx Edit(SysMenu menu)
        {
            var result = new ResultEx();
            using (var dbScope = _dbScopeFactory.Create())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                db.Entry(menu).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                result.msg = "修改成功";
                return result;
            }
        }

        /// <summary>
        /// 批量删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResultEx Deletes(List<int> ids)
        {
            using (var ts = new TransactionScope())
            {
                using (var dbScope = _dbScopeFactory.Create())
                {
                    try
                    {
                        var db = dbScope.DbContexts.Get<WebDbContext>();
                        var list = db.SysMenus.ToList();

                        Func<int, List<int>> func = null;
                        func = new Func<int, List<int>>(p =>
                        {
                            var result = new List<int>();
                            var childList = list.Where(x => x.ParentId == p).ToList();
                            if (childList.Count > 0)
                                result.AddRange(childList.Select(x => x.Id).ToList());
                            foreach (var item in childList)
                            {
                                result.AddRange(func(item.Id));
                            }
                            return result;
                        });
                        foreach (var id in ids)
                        {
                            var childIds = func(id);
                            if (childIds.Count > 0)
                                db.SysMenus.Where(x => childIds.Contains(x.Id)).Delete();
                            db.SysMenus.Where(x => x.Id == id).Delete();
                            db.SysMenuFunctions.Where(x => x.MenuId == id).Delete();
                        }
                        db.SaveChanges();
                        ts.Complete();
                        return ResultEx.Init();
                    }
                    catch (Exception ex)
                    {
                        ts.Dispose();
                        return ResultEx.Init(ex);
                    }
                }
            }
        }

        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="menuID"></param>
        /// <returns></returns>
        public SysMenu GetModel(int? menuID)
        {
            if (!menuID.HasValue)
                return new SysMenu();
            using (var dbScope = _dbScopeFactory.CreateReadOnly())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                return db.SysMenus.Where(p => p.Id == menuID).SingleOrDefault();
            }
        }

        /// <summary>
        /// 获取菜单树形数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MenuModel.TreeList> GetTreeList()
        {
            using (var dbScope = _dbScopeFactory.CreateReadOnly())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                var list = db.SysMenus.ToList();


                Func<int, List<MenuModel.TreeList>> func = null;
                func = new Func<int, List<MenuModel.TreeList>>(p =>
                {
                    var result = new List<MenuModel.TreeList>();
                    var filterList = list.Where(x => x.ParentId == p).OrderBy(x => x.Sort).ToList();

                    foreach (var item in filterList)
                    {
                        var menu = new MenuModel.TreeList();
                        menu.Url = item.Url;
                        menu.Sort = item.Sort;
                        menu.ParentId = item.ParentId;
                        menu.MenuName = item.MenuName;
                        menu.IsEnabled = item.IsEnabled;
                        menu.Id = item.Id;
                        menu.Icon = item.Icon;
                        menu.Functions = string.Join(",", item.SysMenuFunctions.Select(x => x.FunctionName));

                        #region  获取level
                        if (item.ParentId == 0)
                            menu.level = 0;
                        else
                        {
                            var level = 0;
                            var parentID = item.ParentId;
                            while (true)
                            {
                                var parent = list.Where(x => x.Id == parentID).SingleOrDefault();
                                level += 1;
                                if (parent.ParentId == 0)
                                    break;
                                else
                                {
                                    parentID = parent.ParentId;
                                    continue;
                                }
                            }

                            menu.level = level;
                        }
                        #endregion

                        var hasChild = func(item.Id).Count > 0;
                        menu.loaded = true;
                        menu.isLeaf = !hasChild;
                        menu.expanded = hasChild;
                        menu.parent = item.ParentId == 0 ? "null" : item.ParentId.ToString();

                        result.Add(menu);
                        if (hasChild)
                            result.AddRange(func(item.Id));
                    }
                    return result;
                });
                return func(0);
            }
        }

        public List<TreeModel> GetTree(bool showButton = false)
        {
            using (var dbScope = _dbScopeFactory.CreateReadOnly())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                var list = db.SysMenus.Where(p => p.IsEnabled == false).ToList();

                Func<int, List<TreeModel>> func = null;
                func = new Func<int, List<TreeModel>>(m =>
                {
                    var result = new List<TreeModel>();
                    foreach (var item in list.Where(p => p.ParentId == m).OrderBy(p => p.Sort))
                    {
                        var obj = func(item.Id);
                        var buttons = GetMenuButton(item.Id);
                        result.Add(new TreeModel
                        {
                            attributes = "{type:'menu'}",
                            id = item.Id.ToString(),
                            state = (obj.Count == 0 && (!showButton || buttons.Count == 0)) ? "open" : "closed",
                            text = item.MenuName,
                            children = obj.Count > 0 ? obj : (showButton ? buttons : null)
                        });
                    }

                    return result;
                });
                var resultObj = new List<TreeModel>();
                resultObj.Add(new TreeModel
                {
                    id = "0",
                    text = SysConfig.Current.SC_SYS_SCHOOLNAME,
                    state = "open",
                    children = func(0)
                });
                return resultObj;
            }
        }
        private List<TreeModel> GetMenuButton(int menuID)
        {
            using (var dbScope = _dbScopeFactory.CreateReadOnly())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                var list = db.SysMenuFunctions.Where(p => p.MenuId == menuID).ToList();

                var result = new List<TreeModel>();

                foreach (var item in list)
                {
                    result.Add(new TreeModel
                    {
                        attributes = "{type:'button',menuid:" + menuID + "}",
                        children = null,
                        id = "b" + item.Id,
                        state = "open",
                        text = item.FunctionName
                    });
                }
                return result;
            }
        }
        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public List<MenuModel.Function> GetFunctionList(int menuId)
        {
            using (var dbScope = _dbScopeFactory.CreateReadOnly())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                var query = from p in db.SysMenuFunctions
                            where p.MenuId == menuId
                            select new MenuModel.Function
                            {
                                Id = p.Id,
                                FunctionCode = p.FunctionCode,
                                FunctionName = p.FunctionName,
                                Sort = p.Sort,
                                MenuId = p.MenuId
                            };
                return query.ToList();
            }
        }
        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<MenuModel.Function> GetFunctionList(int menuId, int userId)
        {
            using (var dbScope = _dbScopeFactory.CreateReadOnly())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                var query = from p in db.SysAccountRoles
                            join aa in db.SysRoleFunctions on p.RoleId equals aa.RoleId into tempA
                            from a in tempA.DefaultIfEmpty()
                            join bb in db.SysMenuFunctions on a.FunctionId equals bb.Id into tempB
                            from b in tempB.DefaultIfEmpty()
                            where b.MenuId == menuId && p.UserId == userId
                            select new MenuModel.Function
                            {
                                Id = b.Id,
                                FunctionCode = b.FunctionCode,
                                FunctionName = b.FunctionName,
                                Sort = b.Sort,
                                MenuId = b.MenuId
                            };
                return query.Distinct().ToList();
            }
        }
        /// <summary>
        /// 保存功能
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="List"></param>
        /// <returns></returns>
        public ResultEx Save(int menuId, List<SysMenuFunction> List)
        {
            using (TransactionScope ts = new TransactionScope())
            {
                try
                {
                    using (var dbScope = _dbScopeFactory.Create())
                    {
                        var db = dbScope.DbContexts.Get<WebDbContext>();
                        var notDeleteIds = List.Where(p => p.Id != 0).Select(p => p.Id).ToList();
                        if (notDeleteIds.Count > 0)
                            db.SysMenuFunctions.Where(p => !notDeleteIds.Contains(p.Id) && p.MenuId == menuId).Delete();
                        foreach (var item in List)
                        {
                            item.MenuId = menuId;
                            if (item.Id != 0)
                                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                            else
                            {
                                db.SysMenuFunctions.Add(item);
                            }
                        }
                        var rs = db.SaveChanges();
                    }
                    ts.Complete();
                    return ResultEx.Init();
                }
                catch (Exception ex)
                {
                    ts.Dispose();
                    return ResultEx.Init(ex);
                }
            }
        }

        #endregion
    }
}