using pfZhou.Easy.Office.Core.ExcelExport.Attributes;
using System.Collections.Generic;

namespace pfZhou.Easy.Office.Models.ExcelExport
{
    /// <summary>
    /// 装饰器特性类的比较器
    /// </summary>
    public class DecorationCompare : IEqualityComparer<BaseDecorationAttribute>
    {
        public bool Equals(BaseDecorationAttribute x, BaseDecorationAttribute y)
        {
            return x.GetType() == y.GetType();
        }

        public int GetHashCode(BaseDecorationAttribute obj)
        {
            return obj.GetType().GetHashCode();
        }
    }
}
