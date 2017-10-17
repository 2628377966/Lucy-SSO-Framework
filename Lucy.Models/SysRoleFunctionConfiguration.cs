
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


    internal partial class SysRoleFunctionConfiguration : EntityTypeConfiguration<SysRoleFunction>
    {
        public SysRoleFunctionConfiguration(string schema = "dbo")
        {
 
           ToTable(schema + ".Sys_Role_Function");
 
           HasKey(x => new { x.RoleId, x.FunctionId });


            Property(x => x.RoleId).HasColumnName("RoleID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(x => x.FunctionId).HasColumnName("FunctionID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);




            InitializePartial();
        }

        partial void InitializePartial();
    }



}
