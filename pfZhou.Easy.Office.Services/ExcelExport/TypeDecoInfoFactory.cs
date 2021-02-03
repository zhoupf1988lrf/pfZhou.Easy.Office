using pfZhou.Easy.Office.Core.ExcelExport.Attributes;
using pfZhou.Easy.Office.Models.ExcelExport;
using System.Linq;
using System.Reflection;

namespace pfZhou.Easy.Office.Services.ExcelExport
{
    /// <summary>
    /// DTO类型装饰器信息类的工厂
    /// </summary>
    public static class TypeDecoInfoFactory
    {
        /// <summary>
        /// 创建DTO类型装饰器信息
        /// </summary>
        /// <typeparam name="TExcel"></typeparam>
        /// <returns></returns>
        public static TypeDecoInfo CreateTypeDecoInfo<TExportExcel>()
            where TExportExcel : class, new()
        {
            TypeDecoInfo typeDecoInfo = new TypeDecoInfo();

            typeDecoInfo.TypeDecoAttrs = typeof(TExportExcel).GetCustomAttributes<BaseDecorationAttribute>().ToList();
            var propertys = typeof(TExportExcel).GetProperties().Where(p => p.IsDefined(typeof(BaseDecorationAttribute), true)).ToList();
            propertys.ForEach(p =>
            {
                typeDecoInfo.propertyDecoInfos.Add(new PropertyDecoInfo
                {
                    PropertyName = p.Name,
                    PropertyAttrs = p.GetCustomAttributes<BaseDecorationAttribute>().ToList()
                });
            });

            return typeDecoInfo;
        }
    }
}
