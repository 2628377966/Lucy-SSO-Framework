using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy.Tools
{
    public static class ExtendClass
    {
        public static string ToStringEx(this object value, string defaultValue = "")
        {
            try
            {
                if (value == null)
                    return defaultValue;
                return value.ToString();
            }
            catch
            {
                return defaultValue;
            }
        }
        public static string ToStringEx(this string value, string defaultValue = "")
        {
            try
            {
                if (value == null)
                    return defaultValue;
                return value.ToString();
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// Object To Int
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToIntEx(this object value, int defaultValue = 0)
        {
            try
            {
                if (value == null)
                    return defaultValue;
                return int.Parse(value.ToString());
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// Object To JsonString
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string ToJsonString(this object value, string defaultValue = "")
        {
            try
            {
                if (value == null)
                    return defaultValue;
                return Newtonsoft.Json.JsonConvert.SerializeObject(value);
            }
            catch
            {
                return defaultValue;
            }
        }
        public static bool ToBoolEx(this object value, bool defaultValue = false)
        {
            try
            {
                if (value == null)
                    return defaultValue;
                return bool.Parse(value.ToString());
            }
            catch
            {
                return defaultValue;
            }
        }
        /// <summary>
        /// 返回分页数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<T> SplitList<T>(this string value)
        {
            var list = new List<T>();

            if (string.IsNullOrEmpty(value) == false)
            {
                var values = value.Split(',');

                foreach (var item in values)
                {
                    T v = (T)Convert.ChangeType(item, typeof(T));

                    if (v != null)
                    {
                        list.Add(v);
                    }
                }
            }

            return list;
        } 
        /// <summary>
        /// 数组Join返回字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string JoinEx(this List<int> value, string split = ",")
        {
            if (value.Count == 0)
                return string.Empty;
            return string.Join(split, value);
        }
        /// <summary>
        /// 数组Join返回字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string JoinEx(this IEnumerable<int> value, string split = ",")
        {
            if (value.ToList().Count == 0)
                return string.Empty;
            return string.Join(split, value);
        }
        /// <summary>
        /// 数组Join返回字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string JoinEx(this IEnumerable<string> value, string split = ",")
        {
            if (value.ToList().Count == 0)
                return string.Empty;
            return string.Join(split, value);
        }
    }
}
