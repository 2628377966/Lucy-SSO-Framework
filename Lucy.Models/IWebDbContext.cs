
// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
// TargetFrameworkVersion = 4.51
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


using System;

using System.CodeDom.Compiler;

using System.Collections.Generic;



using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;


using System.ComponentModel.DataAnnotations.Schema;


using System.Data.Entity;




using System.Data.Entity.ModelConfiguration;







using System.Threading;
using System.Threading.Tasks;


using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption;




namespace Lucy.Models
{

    public interface IWebDbContext : IDisposable
    {

        IDbSet<SysAccount> SysAccounts { get; set; }



        IDbSet<SysAccountRole> SysAccountRoles { get; set; }



        IDbSet<SysMenu> SysMenus { get; set; }



        IDbSet<SysMenuFunction> SysMenuFunctions { get; set; }



        IDbSet<SysRole> SysRoles { get; set; }



        IDbSet<SysRoleFunction> SysRoleFunctions { get; set; }



        IDbSet<SysRoleMenu> SysRoleMenus { get; set; }





        int SaveChanges();

        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);


    }


}
