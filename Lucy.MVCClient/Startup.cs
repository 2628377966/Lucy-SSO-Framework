﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.IdentityModel.Tokens;
using System.Collections.Generic;
using Microsoft.Owin.Security.OpenIdConnect;
using IdentityModel.Client;
using System.Security.Claims;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(Lucy.MVCClient.Startup))]

namespace Lucy.MVCClient
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //登陆后-- 会发回一个token到主程序，OpenID connect中间件验证token，提取声明信息，然后把声明信息传给cookie中间件设置认证cookie。
            //Mircrosoft的JWT handler解析Claim并且映射到ClaimTypes类上显示的是长类型名
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();
            //配置cookie中间件
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });
            //配置OpenID Connect中间件
            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = Lucy.Constants.MVCCLIENTID,
                Authority =Lucy.Constants.BaseAddress,
                RedirectUri = Lucy.Constants.MVCURL,
                PostLogoutRedirectUri = Lucy.Constants.MVCURL,
                ResponseType = "code id_token",
                Scope = "openid profile all_claims offline_access generalApi",

                TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "role"
                },

                SignInAsAuthenticationType = "Cookies",
                //通知机制让我们做声明转换,转换后的声明会保存到cookie中。
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    AuthorizationCodeReceived = async n =>
                    {
                        // use the code to get the access and refresh token
                        var tokenClient = new TokenClient(
                            Constants.TokenEndpoint,
                           Lucy.Constants.MVCCLIENTID,
                           Lucy.Constants.MVCSECRET);

                        var tokenResponse = await tokenClient.RequestAuthorizationCodeAsync(
                            n.Code, n.RedirectUri);

                        if (tokenResponse.IsError)
                        {
                            throw new Exception(tokenResponse.Error);
                        }

                        // use the access token to retrieve claims from userinfo
                        var userInfoClient = new UserInfoClient(
                        new Uri(Constants.UserInfoEndpoint),
                        tokenResponse.AccessToken);

                        var userInfoResponse = await userInfoClient.GetAsync();

                        // create new identity
                        var id = new ClaimsIdentity(n.AuthenticationTicket.Identity.AuthenticationType);
                        id.AddClaims(userInfoResponse.GetClaimsIdentity().Claims);

                        id.AddClaim(new Claim("access_token", tokenResponse.AccessToken));
                        id.AddClaim(new Claim("expires_at", DateTime.Now.AddSeconds(tokenResponse.ExpiresIn).ToLocalTime().ToString()));
                        id.AddClaim(new Claim("refresh_token", tokenResponse.RefreshToken));
                        id.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));
                        id.AddClaim(new Claim("sid", n.AuthenticationTicket.Identity.FindFirst("sid").Value));

                        n.AuthenticationTicket = new AuthenticationTicket(
                            new ClaimsIdentity(id.Claims, n.AuthenticationTicket.Identity.AuthenticationType, "name", "role"),
                            n.AuthenticationTicket.Properties);
                    },
                    //登出时把id_token发送到identityServer
                    //以便我们重定向到正确的URL上（不是垃圾邮件地址或者钓鱼地址）
                    RedirectToIdentityProvider = n =>
                    {
                        // if signing out, add the id_token_hint
                        if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                        {
                            var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");

                            if (idTokenHint != null)
                            {
                                n.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                            }

                        }

                        return Task.FromResult(0);
                    }
                }
            });
        }
    }
}
