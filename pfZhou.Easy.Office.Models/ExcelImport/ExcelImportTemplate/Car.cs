using pfZhou.Easy.Office.Core.ExcelImportExport.Attributes;
using pfZhou.Easy.Office.Core.ExcelImportExport.Constants;
using System;

namespace pfZhou.Easy.Office.Models.ExcelImport.ExcelImportTemplate
{
    /// <summary>
    /// 
    /// </summary>
    public class Car
    {
        [ColName("车牌号")]  //对应Excel列名
        [Required] //校验必填
        [Regex(RegexConstant.CAR_CODE_REGEX)] //正则表达式校验,RegexConstant预置了一些常用的正则表达式，也可以自定义
        [Dumplicate] //校验模板类该列数据是否重复
        public string CarCode { get; set; }

        [ColName("手机号")]
        [Regex(RegexConstant.MOBILE_CHINA_REGEX)]
        public string Mobile { get; set; }

        [ColName("身份证号")]
        [Regex(RegexConstant.IDENTITY_NUMBER_REGEX)]
        public string IdentityNumber { get; set; }

        [ColName("姓名")]
        [MaxLength(10)] //最大长度校验
        public string Name { get; set; }
        /// <summary>
        /// 姓名编号
        /// </summary>
        public string NameCode { get; set; }

        [ColName("性别")]
        [Regex(RegexConstant.GENDER_REGEX)]
        public GenderEnum Gender { get; set; }

        [ColName("注册日期")]
        [DateTime] //日期校验
        public DateTime RegisterDate { get; set; }

        [ColName("年龄")]
        [Range(0, 120)] //数值范围校验
        public int Age { get; set; }
    }
    public enum GenderEnum
    {
        男,
        女
    }
}
