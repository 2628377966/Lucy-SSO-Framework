
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


    internal partial class SysMenuConfiguration : EntityTypeConfiguration<SysMenu>
    {
        public SysMenuConfiguration(string schema = "dbo")
        {
 
           ToTable(schema + ".Sys_Menu");
 
           HasKey(x => x.Id);


            Property(x => x.Id).HasColumnName("ID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.MenuName).HasColumnName("MenuName").IsRequired().HasMaxLength(50);

            Property(x => x.Url).HasColumnName("Url").IsOptional().IsUnicode(false).HasMaxLength(100);

            Property(x => x.ParentId).HasColumnName("ParentID").IsRequired();

            Property(x => x.IsEnabled).HasColumnName("IsEnabled").IsRequired();

            Property(x => x.Sort).HasColumnName("Sort").IsRequired();

            Property(x => x.Icon).HasColumnName("Icon").IsOptional().IsUnicode(false).HasMaxLength(50);




            InitializePartial();
        }

        partial void InitializePartial();
    }



}
