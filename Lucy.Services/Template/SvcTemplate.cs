
 
using System.Linq;
using System.Collections.Generic;
using EntityFramework.Extensions;
using Mehdime.Entity;
using Lucy.Models;
using Lucy.Services.Dtos;
using Lucy.Services.Interfaces;
using Lucy.Tools;
namespace Lucy.Services.Template
{
  	  public class SysAccountSvc : ISysAccountSvc
      {
        private readonly IDbContextScopeFactory _dbScopeFactory = new DbContextScopeFactory();

        #region ISysAccountSvc
		/// <summary>
		/// 获取模型
		/// </summary>
		/// <param name="id">主键</param>
		/// <returns></returns>
        public SysAccount GetModel(int? id)
        {
            using (var dbScope = _dbScopeFactory.Create())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                if (id == null)
                    return null;
                else
                    return db.SysAccounts.SingleOrDefault(p => p.Id == id.Value);
            }
        }
		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="pager">分页model</param>
		/// <param name="search">查询model</param>
		/// <returns></returns>
        public object GetPager(GridPageModel pager, SearchModel search)
        {
            using (var dbScope = _dbScopeFactory.Create())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                var query = from p in db.SysAccounts
                            select p;
                return new PagedList<SysAccount>(query, pager).ToPager();
            }
        }
		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="model">model</param>
		/// <returns></returns>
        public ResultEx Save(SysAccount model)
        {
                using (var dbScope = _dbScopeFactory.Create())
                {
                    var db = dbScope.DbContexts.Get<WebDbContext>();
                        if (model.Id == 0)
                        {
                            db.SysAccounts.Add(model);
                        }
                        else
                        {
                            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        }
                       return ResultEx.Init(db.SaveChanges() > 0);
                }
        }

		
		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="ids">主键列表</param>
		/// <returns></returns>
        public ResultEx Delete(List<int> ids)
        {
            using (var dbScope = _dbScopeFactory.Create())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                db.SysAccounts.Where(p => ids.Contains(p.Id)).Delete();
                return ResultEx.Init();
            }
        }
        #endregion
      }
	   	  public class SysMenuSvc : ISysMenuSvc
      {
        private readonly IDbContextScopeFactory _dbScopeFactory = new DbContextScopeFactory();

        #region ISysMenuSvc
		/// <summary>
		/// 获取模型
		/// </summary>
		/// <param name="id">主键</param>
		/// <returns></returns>
        public SysMenu GetModel(int? id)
        {
            using (var dbScope = _dbScopeFactory.Create())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                if (id == null)
                    return null;
                else
                    return db.SysMenus.SingleOrDefault(p => p.Id == id.Value);
            }
        }
		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="pager">分页model</param>
		/// <param name="search">查询model</param>
		/// <returns></returns>
        public object GetPager(GridPageModel pager, SearchModel search)
        {
            using (var dbScope = _dbScopeFactory.Create())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                var query = from p in db.SysMenus
                            select p;
                return new PagedList<SysMenu>(query, pager).ToPager();
            }
        }
		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="model">model</param>
		/// <returns></returns>
        public ResultEx Save(SysMenu model)
        {
                using (var dbScope = _dbScopeFactory.Create())
                {
                    var db = dbScope.DbContexts.Get<WebDbContext>();
                        if (model.Id == 0)
                        {
                            db.SysMenus.Add(model);
                        }
                        else
                        {
                            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        }
                       return ResultEx.Init(db.SaveChanges() > 0);
                }
        }

		
		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="ids">主键列表</param>
		/// <returns></returns>
        public ResultEx Delete(List<int> ids)
        {
            using (var dbScope = _dbScopeFactory.Create())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                db.SysMenus.Where(p => ids.Contains(p.Id)).Delete();
                return ResultEx.Init();
            }
        }
        #endregion
      }
	   	  public class SysMenuFunctionSvc : ISysMenuFunctionSvc
      {
        private readonly IDbContextScopeFactory _dbScopeFactory = new DbContextScopeFactory();

        #region ISysMenuFunctionSvc
		/// <summary>
		/// 获取模型
		/// </summary>
		/// <param name="id">主键</param>
		/// <returns></returns>
        public SysMenuFunction GetModel(int? id)
        {
            using (var dbScope = _dbScopeFactory.Create())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                if (id == null)
                    return null;
                else
                    return db.SysMenuFunctions.SingleOrDefault(p => p.Id == id.Value);
            }
        }
		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="pager">分页model</param>
		/// <param name="search">查询model</param>
		/// <returns></returns>
        public object GetPager(GridPageModel pager, SearchModel search)
        {
            using (var dbScope = _dbScopeFactory.Create())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                var query = from p in db.SysMenuFunctions
                            select p;
                return new PagedList<SysMenuFunction>(query, pager).ToPager();
            }
        }
		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="model">model</param>
		/// <returns></returns>
        public ResultEx Save(SysMenuFunction model)
        {
                using (var dbScope = _dbScopeFactory.Create())
                {
                    var db = dbScope.DbContexts.Get<WebDbContext>();
                        if (model.Id == 0)
                        {
                            db.SysMenuFunctions.Add(model);
                        }
                        else
                        {
                            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        }
                       return ResultEx.Init(db.SaveChanges() > 0);
                }
        }

		
		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="ids">主键列表</param>
		/// <returns></returns>
        public ResultEx Delete(List<int> ids)
        {
            using (var dbScope = _dbScopeFactory.Create())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                db.SysMenuFunctions.Where(p => ids.Contains(p.Id)).Delete();
                return ResultEx.Init();
            }
        }
        #endregion
      }
	   	  public class SysRoleSvc : ISysRoleSvc
      {
        private readonly IDbContextScopeFactory _dbScopeFactory = new DbContextScopeFactory();

        #region ISysRoleSvc
		/// <summary>
		/// 获取模型
		/// </summary>
		/// <param name="id">主键</param>
		/// <returns></returns>
        public SysRole GetModel(int? id)
        {
            using (var dbScope = _dbScopeFactory.Create())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                if (id == null)
                    return null;
                else
                    return db.SysRoles.SingleOrDefault(p => p.Id == id.Value);
            }
        }
		/// <summary>
		/// 分页查询
		/// </summary>
		/// <param name="pager">分页model</param>
		/// <param name="search">查询model</param>
		/// <returns></returns>
        public object GetPager(GridPageModel pager, SearchModel search)
        {
            using (var dbScope = _dbScopeFactory.Create())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                var query = from p in db.SysRoles
                            select p;
                return new PagedList<SysRole>(query, pager).ToPager();
            }
        }
		/// <summary>
		/// 保存
		/// </summary>
		/// <param name="model">model</param>
		/// <returns></returns>
        public ResultEx Save(SysRole model)
        {
                using (var dbScope = _dbScopeFactory.Create())
                {
                    var db = dbScope.DbContexts.Get<WebDbContext>();
                        if (model.Id == 0)
                        {
                            db.SysRoles.Add(model);
                        }
                        else
                        {
                            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                        }
                       return ResultEx.Init(db.SaveChanges() > 0);
                }
        }

		
		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="ids">主键列表</param>
		/// <returns></returns>
        public ResultEx Delete(List<int> ids)
        {
            using (var dbScope = _dbScopeFactory.Create())
            {
                var db = dbScope.DbContexts.Get<WebDbContext>();
                db.SysRoles.Where(p => ids.Contains(p.Id)).Delete();
                return ResultEx.Init();
            }
        }
        #endregion
      }
	   	   }
