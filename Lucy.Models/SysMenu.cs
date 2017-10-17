
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

    public partial class SysMenu
    {




        ///<summary>
        /// ID
        ///</summary>


        public int Id { get; set; }



        ///<summary>
        /// 菜单名称
        ///</summary>


        public string MenuName { get; set; }



        ///<summary>
        /// 地址
        ///</summary>


        public string Url { get; set; }



        ///<summary>
        /// 父节点
        ///</summary>


        public int ParentId { get; set; }



        ///<summary>
        /// 是否禁用
        ///</summary>


        public bool IsEnabled { get; set; }



        ///<summary>
        /// 排序
        ///</summary>


        public int Sort { get; set; }



        ///<summary>
        /// 图标
        ///</summary>


        public string Icon { get; set; }





        public virtual ICollection<SysMenuFunction> SysMenuFunctions { get; set; }



        
        public SysMenu()
        {


            SysMenuFunctions = new List<SysMenuFunction>();

            InitializePartial();
        }


        partial void InitializePartial();

    }



}
