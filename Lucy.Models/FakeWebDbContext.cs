
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


    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.14.1.0")]
    public class FakeWebDbContext : IWebDbContext
    {

        public IDbSet<SysAccount> SysAccounts { get; set; }

        public IDbSet<SysAccountRole> SysAccountRoles { get; set; }

        public IDbSet<SysMenu> SysMenus { get; set; }

        public IDbSet<SysMenuFunction> SysMenuFunctions { get; set; }

        public IDbSet<SysRole> SysRoles { get; set; }

        public IDbSet<SysRoleFunction> SysRoleFunctions { get; set; }

        public IDbSet<SysRoleMenu> SysRoleMenus { get; set; }


        public FakeWebDbContext()
        {

            SysAccounts = new FakeDbSet<SysAccount>();

            SysAccountRoles = new FakeDbSet<SysAccountRole>();

            SysMenus = new FakeDbSet<SysMenu>();

            SysMenuFunctions = new FakeDbSet<SysMenuFunction>();

            SysRoles = new FakeDbSet<SysRole>();

            SysRoleFunctions = new FakeDbSet<SysRoleFunction>();

            SysRoleMenus = new FakeDbSet<SysRoleMenu>();

        }

        public int SaveChanges()
        {
            return 0;
        }


        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }


        protected virtual void Dispose(bool disposing)
        {
        }
        
        public void Dispose()
        {
            Dispose(true);
        }

    }

}
