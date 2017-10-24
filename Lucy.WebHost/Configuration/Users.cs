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
using IdentityServer3.Core.Services.InMemory;
using Lucy.Services.Dtos;
using Lucy.Services.Implements;
using System.Collections.Generic;
using System.Security.Claims;

namespace IdentityServer3.Host.Config
{
    static class Users
    {
        public static List<InMemoryUser> Get()
        {
            var userList = new AccountSvc().GetFullList();
            var users = SysAccountToUser(userList);

            return users;
        }
        private static List<InMemoryUser> SysAccountToUser(List<AccountModel.Full> list)
        {
            var users = new List<InMemoryUser>();
            foreach (var item in list)
            {
                users.Add(new InMemoryUser
                {
                    Subject = item.UserName,
                    Username = item.UserName,
                    Password = item.Password,
                    Claims = new Claim[]
                    {
                        new Claim("UserName", item.UserName),
                         new Claim("UserType", item.UserType.ToString()),
                           new Claim("UserTypeName", item.UserTypeName)
                    }
                });
            }
            return users;
        }
    }
}