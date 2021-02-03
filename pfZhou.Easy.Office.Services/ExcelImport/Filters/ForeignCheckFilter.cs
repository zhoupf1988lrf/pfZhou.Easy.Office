using pfZhou.Easy.Office.Core.ExcelImportExport.Attributes;
using pfZhou.Easy.Office.Entities;
using pfZhou.Easy.Office.IServices.ExcelImport;
using pfZhou.Easy.Office.IServices.ExcelImport.Interfaces;
using pfZhou.Easy.Office.Models.ExcelImport;
using pfZhou.Easy.Office.Models.ExcelImport.Models.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace pfZhou.Easy.Office.Services.ExcelImport.Filters
{
    /// <summary>
    /// 数据库外键引用的过滤器
    /// </summary>
    [FilterBind(typeof(ForeignCheckAttribute))]
    public class ForeignCheckFilter : BaseFilter, IFilter
    {
        public List<ExcelDataRow> Filter(List<ExcelDataRow> dataRows, FilterContext filterContext, ImportOption importOption)
        {
            foreach (ExcelDataRow dr in dataRows)
            {
                if (!dr.IsValid && importOption.ImportMode == Core.ExcelImportExport.Enums.ImportMode.ErrorAfterStop)
                    continue;
                dr.excelDataColList.ForEach(col =>
                {
                    ForeignCheckAttribute dBCheck = col.GetAttribute<ForeignCheckAttribute>(filterContext.TypeFilter);

                    if (dBCheck != null)
                    {
                        var businessType = dBCheck.FindClass;
                        if (!businessType.IsSubclassOf(typeof(Entity))) throw new NotSupportedException(nameof(businessType));
                        MethodInfo method = typeof(Models.ExcelImport.Extend).GetMethod(nameof(Models.ExcelImport.Extend.DBCheck), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).MakeGenericMethod(dBCheck.FindClass);
                        string mapperPropertyValue = (string)method.Invoke(null, new object[] { dBCheck, dBCheck.FindProperty, col.CellString });
                        ExcelDataCol excelDataCol = dr.excelDataColList.FirstOrDefault(e => e.PropertyName == dBCheck.MapperProperty);
                        excelDataCol.CellString = mapperPropertyValue;

                        col.SetNotValid(dr, !string.IsNullOrWhiteSpace(mapperPropertyValue), dBCheck.ErrorMsg);
                    }
                });
            }
            return dataRows;
        }

    }

}
