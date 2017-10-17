using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucy.Tools;

namespace Lucy.Services.Dtos
{
    public static class PagerHelper
    {
        public static object ToPager<T>(this PagedList<T> list)
        {
            var result = new Dictionary<string, object>();
            result["records"] = list.TotalCount;
            result["total"] = list.TotalPages;
            result["rows"] = list;
            return result;
        }
    }
}
