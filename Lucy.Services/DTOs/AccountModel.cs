using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucy.Tools;

namespace Lucy.Services.Dtos
{
    public class AccountModel
    {
        public class Search : SearchModel
        {
            public int? usertype { get; set; }
        }
        public class Full
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
            /// <summary>
            /// 用户类型-中文
            /// </summary>
            public string UserTypeName
            {
                get
                {
                    return EnumHelper.GetEnumDescription((SysEnum.UserType)this.UserType);
                }
            }
            /// <summary>
            /// 角色名称
            /// </summary>
            public string RoleName { get; set; }
        }
        public class ModifyPwd
        {
            /// <summary>
            /// 旧密码
            /// </summary>
            public string OldPwd { get; set; }

            /// <summary>
            /// 新密码
            /// </summary>
            public string NewPwd { get; set; }

            /// <summary>
            /// 确认密码
            /// </summary>
            public string ConfirmPwd { get; set; }
            /// <summary>
            /// 用户ID
            /// </summary>
            public int UserID { get; set; }
        }
        public class SmallModel
        {
            public int Id { get; set; }
            public string RealName { get; set; }
            public int UserType { get; set; }
            /// <summary>
            /// 用户类型-中文
            /// </summary>
            public string UserTypeName
            {
                get
                {
                    return EnumHelper.GetEnumDescription((SysEnum.UserType)this.UserType);
                }
            }
        }
    }
}
