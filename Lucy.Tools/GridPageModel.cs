using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lucy.Tools
{
    /// <summary>
    /// JqGrid分页参数
    /// </summary>
    public class GridPageModel
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 每页大小
        /// </summary>
        public int rows { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string sidx { get; set; }
        /// <summary>
        /// 排序方式  
        /// </summary>
        public string sord { get; set; }
        /// <summary>
        /// JQGrid组合筛选
        /// </summary>
        public string filters { get; set; }
    }
}
