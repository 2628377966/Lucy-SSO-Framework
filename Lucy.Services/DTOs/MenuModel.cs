using Lucy.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy.Services.Dtos
{
    public class MenuModel
    {
        public class TreeList : TreeGridModel
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
            /// <summary>
            /// 功能集合
            /// </summary>
            public string Functions { get; set; }
        }
        public class Function
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
        }
    }
}
