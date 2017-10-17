
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




    public partial class SysAccount
    {




        ///<summary>
        /// ID
        ///</summary>


        public int Id { get; set; }



        ///<summary>
        /// 真实姓名
        ///</summary>


        public string RealName { get; set; }



        ///<summary>
        /// 工号/用户名
        ///</summary>


        public string UserName { get; set; }



        ///<summary>
        /// 密码
        ///</summary>


        public string Password { get; set; }



        ///<summary>
        /// 是否启用
        ///</summary>


        public bool IsEnabled { get; set; }



        ///<summary>
        /// 头像
        ///</summary>


        public string HeadPoint { get; set; }



        ///<summary>
        /// 用户类型
        ///</summary>


        public int UserType { get; set; }



        ///<summary>
        /// 创建时间
        ///</summary>


        public DateTime CreateDate { get; set; }




    }



}
