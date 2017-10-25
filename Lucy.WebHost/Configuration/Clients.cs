/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer3.Host.Config
{
    public class Clients
    {
        public static List<Client> Get()
        {
            return new List<Client>
            {
                /////////////////////////////////////////////////////////////
                // MVC OWIN Hybrid Client
                /////////////////////////////////////////////////////////////
                new Client
                {
                    ClientName = "MVC OWIN Hybrid Client",
                    ClientId = Lucy.Constants.MVCCLIENTID,
                    Flow = Flows.Hybrid,
                    AllowAccessTokensViaBrowser = false,

                    ClientSecrets = new List<Secret>
                    {
                        new Secret(Lucy.Constants.MVCSECRET.Sha256())
                    },

                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.Email,
                        Constants.StandardScopes.AllClaims,
                        Constants.StandardScopes.Roles,
                        Constants.StandardScopes.OfflineAccess,
                        Lucy.Constants.WebApi_Scope
                    },

                    ClientUri = "https://identityserver.io",

                    RequireConsent = false,
                    AccessTokenType = AccessTokenType.Reference,

                    RedirectUris = new List<string>
                    {
                       Lucy.Constants.MVCURL
                    },

                    PostLogoutRedirectUris = new List<string>
                    {
                         Lucy.Constants.MVCURL
                    },

                    LogoutUri =  Lucy.Constants.MVCURL+"Home/OidcSignOut",
                    LogoutSessionRequired = true
                },
               new Client{
                    ClientId = Lucy.Constants.Password_ClientId,
                   ClientSecrets = new List<Secret>
                   {
                       new Secret(Lucy.Constants.Password_ClientSecret.Sha256())
                   },
                   ClientName = "密码模式适用于APP/SPA-2",
                   AccessTokenType=AccessTokenType.Reference,
                   Flow = Flows.ResourceOwner,
                   AllowedScopes = new List<string>
                   {
                       Constants.StandardScopes.OpenId,
                       Constants.StandardScopes.Profile,
                       Constants.StandardScopes.Email,
                       Constants.StandardScopes.AllClaims,
                       Constants.StandardScopes.Roles,
                       Constants.StandardScopes.OfflineAccess,
                       Lucy.Constants.WebApi_Scope
                   }
                },
                 new Client
                {
                    ClientName = "MVC OWIN Hybrid Client",
                    ClientId = Lucy.Constants.MVC1_CLIENTID,
                    Flow = Flows.Hybrid,
                    AllowAccessTokensViaBrowser = false,

                    ClientSecrets = new List<Secret>
                    {
                        new Secret(Lucy.Constants.MVC1_SECRET.Sha256())
                    },

                    AllowedScopes = new List<string>
                    {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.Email,
                        Constants.StandardScopes.AllClaims,
                        Constants.StandardScopes.Roles,
                        Constants.StandardScopes.OfflineAccess,
                        Lucy.Constants.WebApi_Scope
                    },

                    ClientUri = "https://identityserver.io",

                    RequireConsent = false,
                    AccessTokenType = AccessTokenType.Reference,

                    RedirectUris = new List<string>
                    {
                       Lucy.Constants.MVC1_URL
                    },

                    PostLogoutRedirectUris = new List<string>
                    {
                         Lucy.Constants.MVC1_URL
                    },

                    LogoutUri =  Lucy.Constants.MVC1_URL+"Home/OidcSignOut",
                    LogoutSessionRequired = true
                }
            };
        }
    }
}