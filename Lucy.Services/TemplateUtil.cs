using Lucy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lucy.Services
{
    public class TemplateUtil
    {
        public class TemplateName
        {
            public string ModelName { get; set; }
            public string EntityName { get; set; }
        }
        public static List<TemplateName> GetModelNames()
        {
            //不适用代码生成器的类,例如无主键id的类
            List<string> excludList = new List<string> { "Database", "ChangeTracker", "Configuration", "SysAccountRoles", "SysRoleMenus", "SysRoleFunctions" };
            List<TemplateName> ModelNames = new List<TemplateName>();
            Type t = new WebDbContext().GetType();
            PropertyInfo[] PropertyList = t.GetProperties();
            foreach (PropertyInfo item in PropertyList)
            {
                if (!excludList.Contains(item.Name))
                {
                    ModelNames.Add(new TemplateName { ModelName = ToSingular(item.Name), EntityName = item.Name });
                }
            }
            return ModelNames;
        }


        /// <summary>
                /// 单词变成单数形式
                /// </summary>
                /// <param name="word"></param>
                /// <returns></returns>
        public static string ToSingular(string word)
        {
            Regex plural1 = new Regex("(?<keep>[^aeiou])ies$");
            Regex plural2 = new Regex("(?<keep>[aeiou]y)s$");
            Regex plural3 = new Regex("(?<keep>[sxzh])es$");
            Regex plural4 = new Regex("(?<keep>[^sxzhyu])s$");
            Regex plural5 = new Regex("(?<keep>[u])s$");
            if (word.ToLower() == "news")
                return word;
            else if (plural1.IsMatch(word))
                return plural1.Replace(word, "${keep}y");
            else if (plural2.IsMatch(word))
                return plural2.Replace(word, "${keep}");
            else if (plural3.IsMatch(word))
                return plural3.Replace(word, "${keep}");
            else if (plural4.IsMatch(word))
                return plural4.Replace(word, "${keep}");
            else if (plural5.IsMatch(word))
                return plural5.Replace(word, "${keep}");


            return word;
        }
        /// <summary>
        /// 单词变成复数形式
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string ToPlural(string word)
        {
            Regex plural1 = new Regex("(?<keep>[^aeiou])y$");
            Regex plural2 = new Regex("(?<keep>[aeiou]y)$");
            Regex plural3 = new Regex("(?<keep>[sxzh])$");
            Regex plural4 = new Regex("(?<keep>[^sxzhy])$");

            if (plural1.IsMatch(word))
                return plural1.Replace(word, "${keep}ies");
            else if (plural2.IsMatch(word))
                return plural2.Replace(word, "${keep}s");
            else if (plural3.IsMatch(word))
                return plural3.Replace(word, "${keep}es");
            else if (plural4.IsMatch(word))
                return plural4.Replace(word, "${keep}s");

            return word;
        }
    }
}
