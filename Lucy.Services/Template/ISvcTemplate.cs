 
using System.Collections.Generic;
using Lucy.Models;
using Lucy.Services.Dtos;
using Lucy.Tools;


  
namespace Lucy.Services.Template
{
	public interface ISysAccountSvc
	{
	#region ISysAccountSvc
	/// <summary>
    /// 获取模型
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns></returns>
	SysAccount GetModel(int? id);
	/// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pager">分页model</param>
    /// <param name="search">查询model</param>
    /// <returns></returns>
	object GetPager(GridPageModel pager, SearchModel search);
	/// <summary>
	/// 保存
	/// </summary>
	/// <param name="model">model</param>
	/// <returns></returns>
	ResultEx Save(SysAccount model);
	 /// <summary>
	/// 删除
	/// </summary>
	/// <param name="ids">主键列表</param>
	/// <returns></returns>
	ResultEx Delete(List<int> ids);
	#endregion
	}
  } 
  
namespace Lucy.Services.Template
{
	public interface ISysMenuSvc
	{
	#region ISysMenuSvc
	/// <summary>
    /// 获取模型
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns></returns>
	SysMenu GetModel(int? id);
	/// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pager">分页model</param>
    /// <param name="search">查询model</param>
    /// <returns></returns>
	object GetPager(GridPageModel pager, SearchModel search);
	/// <summary>
	/// 保存
	/// </summary>
	/// <param name="model">model</param>
	/// <returns></returns>
	ResultEx Save(SysMenu model);
	 /// <summary>
	/// 删除
	/// </summary>
	/// <param name="ids">主键列表</param>
	/// <returns></returns>
	ResultEx Delete(List<int> ids);
	#endregion
	}
  } 
  
namespace Lucy.Services.Template
{
	public interface ISysMenuFunctionSvc
	{
	#region ISysMenuFunctionSvc
	/// <summary>
    /// 获取模型
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns></returns>
	SysMenuFunction GetModel(int? id);
	/// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pager">分页model</param>
    /// <param name="search">查询model</param>
    /// <returns></returns>
	object GetPager(GridPageModel pager, SearchModel search);
	/// <summary>
	/// 保存
	/// </summary>
	/// <param name="model">model</param>
	/// <returns></returns>
	ResultEx Save(SysMenuFunction model);
	 /// <summary>
	/// 删除
	/// </summary>
	/// <param name="ids">主键列表</param>
	/// <returns></returns>
	ResultEx Delete(List<int> ids);
	#endregion
	}
  } 
  
namespace Lucy.Services.Template
{
	public interface ISysRoleSvc
	{
	#region ISysRoleSvc
	/// <summary>
    /// 获取模型
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns></returns>
	SysRole GetModel(int? id);
	/// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="pager">分页model</param>
    /// <param name="search">查询model</param>
    /// <returns></returns>
	object GetPager(GridPageModel pager, SearchModel search);
	/// <summary>
	/// 保存
	/// </summary>
	/// <param name="model">model</param>
	/// <returns></returns>
	ResultEx Save(SysRole model);
	 /// <summary>
	/// 删除
	/// </summary>
	/// <param name="ids">主键列表</param>
	/// <returns></returns>
	ResultEx Delete(List<int> ids);
	#endregion
	}
  } 
    
