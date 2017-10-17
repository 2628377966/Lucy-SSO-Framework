using ExfSoft.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lucy.Tools
{

    /// <summary>
    /// CurrentUser
    /// </summary>
    public class CurrentUser
    {
        /// <summary>
        /// USER_PARAM_KEY
        /// </summary>
        public const string PARAM_KEY = "Lucy.Portal";

        /// <summary>
        /// CRYPTXOR_KEY
        /// </summary>
        public const string CRYPTXOR_KEY = "Lucy.Portal";

        /// <summary>
        /// BaseModel
        /// </summary>
        public class InfoModel
        {
            /// <summary>
            /// MemberID
            /// </summary>
            public int UserId { get; set; }

            /// <summary>
            /// 姓名
            /// </summary>
            public string RealName { get; set; }
            /// <summary>
            /// UserName
            /// </summary>
            public string UserName { get; set; }
            /// <summary>
            /// 头像
            /// </summary>
            public string HeadPoint { get; set; }  
            /// <summary>
            /// 用户类型
            /// </summary>
            public int UserType { get; set; }
        }

        public static bool IsLogin
        {
            get { return User != null; }
        }

        /// <summary>
        /// User
        /// </summary>
        public static InfoModel User
        {
            get
            {
                var user = System.Runtime.Remoting.Messaging.CallContext.GetData(PARAM_KEY);

                if (user == null)
                {
                    if ((HttpContext.Current.Request[PARAM_KEY] != null))
                    {
                        user = JsonUtil.ToObject<CurrentUser.InfoModel>(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(EncryptUtil.CryptXOR(System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(HttpContext.Current.Request[PARAM_KEY])), CRYPTXOR_KEY))));

                        if (user != null)
                        {
                            System.Runtime.Remoting.Messaging.CallContext.SetData(PARAM_KEY, user);
                        }
                    }

                    return (CurrentUser.InfoModel)user;
                }
                else
                {
                    return (CurrentUser.InfoModel)user;
                }
            }
        }

        public static void Set(InfoModel value)
        {
            if (value == null)
            {
                HttpContext.Current.Response.Cookies[PARAM_KEY].Expires = DateTime.Now.AddYears(-1);
            }
            else
            {
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(PARAM_KEY, Convert.ToBase64String(Encoding.UTF8.GetBytes(EncryptUtil.CryptXOR(Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonUtil.ToJson(value))), CRYPTXOR_KEY)))));
            }
        }
    }
}
