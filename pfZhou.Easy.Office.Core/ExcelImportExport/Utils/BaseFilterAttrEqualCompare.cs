using System;
using System.Collections.Generic;
using System.Text;
using pfZhou.Easy.Office.Core.ExcelImportExport.Attributes;

namespace pfZhou.Easy.Office.Core.ExcelImportExport.Utils
{
    /// <summary>
    /// 特性过滤器的比较器
    /// </summary>
    public class BaseFilterAttrEqualCompare : IEqualityComparer<BaseFilterAttribute>
    {
        public bool Equals(BaseFilterAttribute x, BaseFilterAttribute y)
        {
            return x.GetType() == y.GetType();
        }

        public int GetHashCode(BaseFilterAttribute obj)
        {
            return obj.GetType().GetHashCode();
        }
    }
}
