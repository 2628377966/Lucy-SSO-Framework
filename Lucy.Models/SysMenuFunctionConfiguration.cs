
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


    internal partial class SysMenuFunctionConfiguration : EntityTypeConfiguration<SysMenuFunction>
    {
        public SysMenuFunctionConfiguration(string schema = "dbo")
        {
 
           ToTable(schema + ".Sys_Menu_Function");
 
           HasKey(x => x.Id);


            Property(x => x.Id).HasColumnName("ID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.MenuId).HasColumnName("MenuID").IsRequired();

            Property(x => x.FunctionCode).HasColumnName("FunctionCode").IsRequired().IsUnicode(false).HasMaxLength(50);

            Property(x => x.FunctionName).HasColumnName("FunctionName").IsRequired().HasMaxLength(50);

            Property(x => x.Sort).HasColumnName("Sort").IsRequired();





            HasRequired(a => a.SysMenu).WithMany(b => b.SysMenuFunctions).HasForeignKey(c => c.MenuId);



            InitializePartial();
        }

        partial void InitializePartial();
    }



}
