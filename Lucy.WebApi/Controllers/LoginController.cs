using IdentityModel.Client;
using Lucy.Services.Interfaces;
using Lucy.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Lucy.WebApi.Controllers
{
    /// <summary>
    /// 通过resource owner方式获取token
    /// </summary>
    public class LoginController : ApiController
    {
        #region field
        public IAccountSvc _AccountSvc { get; set; }
        #endregion
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost, ResponseType(typeof(ResultEx))]
        public IHttpActionResult Login(string userName, string password)
        {
            var response = GetUserToken(userName, password);
            if (response.IsError)
            {
                return Ok(ResultEx.Init(false, response.Error));
            }
            var claims = new List<System.Security.Claims.Claim>();
            var model = _AccountSvc.GetFullModel(userName);
            if (model != null)
            {
                claims.Add(new System.Security.Claims.Claim("UserName", model.UserName));
                claims.Add(new System.Security.Claims.Claim("UserType", model.UserType.ToString()));
                claims.Add(new System.Security.Claims.Claim("UserTypeName", model.UserTypeName));
            }
            var tockenModel = new TokenModel
            {
                AccessToken = response.AccessToken,
                ExpiresIn = response.ExpiresIn,
                TokenType = response.TokenType,
                Claims = claims
            };

            return Ok(ResultEx.Init(true, "", tockenModel));
        }


        /// <summary>
        /// 获取access_token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public TokenResponse GetUserToken(string userName, string password)
        {
            var client = new TokenClient(address: Constants.TokenEndpoint
                , clientId: Constants.Password_ClientId
                , clientSecret: Constants.Password_ClientSecret);
            return client.RequestResourceOwnerPasswordAsync(userName, password, Constants.WebApi_Scope).Result;
        }
        /// <summary>
        /// 返回的token信息
        /// </summary>
        public class TokenModel
        {
            /// <summary>
            /// AccessToken
            /// </summary>
            public string AccessToken { get; set; }
            /// <summary>
            /// 过期时间
            /// </summary>
            public long ExpiresIn { get; set; }
            /// <summary>
            /// tokenType
            /// </summary>
            public string TokenType { get; set; }
            /// <summary>
            /// 用户信息
            /// </summary>
            public List<System.Security.Claims.Claim> Claims { get; set; }
        }
    }
}
