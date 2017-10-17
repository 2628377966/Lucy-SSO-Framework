using IdentityServer3.Core.Services.InMemory;
using IdentityServer3.Host.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Lucy.WebHost.UserService
{
    public class UserBiz
    {
        public static InMemoryUser CheckLogin(string userName, string password)
        {
            var user = Users.Get().Find(t => t.Username == userName);
            if (user==null)
            {
                return null;
            }
            var md5 = new Lucy.Tools.MD5Encrtpy();
            string passwordHash = md5.encode(md5.encodeNoSalt(password));
            if (object.Equals(passwordHash,user.Password))
            {
                return user;
            }
           
            return null;
        }
        public static InMemoryUser GetUserInfoById(string subject)
        {
            var user = Users.Get().Find(t => t.Subject == subject);
            if (user != null)
            {
                return user;
            }
            return null;
        }
        
    }
}