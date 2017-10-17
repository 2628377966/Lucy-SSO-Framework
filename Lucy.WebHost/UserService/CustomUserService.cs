using IdentityServer3.Core;
using IdentityServer3.Core.Extensions;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using IdentityServer3.Host.Config;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Lucy.WebHost.UserService
{
    public class CustomUserService : UserServiceBase
    {
       /// <summary>
       /// 使用username password自定义登录
       /// </summary>
       /// <param name="context"></param>
       /// <returns></returns>
        public override Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            var signInMessage = context.SignInMessage;
            var user = UserBiz.CheckLogin(context.UserName, context.Password);
            if (user != null)
            {
                context.AuthenticateResult = new AuthenticateResult(user.Subject, user.Username);
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// 每次claim被请求的时候都被调用
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var identity = new ClaimsIdentity();

            var user = UserBiz.GetUserInfoById(context.Subject.GetSubjectId());

            if (user != null)
            {
                identity.AddClaims(user.Claims);
            }

            context.IssuedClaims = identity.Claims; 
            return Task.FromResult(identity.Claims);
        }

       
    }
}