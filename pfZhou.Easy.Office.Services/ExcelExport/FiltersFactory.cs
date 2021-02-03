using pfZhou.Easy.Office.Core.ExcelExport.Attributes;
using pfZhou.Easy.Office.IServices.ExcelExport;
using pfZhou.Easy.Office.Models.ExcelExport;
using System.Collections.Generic;
using System.Linq;

namespace pfZhou.Easy.Office.Services.ExcelExport
{
    /// <summary>
    /// 创建过滤器工厂
    /// </summary>
    public static class FiltersFactory
    {
        public static List<IFilter> CreateFilters(DecorationContext context)
        {
            List<IFilter> filters = new List<IFilter>();
            List<BaseDecorationAttribute> bdecoAttrs = new List<BaseDecorationAttribute>();
            bdecoAttrs.AddRange(context.TypeDecoInfo.TypeDecoAttrs);
            bdecoAttrs.AddRange(context.TypeDecoInfo.propertyDecoInfos.SelectMany(pd => pd.PropertyAttrs));
            bdecoAttrs.Distinct(new DecorationCompare()).ToList().ForEach(bd =>
            {
                filters.Add(FilterFactory.CreateInstance(bd));
            });
            return filters;
        }
    }
}
