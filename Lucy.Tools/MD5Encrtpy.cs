using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy.Tools
{
    public class MD5Encrtpy
    {
        private static String[] hexDigits = { "0", "1", "2", "3", "4", "5",
            "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };

        private Object salt;
     
        public MD5Encrtpy()
        {
            this.salt = "yqsh_sso";
        }
        public MD5Encrtpy(Object salt)
        {
            this.salt = salt;
        }
        /// <summary>
        /// 加盐加密
        /// </summary>
        /// <param name="rawPass"></param>
        /// <returns></returns>
        public String encode(String rawPass)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

            rawPass = mergePasswordAndSalt(rawPass);

            var bytes = Encoding.UTF8.GetBytes(rawPass);

            return byteArrayToHexString(md5.ComputeHash(bytes));
        }
        /// <summary>
        /// 不加盐加密
        /// </summary>
        /// <param name="rawPass"></param>
        /// <returns></returns>
        public String encodeNoSalt(String rawPass)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

            var bytes = Encoding.UTF8.GetBytes(rawPass);

            return byteArrayToHexString(md5.ComputeHash(bytes));
        }


        public bool isPasswordValid(String encPass, String rawPass)
        {
            String pass1 = "" + encPass;
            String pass2 = encode(rawPass);

            return pass1.Equals(pass2);
        }

        private String mergePasswordAndSalt(String password)
        {
            if (password == null)
            {
                password = "";
            }

            if ((salt == null) || "".Equals(salt))
            {
                return password;
            }
            else
            {
                return password + "{" + salt.ToString() + "}";
            }
        }

        /**
         * 转换字节数组为16进制字串
         * 
         * @param b
         *            字节数组
         * @return 16进制字串
         */
        private static String byteArrayToHexString(byte[] b)
        {
            StringBuilder resultSb = new StringBuilder();
            for (int i = 0; i < b.Length; i++)
            {
                resultSb.Append(byteToHexString(b[i]));
            }
            return resultSb.ToString();
        }

        private static String byteToHexString(byte b)
        {
            int n = b;
            if (n < 0)
                n = 256 + n;
            int d1 = n / 16;
            int d2 = n % 16;
            return hexDigits[d1] + hexDigits[d2];
        }
    }
}
