using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy.Tools
{

    [AttributeUsage(AttributeTargets.Property)]
    public partial class KeyFlagAttribute : Attribute
    {
        private bool _IsExtend;
        public bool IsExtend { get { return _IsExtend; } }
        public KeyFlagAttribute(bool isExtend)
        {
            _IsExtend = isExtend;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public partial class ImportAttribute : Attribute
    {
        /// <summary>
        /// 导入Excel字段名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// ImportAttributeType
        /// </summary>
        public ImportAttributeTypeEnum ImportAttributeType { get; set; }
        /// <summary>
        /// 国标类型
        /// </summary>
        public SysEnum.AuditStatus GbType { get; set; }

        /// <summary>
        /// 枚举类型
        /// </summary>
        public Type TypeEnum { get; set; }
        /// <summary>
        /// 是否为空
        /// </summary>
        [DefaultValue(false)]
        public bool IsRequired { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        [DefaultValue(0)]
        public int MaxLength { get; set; }
        /// <summary>
        /// 指定长度
        /// </summary>
        [DefaultValue(0)]
        public int Length { get; set; }

    }
    public enum ImportAttributeTypeEnum
    {
        IsString,
        IsDateTime,
        IsDouble,
        IsDecimal,
        IsInt,
        IsFloat,
        IsCode,
        IsEnum,
        IsBool,
        IsEmail,
        IsPhone, 
        IsClass
    }
}
