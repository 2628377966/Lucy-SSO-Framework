
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




    public partial class SysMenuFunction
    {




        ///<summary>
        /// ID
        ///</summary>


        public int Id { get; set; }



        ///<summary>
        /// 菜单ID
        ///</summary>


        public int MenuId { get; set; }



        ///<summary>
        /// 功能代码
        ///</summary>


        public string FunctionCode { get; set; }



        ///<summary>
        /// 功能名称
        ///</summary>


        public string FunctionName { get; set; }



        ///<summary>
        /// 排序
        ///</summary>


        public int Sort { get; set; }






        public virtual SysMenu SysMenu { get; set; }


    }



}
