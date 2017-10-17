using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace Lucy.WebApi.Controllers
{
    /// <summary>
    /// 权限测试接口
    /// </summary>
    public class IdentityController : ApiController
    {
        /// <summary>
        /// 权限测试-返回所有的claims
        /// </summary>
        /// <returns></returns>
        public dynamic Get()
        {
            var principal = User as ClaimsPrincipal;

            return from c in principal.Identities.First().Claims
                   select new
                   {
                       c.Type,
                       c.Value
                   };
        }
    }
}
