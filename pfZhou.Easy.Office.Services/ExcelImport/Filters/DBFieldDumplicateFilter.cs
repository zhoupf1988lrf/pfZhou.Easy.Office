using pfZhou.Easy.Office.Core.ExcelImportExport.Attributes;
using pfZhou.Easy.Office.Entities;
using pfZhou.Easy.Office.IServices.ExcelImport;
using pfZhou.Easy.Office.IServices.ExcelImport.Interfaces;
using pfZhou.Easy.Office.Models.ExcelImport;
using pfZhou.Easy.Office.Models.ExcelImport.Models.Excel;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace pfZhou.Easy.Office.Services.ExcelImport.Filters
{
    /// <summary>
    /// 数据库表字段重复的过滤器
    /// </summary>
    [FilterBind(typeof(DBFieldDumplicateAttribute))]
    public class DBFieldDumplicateFilter : BaseFilter, IFilter
    {
        public List<ExcelDataRow> Filter(List<ExcelDataRow> dataRows, FilterContext filterContext, ImportOption importOption)
        {
            foreach (var dr in dataRows)
            {
                if (!dr.IsValid && importOption.ImportMode == Core.ExcelImportExport.Enums.ImportMode.ErrorAfterStop)
                    continue;
                dr.excelDataColList.ForEach(edc =>
                {
                    DBFieldDumplicateAttribute dBFieldDumplicate = edc.GetAttribute<DBFieldDumplicateAttribute>(filterContext.TypeFilter);
                    if (dBFieldDumplicate != null)
                    {
                        Type validateClass = dBFieldDumplicate.ValidateClass;
                        if (!validateClass.IsSubclassOf(typeof(Entity))) throw new NotSupportedException(nameof(validateClass));
                        MethodInfo method = typeof(Extend).GetMethod(nameof(Extend.DBCheck)).MakeGenericMethod(validateClass);
                        string selectResult = (string)method.Invoke(null, new object[] { dBFieldDumplicate, dBFieldDumplicate.ValidateProperty, edc.CellString });
                        edc.SetNotValid(dr, string.IsNullOrWhiteSpace(selectResult), dBFieldDumplicate.ErrorMsg);
                    }
                });
            }
            return dataRows;
        }
    }
}
