
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


    internal partial class SysAccountConfiguration : EntityTypeConfiguration<SysAccount>
    {
        public SysAccountConfiguration(string schema = "dbo")
        {
 
           ToTable(schema + ".Sys_Account");
 
           HasKey(x => x.Id);


            Property(x => x.Id).HasColumnName("ID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.RealName).HasColumnName("RealName").IsRequired().HasMaxLength(50);

            Property(x => x.UserName).HasColumnName("UserName").IsRequired().IsUnicode(false).HasMaxLength(20);

            Property(x => x.Password).HasColumnName("Password").IsRequired().HasMaxLength(200);

            Property(x => x.IsEnabled).HasColumnName("IsEnabled").IsRequired();

            Property(x => x.HeadPoint).HasColumnName("HeadPoint").IsOptional().HasMaxLength(50);

            Property(x => x.UserType).HasColumnName("UserType").IsRequired();

            Property(x => x.CreateDate).HasColumnName("CreateDate").IsRequired();




            InitializePartial();
        }

        partial void InitializePartial();
    }



}
