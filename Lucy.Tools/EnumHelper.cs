using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy.Tools
{
    public static class EnumHelper
    {
        #region 获取枚举
        public static List<EnumValue> GetEnumList(Type enumType)
        {
            var list = new List<EnumValue>();
            string[] strArray = Enum.GetNames(enumType);
            foreach (string item in strArray)
            {
                int enumValue = (int)Enum.Parse(enumType, item, true);
                string text = GetEnumDescription(enumType, item);

                list.Add(new EnumValue { Text = text, Value = enumValue });

            }
            return list;
        }

        /// <summary>
        /// 获得描述
        /// </summary>
        /// <param name="value">枚举</param>
        /// <returns>描述内容</returns>
        public static string GetEnumDescription(Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            string description = value.ToString();

            Type eunmtype = value.GetType();

            return GetEnumDescription(eunmtype, description);
        }

        public static string GetEnumDescription(Type enumType, string name)
        {
            try
            {
                DescriptionAttribute[] attributes = (DescriptionAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes != null && attributes.Length > 0)
                {
                    name = attributes[0].Description;
                }
                else
                {
                    name = name.ToString();
                }
                return name;
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion
    }
    public class EnumValue
    {
        public int Value { get; set; } 
        public string Text { get; set; }
    }
}
