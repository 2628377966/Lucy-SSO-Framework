using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy.Tools
{
    public class SysEnum
    {
        public enum Sex : int
        {
            男 = 1,
            女 = 2
        }
        public enum UserType : int
        {
            教职工 = 1,
            学生 = 2,
            管理员 = 3
        }
        public enum LoginType : int
        {
            Web客户端 = 1,
            单点登录 = 2,
            手机客户端 = 3
        }
        public enum AuditStatus : int
        {
            未审核 = 0,
            审核通过 = 1,
            审核不通过 = 2
        }
        public enum ThirdInterface : int
        {
            教务 = 1,
            学工 = 2,
            就业 = 3,
            一卡通 = 4,
            图书馆 = 5,
            OA = 6,
            招生系统 = 7
        }
        public enum NewsType : int
        {
            帮助中心 = 1
        }
    }
}
