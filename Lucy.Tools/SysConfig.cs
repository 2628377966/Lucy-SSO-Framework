using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lucy.Tools
{
    public class SysConfig
    {
        public const string CONFIGFILENAME = "sysconfig.config";

        /// <summary>
        /// Config
        /// </summary>
        [Serializable]
        public class Config
        {
            /// <summary>
            /// 是否开启权限
            /// </summary>
            [XmlElement("是否开启权限")]
            public bool SC_SYS_OPENPOWER { get; set; } 
            /// <summary>
            /// 学校名称
            /// </summary>
            [XmlElement("学校名称")]
            public string SC_SYS_SCHOOLNAME { get; set; }
            /// <summary>
            /// 系统名称
            /// </summary>
            [XmlElement("系统名称")]
            public string SC_SYS_WEBSITENAME { get; set; }
            /// <summary>
            /// 微信二维码
            /// </summary>
            [XmlElement("微信二维码")]
            public string SC_SYS_WECHATQRCODE { get; set; }
            /// <summary>
            /// 网站底部
            /// </summary>
            [XmlElement("网站底部")]
            public string SC_SYS_FOOTER { get; set; }
        }

        /// <summary>
        /// _current
        /// </summary>
        static Config _current = null;

        public static Config Current
        {
            get
            {
                var path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "app_data", CONFIGFILENAME);
                if (!File.Exists(path)) return new Config();
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                try
                {
                    XmlSerializer ser = new XmlSerializer(typeof(Config));
                    _current = ser.Deserialize(fs) as Config;
                }
                catch
                {
                    _current = new Config();
                }
                finally
                {
                    fs.Close();
                }
                return _current;
            }
            set
            {
                XmlSerializer ser = new XmlSerializer(typeof(Config));
                string _path = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "app_data", CONFIGFILENAME);
                if (!File.Exists(_path)) File.Create(_path).Close();
                StreamWriter fs = new StreamWriter(_path, false);
                ser.Serialize(fs, value);
                fs.Close();
                _current = value;
            }
        }
    }
}
