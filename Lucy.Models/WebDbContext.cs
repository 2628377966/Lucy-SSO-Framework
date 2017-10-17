
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


    public partial class WebDbContext : DbContext, IWebDbContext
    {

        public IDbSet<SysAccount> SysAccounts { get; set; }


        public IDbSet<SysAccountRole> SysAccountRoles { get; set; }


        public IDbSet<SysMenu> SysMenus { get; set; }


        public IDbSet<SysMenuFunction> SysMenuFunctions { get; set; }


        public IDbSet<SysRole> SysRoles { get; set; }


        public IDbSet<SysRoleFunction> SysRoleFunctions { get; set; }


        public IDbSet<SysRoleMenu> SysRoleMenus { get; set; }


        
        static WebDbContext()
        {

            System.Data.Entity.Database.SetInitializer<WebDbContext>(null);

        }

        public WebDbContext()
            : base("Name=WebDB")
        {
            InitializePartial();

        }

        public WebDbContext(string connectionString) : base(connectionString)
        {
            InitializePartial();

        }

        public WebDbContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model) : base(connectionString, model)
        {
            InitializePartial();

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Configurations.Add(new SysAccountConfiguration());

            modelBuilder.Configurations.Add(new SysAccountRoleConfiguration());

            modelBuilder.Configurations.Add(new SysMenuConfiguration());

            modelBuilder.Configurations.Add(new SysMenuFunctionConfiguration());

            modelBuilder.Configurations.Add(new SysRoleConfiguration());

            modelBuilder.Configurations.Add(new SysRoleFunctionConfiguration());

            modelBuilder.Configurations.Add(new SysRoleMenuConfiguration());



            OnModelCreatingPartial(modelBuilder);

        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {

            modelBuilder.Configurations.Add(new SysAccountConfiguration(schema));

            modelBuilder.Configurations.Add(new SysAccountRoleConfiguration(schema));

            modelBuilder.Configurations.Add(new SysMenuConfiguration(schema));

            modelBuilder.Configurations.Add(new SysMenuFunctionConfiguration(schema));

            modelBuilder.Configurations.Add(new SysRoleConfiguration(schema));

            modelBuilder.Configurations.Add(new SysRoleFunctionConfiguration(schema));

            modelBuilder.Configurations.Add(new SysRoleMenuConfiguration(schema));

            return modelBuilder;
        }


        partial void InitializePartial();
        partial void OnModelCreatingPartial(DbModelBuilder modelBuilder);

    }

}
